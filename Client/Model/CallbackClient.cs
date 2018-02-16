using System;
using System.ServiceModel;
using Client.MessageServiceReference;

namespace Client.Model
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class CallbackClient : IMessageServiceCallback
    {
        public EventHandler PongEvent;
        public EventHandler<MessageArg> MessageAdded;
        //public Message Message { get; private set; }

        public void ChatCreated()
        {
        }

        public void UserCreated(bool isFailed)
        {
            
        }

        public void OnMessageAdded(Message message)
        {
            MessageAdded?.Invoke(this, new MessageArg(message));
        }

        public void Pong()
        {
            PongEvent?.Invoke(this, null);
        }
    }

    public class MessageArg : EventArgs
    {
        public MessageArg(Message message)
        {
            Message = message;
        }
        public Message Message { get; set; }
    }
}