using System;
using System.ServiceModel;
using Host.Model.Data;
using Host.Model.Storages;
using NLog;

namespace Host.Model
{
    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IMessageService
    {
        [OperationContract]
        void AddMessage(string chatId, Message message);
        
        [OperationContract]
        void AddUser(string name, string password);

        [OperationContract]
        void CreateChat(string name);
        
        [OperationContract]
        Message[] GetChatMessages(string chatId);

        [OperationContract]
        Chat[] GetChats();
        
        [OperationContract]
        void Ping();

        [OperationContract]
        bool UserExist(string id);
    }

    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void Pong();

        [OperationContract(IsOneWay = true)]
        void ChatCreated();

        [OperationContract(IsOneWay = true)]
        void UserCreated(bool isFailed);

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

        public bool UserExist(string id)
        {
            _logger.Info($"User exist request; id:{id}");
            return _storage.UserExist(id);
        }

        public Chat[] GetChats()
        {
            _logger.Info($"GetMessage request");
            return _storage.GetChats().ToArray();
        }

        public void AddUser(string name, string password)
        {
            _logger.Info($"AddUser request; name:{name}");
            try
            {
                _storage.AddUser(name, password, "default");
                _callback.UserCreated(false);
            }
            catch (UserNameException e)
            {
                _logger.Error(e.Message);
                _callback.UserCreated(true);
            }
            catch (UserLimitException e)
            {
                _logger.Error(e.Message);
                _callback.UserCreated(true);
            }
        }

        public void CreateChat(string name)
        {
            _logger.Info($"CreateChat request; name:{name}");
            _storage.CreateChat(name);
            _callback.ChatCreated();
        }

        public void AddMessage(string chatId, Message message)
        {
            _logger.Info($"SetMessage request; chatId:{chatId}, Message:{Environment.NewLine}{message}");
            _storage.AddMessage(chatId, message);
            _callback.OnMessageAdded(message);
        }

        public Message[] GetChatMessages(string chatId)
        {
            _logger.Info($"GetChatMessages request; chatId:{chatId}");
            return _storage.GetChatMessages(chatId).ToArray();
        }
    }
}