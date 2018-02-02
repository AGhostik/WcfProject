using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sender.UI;

namespace SenderTests.UI
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Send_TextChanged()
        {
            var client = new FakeClient();
            var viewModel = new MainViewModel(client);
            viewModel.Send.Execute(null);
            Assert.AreEqual(viewModel.Text, client.GetMessageAsync().Result);
        }
    }
}
