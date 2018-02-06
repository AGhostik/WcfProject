using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Host.Model.Data;
using NLog;

namespace Host.Model
{
    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IMessageService
    {
        [OperationContract]
        void Ping();

        [OperationContract]
        void GetChats();

        [OperationContract]
        void CreateChat();

        [OperationContract]
        void AddMessage(string chatId, Message message);
    }
    
    public interface IMessageCallback
    { 
        [OperationContract]
        void Pong();

        [OperationContract]
        void ChatsReceived(List<Chat> chats);

        [OperationContract]
        void ChatCreated();

        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(Message message);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class MessageService : IMessageService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageCallback _callback;
        private readonly IMessageStorage _storage;

        public MessageService(IMessageStorage storage)
        {
            _storage = storage;
            _callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
        }
        
        public void Ping()
        {
            _callback.Pong();
        }

        public void GetChats()
        {
            _logger.Info($"GetMessage request");
            Task.Delay(1000).Wait();
            _callback.ChatsReceived(_storage.GetChats());
        }

        public void CreateChat()
        {
            _storage.CreateChat();
            _callback.ChatCreated();
        }

        public void AddMessage(string chatId, Message message)
        {
            _logger.Info($"SetMessage request");
            _storage.AddMessage(chatId, message);
            _callback.OnMessageAdded(message);
        }
    }
}