using ClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverTests.UI
{
    public class FakeClient : IClient
    {
        public void Close()
        {
            
        }

        public async Task<string> GetMessageAsync()
        {
            return "message";
        }

        public Task Init(string url)
        {
            return Task.Delay(0);
        }

        public Task SetMessageAsync(string message)
        {
            return Task.Delay(0);
        }
    }
}
