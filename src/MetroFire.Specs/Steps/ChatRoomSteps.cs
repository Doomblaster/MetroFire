﻿using System.Linq;
using FluentAssertions;
using Rogue.MetroFire.CampfireClient;
using Rogue.MetroFire.CampfireClient.Serialization;
using TechTalk.SpecFlow;

namespace MetroFire.Specs.Steps
{
	[Binding]
	public class ChatRoomSteps
	{
		private readonly RoomContext _roomContext;

		public ChatRoomSteps(RoomContext roomContext)
		{
			_roomContext = roomContext;
		}

		[Given(@"a room called ""(.*)""")]
		public void GivenARoomCalledTest(string roomName)
		{
			_roomContext.CreateRoom(roomName);
		}

		[When(@"the message ""(.*)"" is received for room ""(.*)""")]
		public void GivenThatTheMessageHelloWorldIsReceivedForRoomTest(string message, string roomName)
		{
			var idForRoom = _roomContext.IdForRoom(roomName);
			_roomContext.SendMessage(roomName, 
				new MessagesReceivedMessage(idForRoom, new[]{new Message{Body=message, Id=idForRoom}}, null));
		}

		[When(@"the following messages are received for room ""(.*)"" in order:")]
		public void WhenTheFollowingMessagesAreReceivedInOrder(string roomName, Table table)
		{
			var messages = table.Rows.Select(r => new Message {Body = r["Message"]}).ToArray();
			var id = _roomContext.IdForRoom(roomName);
			_roomContext.SendMessage(roomName, new MessagesReceivedMessage(id, messages, null));

		}


		[Then(@"the message ""(.*)"" should be displayed in room ""(.*)""")]
		public void ThenTheMessageHelloWorldShouldBeDisplayedInRoomTest(string message, string roomName)
		{
			_roomContext.MessagesForRoom(roomName).Last().Body.Should().Be(message);
		}

		[Then(@"the following messages should be displayed in room ""(.*)"" in order:")]
		public void ThenTheFollowingMessagesShouldBeDisplayedInRoomTestInOrder(string roomName, Table table)
		{
			var messageBodies = table.Rows.Select(r => r["Message"]);
			_roomContext.MessagesForRoom(roomName).Select(m => m.Body).Should().BeEquivalentTo(messageBodies);
		}

	}
}
