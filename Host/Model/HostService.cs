using System.ServiceModel;

namespace Host.Model
{
    public class HostService : IHostService
    {
        private readonly ServiceHost _host;

        public HostService(ServiceHost host)
        {
            _host = host;
        }

        public void Open()
        {
            _host?.Open();
        }

        public void Close()
        {
            _host?.Close();
        }
    }
}