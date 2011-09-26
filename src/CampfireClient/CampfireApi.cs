﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Rogue.MetroFire.CampfireClient.Serialization;
 
namespace Rogue.MetroFire.CampfireClient
{

	public class CampfireApi : ICampfireApi
	{
		private LoginInfo _loginInfo;
		private readonly IDictionary<SerializerEntry, XmlSerializer> _xmlSerializers = new Dictionary<SerializerEntry, XmlSerializer>();
		private static int _defaultTimeout;

		public CampfireApi()
		{
			var serializers = XmlSerializer.FromTypes(new[] {typeof (Room), typeof (User), typeof (Account), typeof(Message)});
			_xmlSerializers[new SerializerEntry(typeof (Room))] = serializers[0];
			_xmlSerializers[new SerializerEntry(typeof (User))] = serializers[1];
			_xmlSerializers[new SerializerEntry(typeof (Account))] = serializers[2];
			_xmlSerializers[new SerializerEntry(typeof (Message))] = serializers[3];

			_xmlSerializers[new SerializerEntry("messages", typeof(Message[]))] = 
				new XmlSerializer(typeof(Message[]), new XmlRootAttribute("messages"));

			_xmlSerializers[new SerializerEntry("rooms", typeof(Room[]))] = 
				new XmlSerializer(typeof(Room[]), new XmlRootAttribute("rooms"));

			_defaultTimeout = (int)TimeSpan.FromSeconds(15).TotalMilliseconds;
		}

		public Account GetAccountInfo()
		{
			return Get<Account>("/account.xml");
		}

		public Room[] ListRooms()
		{
			var roomArray = Get<Room[]>("rooms.xml", "rooms");
			return roomArray;
		}

		public Unit Join(int id)
		{
			string relativeUri = String.Format("room/{0}/join.xml", id);
			Post<NoResponse>(relativeUri);

			return Unit.Default;
		}

		public Message Speak(int id, string text)
		{
			string relativeUri = String.Format("room/{0}/speak.xml", id);
			var msg = new Message {Type = MessageType.TextMessage, Body = text};

			return Post<Message>(relativeUri, msg, HttpStatusCode.Created);
		}

		public void SetLoginInfo(LoginInfo loginInfo)
		{
			_loginInfo = loginInfo;
		}

		public Room[] ListPresence()
		{
			var roomArray = Get<Room[]>("presence.xml", "rooms");
			return roomArray;
		}

		public void Leave(int id)
		{
			var relativeUri = String.Format("room/{0}/leave.xml", id);
			Post<NoResponse>(relativeUri);
		}

		public Room GetRoom(int roomId)
		{
			var relativeUri = String.Format("room/{0}.xml", roomId);
			return Get<Room>(relativeUri);
		}

		public User GetUser(int userId)
		{
			var relativeUri = String.Format("users/{0}.xml", userId);
			return Get<User>(relativeUri);
		}

		public Message[] GetMessages(int id, int? sinceId = null)
		{
			var uri = String.Format("room/{0}/recent.xml", id);

			if (sinceId != null)
			{
				uri += "?since_message_id=" + sinceId;
			}

			return Get<Message[]>(uri, "messages");
		}

		private T Post<T>(string relativeUri, object data = null, HttpStatusCode expectedCode = HttpStatusCode.OK,
			string returnedRoot = null)
			where T: class
		{
			try
			{
				return DoPost<T>(relativeUri, data, expectedCode, returnedRoot);
			}
			catch (WebException wex)
			{
				if (wex.Status == WebExceptionStatus.Timeout)
				{
					throw new TimeoutException("Campfire API call timed out", wex);
				}
				throw;
			}
		}

		private T DoPost<T>(string relativeUri, object data, HttpStatusCode expectedCode, string returnedRoot) where T : class
		{
			var uri = FormatUri(relativeUri);
			var request = CreateRequest(uri);

			if (data != null)
			{
				var serializer = new XmlSerializer(data.GetType());
				using (var stream = request.GetRequestStream())
				{
					serializer.Serialize(stream, data);
				}
			}

			var response = (HttpWebResponse) request.GetResponse();
			if (response.StatusCode != expectedCode)
			{
				throw new Exception("Unexpected response code: " + response.StatusCode);
			}

			var responseStream = response.GetResponseStream();
			if (typeof (T) == typeof (NoResponse) || responseStream == null)
			{
				return null;
			}

			var deserializer = GetSerializer<T>(returnedRoot);
			return (T) deserializer.Deserialize(responseStream);
		}

