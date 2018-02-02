using ClientLibrary.Model;
using System.Threading.Tasks;

namespace SenderTests.UI
{
    public class FakeClient : IClient
    {
        public void Close()
        {
            
        }

        private string _message;

        public async Task<string> GetMessageAsync()
        {
            return _message;
        }

        public Task Init(string url)
        {
            return Task.Delay(0);
        }

        public Task SetMessageAsync(string message)
        {
            _message = message;
            return Task.Delay(0);
        }
    }
}
