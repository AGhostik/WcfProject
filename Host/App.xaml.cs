using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows;
using Host.Model;
using Host.Model.Storages;
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
            container.RegisterType<IMessageStorage, XmlStorage>();

            container.RegisterInstance(new StorageSettings
            {
                MessagesLimit = 0,
                UsersLimit = 5,
                ChatsLimit = 8
            });
            container.RegisterInstance<ServiceHost>(CreateServiceHost(container));

            var mainWindows = container.Resolve<MainWindow>();
            mainWindows.Show();

            _logger.Info("App init");
        }

        private UnityServiceHost CreateServiceHost(IUnityContainer container)
        {
            var unityServiceHost =
                new UnityServiceHost(container, typeof(MessageService), new Uri("http://localhost:8080"));
            var binding = new WSDualHttpBinding
            {
                ReceiveTimeout = new TimeSpan(0, 0, 30),
                CloseTimeout = new TimeSpan(0, 0, 30),
                OpenTimeout = new TimeSpan(0, 0, 30),
                SendTimeout = new TimeSpan(0, 0, 30)
            };
            unityServiceHost.AddServiceEndpoint(typeof(IMessageService), binding, "MessageService");
            var sdb = unityServiceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;
            return unityServiceHost;
        }
    }
}