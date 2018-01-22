using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
        private readonly MessageStorage _storage;

        public MessageService()
        {
            _storage = new MessageStorage();
        }

        public string GetMessage()
        {
            return _storage.GetMessage();
        }

        public void SetMessage(string message)
        {
            _storage.SetMessage(message);
        }
    }
}
