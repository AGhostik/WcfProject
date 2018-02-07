using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using Client.MessageServiceReference;
using Client.UI;
using NLog;
using Xceed.Wpf.DataGrid;

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
            await _proxy.GetChatsAsync();
            return _callback.Chats;
        }

        public async Task<Message[]> GetChatMessages(string chatId)
        {
            await _proxy.GetChatMessagesAsync(chatId);
            return _callback.Messages;
        }
    }

    public class CallbackClient : IMessageServiceCallback
    {
        public Message Message { get; private set; }
        public Message[] Messages { get; private set; }
        public Chat[] Chats { get; private set; }

        public void Pong()
        {
            
        }

        public void ChatsReceived(Chat[] chats)
        {
            Chats = chats;
        }

        public void ChatCreated()
        {
            
        }

        public void OnMessageAdded(Message message)
        {
            Message = message;
        }

        public void ChatMessagesReceived(Message[] messages)
        {
            Messages = messages;
        }
    }
}