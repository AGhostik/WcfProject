using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Unity.Wcf;

namespace Host.Model
{
    public class HostService : IHostService
    {
        private ServiceHost _host;

        public HostService(ServiceHost host)
        {
            _init(host);
        }

        public void Open()
        {
            _host?.Open();
        }

        public void Close()
        {
            _host?.Close();
        }

        private void _init(ServiceHost host)
        {
            _host = host; //new ServiceHost(typeof(MessageService), new Uri("http://localhost:8080/MessageService"));

            var sdb = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;

        }
    }
}