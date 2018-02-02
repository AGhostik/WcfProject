using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Windows;
using Host.Model;
using Host.UI;
using NLog;
using Unity;
using Unity.Wcf;

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
            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IMessageStorage, MessageStorage>();
            
            container.RegisterInstance<ServiceHost>(CreateServiceHost(container));

            var mainWindows = container.Resolve<MainWindow>();
            mainWindows.Show();

            _logger.Info("App init");
        }

        private UnityServiceHost CreateServiceHost(IUnityContainer container)
        {
            var unityServiceHost = new UnityServiceHost(container, typeof(MessageService), new Uri("http://localhost:8080"));
            unityServiceHost.AddServiceEndpoint(typeof(IMessageService), new BasicHttpBinding(), "MessageService");
            var sdb = unityServiceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;
            return unityServiceHost;
        }
    }
}
