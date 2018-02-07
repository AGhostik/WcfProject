using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using Client.MessageServiceReference;
using Client.UI;
using NLog;
using Xceed.Wpf.DataGrid;
using Message = Client.MessageServiceReference.Message;

namespace Client.Model
{
    public class MessageClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageService _proxy;
        private readonly CallbackClient _callback;
        private readonly ConnectionSettings _connectionSettings;

        public MessageClient(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _callback = new CallbackClient();
            var client = new DuplexContractClient(_callback, new WSDualHttpBinding(), new EndpointAddress(connectionSettings.Url + "/MessageService"));
            _proxy = client.ChannelFactory.CreateChannel();
            _logger.Info("Proxy created");
        }

        public async Task PingServer()
        {
            await _proxy.PingAsync();
        }

        public async Task<Message> AddMessage(string message)
        {
            var newMessage = new Message
            {
                Author = _connectionSettings.Username,
                Content = message
            };
            await _proxy.AddMessageAsync("0", newMessage);
            return _callback.Message;
        }

        public async Task GetChats()
        {
            await _proxy.GetChatsAsync();
        }
    }

    public class CallbackClient : IMessageServiceCallback
    {
        public Message Message { get; private set; }

        public void Pong()
        {
            
        }

        public void ChatsReceived(Chat[] chats)
        {
            
        }

        public void ChatCreated()
        {
            
        }

        public void OnMessageAdded(Message message)
        {
            Message = message;
        }
    }
}