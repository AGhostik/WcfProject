namespace Host.Model
{
    public class MessageStorage : IMessageStorage
    {
        private static string _message;
        private readonly object _syncObject = new object();

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