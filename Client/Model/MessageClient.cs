using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.MessageServiceReference;
using NLog;

namespace Client.Model
{
    public class MessageClient : IDisposable
    {
        private readonly CallbackClient _callback;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageService _proxy;
        
        public EventHandler<MessageArg> MessageAddedEvent;

        public string Username { get; }

        public MessageClient(IMessageService proxy, CallbackClient callback, string username)
        {
            _callback = callback;
            _proxy = proxy;
            _proxy.Subscribe();
            _callback.MessageAdded += (sender, arg) =>
            {
                MessageAddedEvent?.Invoke(sender, arg);
            };
            Username = username;
        }

        public async Task PingServer()
        {
            await _proxy.PingAsync();
        }

        public async Task AddMessage(string chatId, string message)
        {
            var newMessage = new Message()
            {
                Author = Username,
                Content = message
            };
            await _proxy.AddMessageAsync(chatId, newMessage);
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

        public void Dispose()
        {
            _proxy.Unsubscribe();
        }
    }
}