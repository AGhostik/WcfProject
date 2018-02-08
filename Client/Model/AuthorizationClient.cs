using System.ServiceModel;
using System.Threading.Tasks;
using Client.MessageServiceReference;
using NLog;

namespace Client.Model
{
    public class AuthorizationClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private CallbackClient _callback;
        private IMessageService _proxy;

        public async Task PingServer(string url)
        {
            _callback = new CallbackClient();
            var client = new DuplexContractClient(_callback, new WSDualHttpBinding(),
                new EndpointAddress(url + "/MessageService"));
            _proxy = client.ChannelFactory.CreateChannel();

            await _proxy.PingAsync();
        }
    }
}