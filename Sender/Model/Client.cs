using System;
using System.ServiceModel;
using System.Threading.Tasks;
using NLog;
using Sender.MessageServiceReference;

namespace Sender.Model
{
    public class Client : IClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private MessageServiceClient _client;

        public Client()
        {
            
        }

        public void Init(string url)
        {
            _client = new MessageServiceClient();
            _client.Endpoint.Address = new EndpointAddress(url + "/MessageService");
            _client.Open();
            _logger.Info($"Connection created; {url}");
        }

        public void Close()
        {
            _client?.Close();
        }

        public async Task SetMessageAsync(string message)
        {
            if (_client == null)
            {
                _logger.Error("Client not initialized");
                throw new NullReferenceException("Client not initialized");
            }
            await _client.SetMessageAsync(message);
            _logger.Info($"Set message; length = {message?.Length}");
        }
    }
}
