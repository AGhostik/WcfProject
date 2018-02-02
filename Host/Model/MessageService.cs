using System.ServiceModel;
using System.Threading.Tasks;
using NLog;

namespace Host.Model
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        void Ping();

        [OperationContract]
        string GetMessage();

        [OperationContract]
        void SetMessage(string message);
    }

    public class MessageService : IMessageService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageStorage _storage;

        public MessageService(IMessageStorage storage)
        {
            _storage = storage;
        }

        public void Ping()
        {

        }

        public string GetMessage()
        {
            _logger.Info($"GetMessage request");
            Task.Delay(1000).Wait();
            return _storage.GetMessage();
        }

        public void SetMessage(string message)
        {
            _logger.Info($"SetMessage request; {message?.Length}");
            _storage.SetMessage(message);
        }
    }
}