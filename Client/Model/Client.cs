using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.MessageServiceReference;
using NLog;

namespace Client.Model
{
    public class Client
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private MessageServiceClient _client;

        public async Task Init(string url)
        {
            var callback = new MyCallback();
            _client = new MessageServiceClient(new InstanceContext(callback));
            _client.Endpoint.Address = new EndpointAddress(url + "/MessageService");
            _client.Open();
            await _client.PingAsync();
            _logger.Info($"Connection created; {url}");
        }

        public async Task AddMessage(Message message)
        {
            await _client.AddMessageAsync("0", message);
        }

        public async Task<Chat[]> GetChats()
        {
            return await _client.GetChatsAsync();
        }
    }

    public class MyCallback : IMessageServiceCallback
    {
        public void OnMessageAdded(Message message)
        {
            throw new NotImplementedException();
        }
    }
}