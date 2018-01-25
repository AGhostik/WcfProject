namespace Host.Model
{
    public class MessageStorage : IMessageStorage
    {
        private readonly object _syncObject = new object();
        private static string _message;

        public void SetMessage(string message)
        {
            lock (_syncObject)
            {
                _message = message;
            }
        }

        public string GetMessage()
        {
            lock (_syncObject)
            {
                return _message;
            }
        }
    }
}