using System.ServiceModel;
using System.Threading.Tasks;

namespace Host.Model
{
    public class HostService : IHostService
    {
        private readonly ServiceHost _host;

        public HostService(ServiceHost host)
        {
            _host = host;
        }

        public async Task Open()
        {
            await Task.Factory.StartNew(() => { _host?.Open(); });
        }

        public async Task Close()
        {
            await Task.Factory.StartNew(() => { _host?.Close(); });
        }
    }
}