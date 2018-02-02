using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receiver.UI;

namespace ReceiverTests.UI
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Update_TextChanged()
        {
            var viewModel = new MainViewModel(new FakeClient());
            viewModel.Update.Execute(null);
            Assert.AreEqual(viewModel.Text, "message");
        }
    }
}
