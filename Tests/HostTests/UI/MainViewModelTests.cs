using System;
using Host.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HostTests.UI
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Start_ButtonDisabled()
        {
            var viewModel = new MainViewModel(new FakeHostService());

            viewModel.StartService.Execute(null);

            Assert.IsFalse(viewModel.StartEnabled);
        }
    }
}
