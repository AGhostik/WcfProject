using System.Collections.Generic;
using Host.Model.Data;

namespace Host.Model.Storages
{
    public interface IMessageStorage
    {
        void AddMessage(string chatId, Message message);
        void AddUser(string name, string password, string group);
        void CreateChat(string name);
        List<Message> GetChatMessages(string id);
        List<Chat> GetChats();
        bool UserExist(string id);
        void RemoveMessage(string chatId);
    }
}