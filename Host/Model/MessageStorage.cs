namespace Host.Model
{
    public class MessageStorage
    {
        private static string _message;

        public void SetMessage(string message)
        {
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }
    }
}
