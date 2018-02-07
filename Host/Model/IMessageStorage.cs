using System.Collections.Generic;
using Host.Model.Data;

namespace Host.Model
{
    public interface IMessageStorage
    {
        void AddMessage(string chatId, Message message);
        void CreateChat(string name);
        List<Message> GetChatMessages(string chatId);
        List<Chat> GetChats();
    }
}