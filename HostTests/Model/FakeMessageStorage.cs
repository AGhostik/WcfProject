using Host.Model;

namespace HostTests.Model
{
    public class FakeMessageStorage : IMessageStorage
    {
        private string _message;

        public string GetMessage()
        {
            return _message;
        }

        public void SetMessage(string message)
        {
            _message = message;
        }
    }
}
