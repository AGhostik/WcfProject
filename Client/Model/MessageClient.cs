using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using Client.MessageServiceReference;
using NLog;
using Message = Client.MessageServiceReference.Message;

namespace Client.Model
{
    public class MessageClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageService _proxy;
        private readonly CallbackClient _callback;

        public MessageClient()
        {
            _callback = new CallbackClient();
            var client = new DuplexContractClient(_callback, new WSDualHttpBinding(), new EndpointAddress("http://127.0.0.1:8080/MessageService"));
            _proxy = client.ChannelFactory.CreateChannel();
            _logger.Info("Proxy created");
        }

        public async Task PingServer()
        {
            await _proxy.PingAsync();
        }

        public async Task<Message> AddMessage(Message message)
        {
            await _proxy.AddMessageAsync("0", message);
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