using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Unity;

namespace Host.Model
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string GetMessage();

        [OperationContract]
        void SetMessage(string message);
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageStorage _storage;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MessageService():this(DependencyFactory.Resolve<IMessageStorage>())
        {
            if (_storage == null)
            {
                _storage = new MessageStorage();
                Debug.WriteLine("IMessageStorage dont resolved by DependencyFactory");
            }
            else
            {
                Debug.WriteLine("IMessageStorage resolved");
            }
        }
        public MessageService(IMessageStorage storage)
        {
            _storage = storage;
        }

        public string GetMessage()
        {
            _logger.Info($"GetMessage request");
            Task.Delay(10000).Wait();
            return _storage.GetMessage();
        }

        public void SetMessage(string message)
        {
            _logger.Info($"SetMessage request; {message?.Length}");
            _storage.SetMessage(message);
        }
    }
}
