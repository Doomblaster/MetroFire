﻿using System;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Xaml;
using Rogue.MetroFire.CampfireClient;
using Rogue.MetroFire.CampfireClient.Serialization;
using System.Reactive.Linq;
using System.Linq;

namespace Rogue.MetroFire.UI.ViewModels
{
	public class RoomModuleViewModel : ReactiveObject, IRoomModuleViewModel
	{
		private IRoom _room;
		private readonly IMessageBus _bus;
		private readonly IUserCache _userCache;
		private bool _isActive;
		private bool _userEditingMessage;

		private const string DefaultMessage = "Type your message here or paste/drop files\u00A0";
		private string _userMessage;
		private bool _userEditedMessage;
		private readonly IChatDocument _chatDocument;
		private readonly List<RoomMessage> _messages;

		public RoomModuleViewModel(IRoom room, IMessageBus bus,IUserCache userCache, IChatDocument chatDocument)
		{
			_room = room;
			_bus = bus;
			_userCache = userCache;
			_chatDocument = chatDocument;

			Users = new ReactiveCollection<UserViewModel>();

			UserMessage = DefaultMessage;

			PostMessageCommand = new ReactiveCommand(this.ObservableForProperty(vm => vm.UserEditedMessage)
				.Select(c => c.Value).StartWith(false));
			PostMessageCommand.Subscribe(HandlePostMessage);

			_messages = new List<RoomMessage>();


			_bus.Listen<MessagesReceivedMessage>().Where(msg => msg.RoomId == room.Id).SubscribeUI(HandleMessagesReceived);
			_bus.Listen<RoomInfoReceivedMessage>().Where(msg => msg.Room.Id == _room.Id).SubscribeUI(HandleRoomInfoReceived);
			_bus.Listen<UsersUpdatedMessage>().SubscribeUI(HandleUsersUpdated);

			_bus.SendMessage(new RequestRecentMessagesMessage(room.Id));
			_bus.SendMessage(new RequestRoomInfoMessage(_room.Id));
		}

		private void HandleUsersUpdated(UsersUpdatedMessage obj)
		{
			foreach (var msg in _messages.Where(m => m.TextObject != null))
			{
				foreach (var user in obj.UsersToUpdate)
				{
					if (msg.UserId == user.Id)
					{
						msg.User = user;
						_chatDocument.UpdateMessage(msg.TextObject, user, msg.Message.Type, msg.Message.Body);
					}
				}
			}
		}

		public ReactiveCollection<UserViewModel> Users { get; private set; }

		private void HandleRoomInfoReceived(RoomInfoReceivedMessage obj)
		{
			_room = obj.Room;
			if (_room.Users != null)
			{
				PopulateUsers(_room.Users);
			}
		}

		private void PopulateUsers(IEnumerable<User> users)
		{
			Users.Clear();
			foreach (var vm in users.Select(u => new UserViewModel(u)))
			{
				Users.Add(vm);
			}
		}

		private void HandleMessagesReceived(MessagesReceivedMessage obj)
		{
			foreach (var message in obj.Messages)
			{
				var existingUser = Users.Select(u => u.User).FirstOrDefault(u => u.Id == message.UserId);
				User user = message.UserId != null ? _userCache.GetUser(message.UserId.GetValueOrDefault(), existingUser) : null;
				var textObject = _chatDocument.AddMessage(user, message.Type, message.Body);

				_messages.Add(new RoomMessage(message, user, textObject));
			}
		}

		private void HandlePostMessage(object o)
		{
			_bus.SendMessage(new RequestSpeakInRoomMessage(Id, UserMessage));
			UserMessage = String.Empty;
		}

		public IChatDocument ChatDocument
		{
			get{return _chatDocument;}
		}

		public ReactiveCommand PostMessageCommand { get; set; }

		public string RoomName
		{
			get { return _room.Name; }
		}

		public bool IsActive
		{
			get { return _isActive; }
			set { this.RaiseAndSetIfChanged(vm => vm.IsActive, ref _isActive, value); }
		}

		public string UserMessage
		{
			get { return _userMessage; }
			set
			{
				this.RaiseAndSetIfChanged(vm => vm.UserMessage, ref _userMessage, value);
				UserEditedMessage = value != DefaultMessage && !String.IsNullOrEmpty(value);
			}
		}

		public bool UserEditingMessage
		{
			get { return _userEditingMessage; }
			set
			{
				if (value && UserMessage == DefaultMessage)
				{
					UserMessage = string.Empty;
				}
				if (!value && String.IsNullOrEmpty(UserMessage))
				{
					UserMessage = DefaultMessage;
				}
				this.RaiseAndSetIfChanged(vm => vm.UserEditingMessage, ref _userEditingMessage, value);
			}
		}

		public bool UserEditedMessage
		{
			get { return _userEditedMessage; }
			set { this.RaiseAndSetIfChanged(vm => vm.UserEditedMessage, ref _userEditedMessage, value); }
		}

		public int Id
		{
			get { return _room.Id; }
		}

		private class RoomMessage
		{
			public RoomMessage(Message message, User user, object textObject)
			{
				User = user;
				TextObject = textObject;
				Message = message;
			}

			public Message Message { get; private set; }
			public int? UserId { get { return Message.UserId; } }
			public User User { get; set; }
			public object TextObject { get; private set; }
		}
	}
}