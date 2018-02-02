using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using Host.Model;
using Host.UI;
using NLog;
using Unity;

namespace Host
{
    public partial class App : Application
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();

           

            container.RegisterType<IHostService, HostService>();
            container.RegisterType<IMessageStorage, MessageStorage>();

            var mainWindows = container.Resolve<MainWindow>();
            mainWindows.Show();

            _logger.Info("App init");
        }
    }
}
