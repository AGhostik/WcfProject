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

        public async Task<MessageClient> Login(string url, string username, string password)
        {
            _callback = new CallbackClient();
            var client = new DuplexContractClient(_callback, new WSDualHttpBinding(),
                new EndpointAddress(url + "/MessageService"));
            _proxy = client.ChannelFactory.CreateChannel();

            await _proxy.PingAsync();

            return new MessageClient(_proxy, _callback, username);
        }
    }
}