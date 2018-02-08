using System;
using Client.MessageServiceReference;

namespace Client.Model
{
    public class CallbackClient : IMessageServiceCallback
    {
        public EventHandler PongEvent;
        public Message Message { get; private set; }

        public void ChatCreated()
        {
        }

        public void OnMessageAdded(Message message)
        {
            Message = message;
        }

        public void Pong()
        {
            PongEvent?.Invoke(this, null);
        }
    }
}