using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Host.Model
{
    public class HostService : IHostService
    {
        private readonly ServiceHost _host;

        public HostService()
        {
            _host = new ServiceHost(typeof(MessageService),  new Uri("http://localhost:8080/MessageService"));

            _host.AddServiceEndpoint(typeof(IMessageService), new WSHttpBinding(), "MessageService");

            //// Enable metadata exchange
            //var smb = new ServiceMetadataBehavior() { HttpGetEnabled = true };
            //_host.Description.Behaviors.Add(smb);

            // Enable exeption details
            var sdb = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;
        }

        public void Open()
        {
            _host.Open();
        }

        public void Close()
        {
            _host.Close();
        }
    }
}
