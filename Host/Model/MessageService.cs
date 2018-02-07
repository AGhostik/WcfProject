using System.ServiceModel;
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
        Chat[] GetChats();

        [OperationContract]
        void CreateChat(string name);

        [OperationContract]
        Message[] GetChatMessages(string chatId);

        [OperationContract]
        void AddMessage(string chatId, Message message);
    }

    public interface IMessageCallback
    {
        [OperationContract]
        void Pong();

        [OperationContract]
        void ChatCreated();

        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(Message message);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class MessageService : IMessageService
    {
        private readonly IMessageCallback _callback;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageStorage _storage;

        public MessageService(IMessageStorage storage)
        {
            _storage = storage;
            _callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
        }

        public void Ping()
        {
            _logger.Info($"Ping request");
            _callback.Pong();
        }

        public Chat[] GetChats()
        {
            _logger.Info($"GetMessage request");
            return _storage.GetChats().ToArray();
        }

        public void CreateChat(string name)
        {
            _logger.Info($"CreateChat request");
            _storage.CreateChat(name);
            _callback.ChatCreated();
        }

        public void AddMessage(string chatId, Message message)
        {
            _logger.Info($"SetMessage request");
            _storage.AddMessage(chatId, message);
            _callback.OnMessageAdded(message);
        }

        public Message[] GetChatMessages(string chatId)
        {
            _logger.Info($"GetChatMessages request");
            return _storage.GetChatMessages(chatId).ToArray();
        }
    }
}