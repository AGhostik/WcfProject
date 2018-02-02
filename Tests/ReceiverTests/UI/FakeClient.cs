using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Receiver.Model;

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

        public void Init(string url)
        {
            
        }
    }
}
