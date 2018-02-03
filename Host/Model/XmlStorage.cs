using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Host.Model
{
    public class XmlStorage : IMessageStorage
    {
        string chatId = "1";
        private const string FilePath = "Storage.xml";

        public XmlStorage(StorageSettings storageSettings)
        {
            if (!File.Exists(FilePath))
            {
                var document = new XDocument(new XElement("chats"));
                document.Save(FilePath);
                CreateChat();
            }
        }
        
        public string GetMessage()
        {
            var document = XDocument.Load(FilePath);
            var lastMessage = document.Element("chats")?.Elements("chat").Last(chat => chat.Attribute("id")?.Value == chatId).Elements("message").Last();
            return lastMessage?.Value ?? string.Empty;
        }

        public void SetMessage(string message)
        {
            var document = XDocument.Load(FilePath);
            var currentChat = document.Element("chats")?.Elements("chat").Last(chat => chat.Attribute("id")?.Value == chatId);
            var newMessage = new XElement("message", new XAttribute("time", DateTime.Now), new XAttribute("author", "anon"))
            {
                Value = message
            };
            currentChat?.Add(newMessage);
            document.Save(FilePath);
        }

        public void CreateChat()
        {
            var document = XDocument.Load(FilePath);
            var newChat = new XElement($"chat", new XAttribute("id", chatId), new XAttribute("name", "Super Secret Chat"));
            document.Element("chats")?.Add(newChat);
        }
    }
}
