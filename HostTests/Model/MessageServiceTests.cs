using System;
using Host.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HostTests.Model
{
    [TestClass]
    public class MessageServiceTests
    {
        private const string Message = "test";

        [TestMethod]
        public void GetMessage()
        {
            var storage = new FakeMessageStorage();
            storage.SetMessage(Message);
            var service = new MessageService(storage);
            var queryResult = service.GetMessage();

            Assert.AreEqual(queryResult, Message);
        }

        [TestMethod]
        public void SetMessage()
        {
            var storage = new FakeMessageStorage();
            var service = new MessageService(storage);
            service.SetMessage(Message);

            Assert.AreEqual(storage.GetMessage(), Message);
        }
    }
}
