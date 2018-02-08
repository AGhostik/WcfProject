using System.ServiceModel;
using System.Threading.Tasks;
using Client.MessageServiceReference;
using Client.UI;
using NLog;

namespace Client.Model
{
    public class MessageClient
    {
        private readonly CallbackClient _callback;
        private readonly ConnectionSettings _connectionSettings;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageService _proxy;

        public MessageClient(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _callback = new CallbackClient();
            var client = new DuplexContractClient(_callback, new WSDualHttpBinding(),
                new EndpointAddress(connectionSettings.Url + "/MessageService"));
            _proxy = client.ChannelFactory.CreateChannel();
            //_logger.Info("Proxy created");
        }

        public async Task PingServer()
        {
            await _proxy.PingAsync();
        }

        public async Task<Message> AddMessage(string chatId, string message)
        {
            var newMessage = new Message
            {
                Author = _connectionSettings.Username,
                Content = message
            };
            await _proxy.AddMessageAsync(chatId, newMessage);
            return _callback.Message;
        }

        public async Task<Chat[]> GetChats()
        {
            return await _proxy.GetChatsAsync();
        }

        public Chat[] GetChatsSync()
        {
            return _proxy.GetChats();
        }

        public async Task<Message[]> GetChatMessages(string chatId)
        {
            return await _proxy.GetChatMessagesAsync(chatId);
        }
    }
}