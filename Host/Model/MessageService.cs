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
        bool Subscribe();

        [OperationContract]
        List<Chat> GetChats();

        [OperationContract]
        void CreateChat();

        [OperationContract(IsOneWay = true)]
        void AddMessage(string chatId, Message message);
    }
    
    public interface IMessageCallback
    { 
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(Message message);
    }

    public class MessageService : IMessageService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageStorage _storage;

        public MessageService(IMessageStorage storage)
        {
            _storage = storage;
        }

        private static readonly List<IMessageCallback> Subscribers = new List<IMessageCallback>();

        public bool Subscribe()
        {
            try
            {
                var callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                //callback.id = id;
                if (!Subscribers.Contains(callback))
                    Subscribers.Add(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Ping()
        {

        }

        public List<Chat> GetChats()
        {
            _logger.Info($"GetMessage request");
            Task.Delay(1000).Wait();
            return _storage.GetChats();
        }

        public void CreateChat()
        {
            _storage.CreateChat();
        }

        public void AddMessage(string chatId, Message message)
        {
            _logger.Info($"SetMessage request");
            _storage.AddMessage(chatId, message);

            Subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    callback.OnMessageAdded(message);
                }
                else
                {
                    Subscribers.Remove(callback);
                }
            });
        }
    }
}