		private XmlSerializer GetSerializer<T>(string root)
		{
			return _xmlSerializers[new SerializerEntry(root, typeof (T))];
		}

		private HttpWebRequest CreateRequest(Uri uri)
		{
			var request = (HttpWebRequest)WebRequest.Create(uri);
			request.Credentials = CreateCredentials();
			request.Method = "POST";
			request.Proxy = null;
			request.Timeout = _defaultTimeout;

			request.ContentType = "application/xml";

			return request;
		}

		private T Get<T>(string relativeUri, string root = null)
		{
			try
			{
				return DoGet<T>(relativeUri, root);
			}
			catch (WebException wex)
			{
				if (wex.Status == WebExceptionStatus.Timeout)
				{
					throw new TimeoutException("Campfire API call has timed out", wex);
				}
				throw;
			}
		}

		private T DoGet<T>(string relativeUri, string root)
		{
			var client = CreateClient();

			var uri = FormatUri(relativeUri);

			var xml = client.DownloadString(uri);

			var serializer = GetSerializer<T>(root);

			return (T) serializer.Deserialize(new StringReader(xml));
		}

		private Uri FormatUri(string relativeUri)
		{
			var baseUri = new Uri(String.Format("https://{0}.campfirenow.com", _loginInfo.Account));
			var uri = new Uri(baseUri, relativeUri);
			return uri;
		}

		private WebClient CreateClient()
		{
			var client = new WebClientWithTimeout {Credentials = CreateCredentials(), Proxy = null, Timeout = _defaultTimeout};
			return client;
		}

		private NetworkCredential CreateCredentials()
		{
			return new NetworkCredential(_loginInfo.Token, "X");
		}

		private class NoResponse{}

		private class SerializerEntry
		{
			private readonly string _rootElement;
			private readonly Type _type;

			public SerializerEntry(string rootElement, Type type)
			{
				_rootElement = rootElement;
				_type = type;
			}

			public SerializerEntry(Type type)
			{
				_type = type;
			}


			private bool Equals(SerializerEntry other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return Equals(other._rootElement, _rootElement) && Equals(other._type, _type);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof (SerializerEntry)) return false;
				return Equals((SerializerEntry) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return ((_rootElement != null ? _rootElement.GetHashCode() : 0)*397) ^ (_type != null ? _type.GetHashCode() : 0);
				}
			}
		}

		public IDisposable Stream(int id, Action<Message> action, Action<Exception> onError = null)
		{
			return CreateStreamingObservable(id).Subscribe(action, e =>
				{
					if (onError != null)
					{
						onError(e);
					}
				});
		}

		private IObservable<Message> CreateStreamingObservable(int id)
		{
			return RunStream(id).ToObservable()
				.SubscribeOn(Scheduler.ThreadPool)
				.ObserveOn(Scheduler.ThreadPool);
		}

		private IEnumerable<Message> RunStream(int id)
		{
			int secondsToWaitForRetry = 2;
			while(true)
			{
				var uri = String.Format("https://streaming.campfirenow.com/room/{0}/live.json", id);
				var request = CreateRequest(new Uri(uri));
				request.Method = "GET";
				//request.Proxy = new WebProxy("127.0.0.1:8888");

				request.Timeout = -1;

				string credentials = String.Format("{0}:{1}", _loginInfo.Token, "X");
				byte[] bytes = Encoding.ASCII.GetBytes(credentials);
				string base64 = Convert.ToBase64String(bytes);
				string authorization = String.Concat("basic ", base64);
				request.Headers.Add("Authorization", authorization);

				WebResponse response = null;
				try
				{
					response = request.GetResponse();
				}
				catch (WebException e)
				{
					if (e.Status == WebExceptionStatus.Timeout)
					{
						Thread.Sleep(TimeSpan.FromSeconds(secondsToWaitForRetry));
						secondsToWaitForRetry *= 2;
						continue;
					}
					throw;
				}
				secondsToWaitForRetry = 2;
				using (var stream = response.GetResponseStream())
				{
					var streamReader = new StreamReader(stream);
					var str = streamReader.ReadLine();
					if (str == null || str.Trim() == String.Empty)
					{
						continue;
					}

					JsonReader reader = new JsonTextReader(new StringReader(str));

					var serializer = new JsonSerializer();
					var message = serializer.Deserialize<Message>(reader);

					yield return message;
				}
			}

		}
	}


}
