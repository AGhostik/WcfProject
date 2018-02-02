using System;
using System.ServiceModel;
using System.Threading.Tasks;
using ClientLibrary.MessageServiceReference;
using NLog;

namespace ClientLibrary.Model
{
    public class Client : IClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private MessageServiceClient _client;

        public Client()
        {
            
        }

        public async Task Init(string url)
        {
            _client = new MessageServiceClient();
            _client.Endpoint.Address = new EndpointAddress(url + "/MessageService");
            _client.Open();
            await _client.PingAsync();
            _logger.Info($"Connection created; {url}");
        }

        public void Close()
        {
            _client?.Close();
        }

        public async Task<string> GetMessageAsync()
        {
            if (_client == null)
            {
                _logger.Error("Client not initialized");
                throw new NullReferenceException("Client not initialized");
            }
            var text = await _client.GetMessageAsync();
            _logger.Info($"Get message; length = {text?.Length}");
            return text;
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
