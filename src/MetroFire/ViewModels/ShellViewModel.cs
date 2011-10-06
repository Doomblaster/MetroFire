﻿using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using System;
using ReactiveUI.Xaml;

namespace Rogue.MetroFire.UI.ViewModels
{
	public class ShellViewModel : ReactiveObject, IShellViewModel
	{
		private int _unreadCount;
		private const string DefaultTitle = "metro fire";
		private string _title;

		public ShellViewModel(IMessageBus bus)
		{
			Title = DefaultTitle;

			var activated = bus.Listen<ApplicationActivatedMessage>();
			var deactivated = bus.Listen<ApplicationDeactivatedMessage>();

			bus.Listen<RoomActivityMessage>()
				.SkipUntil(deactivated)
				.TakeUntil(activated.Do(_ => UnreadCount = 0))
				.Repeat() 
				.Subscribe(_ => UnreadCount++);

			
		}

		public ReactiveCommand SettingsCommand { get; private set; }

		protected int UnreadCount
		{
			get {
				return _unreadCount;
			}
			set {
				_unreadCount = value;

				Title = _unreadCount > 0 ? string.Format("({0}) {1}", _unreadCount, DefaultTitle) : DefaultTitle;
			}
		}

		public string Title
		{
			get { return _title; }
			private set { this.RaiseAndSetIfChanged(vm => vm.Title, ref _title, value); }
		}
	}
}
