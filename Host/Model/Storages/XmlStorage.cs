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
                CreateChat();
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

        public void CreateChat()
        {
            lock (_syncObject)
            {
                var document = XDocument.Load(FilePath);
                var newId = _getNewChatId(document);
                var newChat = new XElement($"chat", new XAttribute("id", newId),
                    new XAttribute("name", "Super Secret Chat"));
                document.Element("chats")?.Add(newChat);
            }
        }

        private string _getNewChatId(XDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var chats = document.Element("chats")?.Elements("chat").ToArray();

            if (chats == null)
            {
                throw new Exception("Chats not exist. Cant search available chatId");
            }

            for (var i = 0; i < chats.Length; i++)
            {
                if (chats[i].Attribute("id")?.Value != i.ToString())
                {
                    return i.ToString();
                }
            }

            return $"{chats.Length + 1}";
        }
    }
}