using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Host.Model.Data;

namespace Host.Model.Storages
{
    public class XmlStorage : IMessageStorage
    {
        private const string FilePath = "Storage.xml";
        private readonly StorageSettings _storageSettings;
        private readonly object _syncObject = new object();

        public XmlStorage(StorageSettings storageSettings)
        {
            if (!File.Exists(FilePath))
            {
                _createXml();
                CreateChat("Super Secret Chat");
            }

            _storageSettings = storageSettings;
        }

        public bool UserExist(string id)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var users = _getAllUsers(document).ToArray();

                return users.Any(user => user.Attribute(XNames.Id)?.Value == id);
            }
        }

        public void AddUser(string name, string password, string group)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                if (_storageSettings.UsersLimit > 0 && _getAllUsers(document).Count() >= _storageSettings.UsersLimit)
                {
                    throw new UserLimitException(name);
                }
                var users = _getAllUsers(document);
                if (users.Any(user => user.Name == name))
                {
                    throw new UserNameException(name);
                }

                var newId = _getNewUserId(document);
                var newUser = new XElement(XNames.User, new XAttribute(XNames.Id, newId),
                    new XAttribute(XNames.Name, name), new XAttribute(XNames.Password, password),
                    new XAttribute(XNames.Group, group));
                document.Element(XNames.Users)?.Add(newUser);
                document.Save(FilePath);
            }
        }

        public void AddMessage(string chatId, Message message)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var currentChat = _getChat(chatId, document);
                var newMessage = new XElement(XNames.Message, new XAttribute(XNames.Time, DateTime.Now),
                    new XAttribute(XNames.Author, message.Author))
                {
                    Value = message.Content
                };
                if (_storageSettings.MessagesLimit > 0 &&
                    currentChat?.Elements(XNames.Message).Count() >= _storageSettings.MessagesLimit)
                    currentChat.Elements(XNames.Message).First().Remove();
                currentChat?.Add(newMessage);
                document.Save(FilePath);
            }
        }

        public void RemoveMessage(string chatId)
        {
            throw new NotImplementedException();
        }

        public List<Chat> GetChats()
        {
            lock (_syncObject)
            {
                var chatList = new List<Chat>();
                var document = XDocument.Load(FilePath);
                var chats = _getAllChats(document);
                if (chats == null) return chatList;
                chatList.AddRange(chats.Select(chat =>
                    new Chat {Id = chat.Attribute(XNames.Id)?.Value, Name = chat.Attribute(XNames.Name)?.Value}));
                return chatList;
            }
        }

        public void CreateChat(string name)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var newId = _getNewChatId(document);
                var newChat = new XElement(XNames.Chat, new XAttribute(XNames.Id, newId),
                    new XAttribute(XNames.Name, name));
                document.Element(XNames.Root)?.Element(XNames.Chats)?.Add(newChat);
                document.Save(FilePath);
            }
        }

        public List<Message> GetChatMessages(string id)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var currentChat = _getChat(id, document);
                var messages = currentChat?.Elements(XNames.Message);
                if (messages == null) throw new Exception("No messages");

                return messages.Select(message => new Message
                    {
                        Author = message.Attribute(XNames.Author)?.Value,
                        Content = message.Value,
                        Time = DateTime.Parse(message.Attribute(XNames.Time)?.Value)
                    })
                    .ToList();
            }
        }

        private void _createXml()
        {
            var document = new XDocument();
            var root = new XElement(XNames.Root);
            root.Add(new XElement(XNames.Chats));
            root.Add(new XElement(XNames.Users));
            document.Add(root);
            document.Save(FilePath);
        }

        private static IEnumerable<XElement> _getAllChats(XContainer document)
        {
            return document.Element(XNames.Root)?.Element(XNames.Chats)?.Elements(XNames.Chat);
        }

        private static XElement _getChat(string id, XContainer document)
        {
            var chats = _getAllChats(document);
            return chats?.Last(chat => chat.Attribute(XNames.Id)?.Value == id);
        }

        private static IEnumerable<XElement> _getAllUsers(XContainer document)
        {
            return document.Element(XNames.Root)?.Element(XNames.Users)?.Elements(XNames.User);
        }

        private string _getNewUserId(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var users = _getAllUsers(document).ToArray();

            if (users == null) throw new Exception("Userss not exist. Cant search available user Id");

            for (var i = 0; i < users.Length; i++)
                if (users[i].Attribute(XNames.Id)?.Value != i.ToString())
                    return i.ToString();

            return $"{users.Length + 1}";
        }

        private string _getNewChatId(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var chats = _getAllChats(document).ToArray();

            if (chats == null) throw new Exception("Chats not exist. Cant search available chat Id");

            for (var i = 0; i < chats.Length; i++)
                if (chats[i].Attribute(XNames.Id)?.Value != i.ToString())
                    return i.ToString();

            return $"{chats.Length + 1}";
        }
    }
}