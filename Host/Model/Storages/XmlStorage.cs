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
        private readonly object _syncObject = new object();

        public XmlStorage(StorageSettings storageSettings)
        {
            if (!File.Exists(FilePath))
            {
                var document = new XDocument(new XElement("chats"));
                document.Save(FilePath);
                CreateChat("Super Secret Chat");
            }
        }

        public void AddMessage(string chatId, Message message)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var currentChat = document.Element("chats")?.Elements("chat")
                    .Last(chat => chat.Attribute("id")?.Value == chatId);
                var newMessage = new XElement("message", new XAttribute("time", DateTime.Now),
                    new XAttribute("author", message.Author))
                {
                    Value = message.Content
                };
                currentChat?.Add(newMessage);
                document.Save(FilePath);
            }
        }

        public List<Chat> GetChats()
        {
            lock (_syncObject)
            {
                var chatList = new List<Chat>();
                var document = XDocument.Load(FilePath);
                var chats = document.Element("chats")?.Elements("chat");
                if (chats == null) return chatList;
                chatList.AddRange(chats.Select(chat =>
                    new Chat {Id = chat.Attribute("id")?.Value, Name = chat.Attribute("name")?.Value}));
                return chatList;
            }
        }

        public void CreateChat(string name)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var newId = _getNewChatId(document);
                var newChat = new XElement($"chat", new XAttribute("id", newId),
                    new XAttribute("name", name));
                document.Element("chats")?.Add(newChat);
            }
        }

        public List<Message> GetChatMessages(string chatId)
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var currentChat = document.Element("chats")?.Elements("chat")
                    .Last(chat => chat.Attribute("id")?.Value == chatId);
                var messages = currentChat?.Elements("message");
                if (messages == null) throw new Exception("No messages");

                return messages.Select(message => new Message
                    {
                        Author = message.Attribute("author")?.Value,
                        Content = message.Value,
                        Time = DateTime.Parse(message.Attribute("time")?.Value)
                    })
                    .ToList();
            }
        }

        private string _getNewChatId(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var chats = document.Element("chats")?.Elements("chat").ToArray();

            if (chats == null) throw new Exception("Chats not exist. Cant search available chatId");

            for (var i = 0; i < chats.Length; i++)
                if (chats[i].Attribute("id")?.Value != i.ToString())
                    return i.ToString();

            return $"{chats.Length + 1}";
        }
    }
}