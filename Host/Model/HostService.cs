using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Host.Model
{
    public class HostService
    {
        private ServiceHost _host;

        //public EventHandler HostClosed;
        //public EventHandler HostFaulted;
        //public EventHandler HostOpened;

        public void Open()
        {
            _host = new ServiceHost(typeof(MessageService), new Uri("http://localhost:8080/MessageService"));
            _host.AddServiceEndpoint(typeof(IMessageService), new WSHttpBinding(), "MessageService");

            var sdb = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;

            //_host.Opened += (sender, args) => { HostOpened?.Invoke(this, null); };
            //_host.Closed += (sender, args) => { HostClosed?.Invoke(this, null); };
            //_host.Faulted += (sender, args) => { HostFaulted?.Invoke(this, null); };

            _host.Open();
        }

        public void Close()
        {
            _host.Close();
        }
    }
}