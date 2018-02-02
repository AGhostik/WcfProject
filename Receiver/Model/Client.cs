using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Receiver.MessageServiceReference;

namespace Receiver.Model
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
    }
}
