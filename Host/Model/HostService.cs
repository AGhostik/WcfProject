using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Host.Model
{
    public class HostService : IHostService
    {
        private ServiceHost _host;

        public void Open()
        {
            _host = new ServiceHost(typeof(MessageService), new Uri("http://localhost:8080/MessageService"));
            _host.AddServiceEndpoint(typeof(IMessageService), new WSHttpBinding(), "MessageService");

            var sdb = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;

            _host.Open();
        }

        public void Close()
        {
            _host.Close();
        }
    }
}