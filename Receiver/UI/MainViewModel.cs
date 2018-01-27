using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Receiver.MessageServiceReference;

namespace Receiver.UI
{
    public class MainViewModel : ViewModelBase
    {
        private string _text;
        public EventHandler ConnectionError;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public RelayCommand Update { get; set; }

        public MainViewModel()
        {
            _init();
        }

        private void _init()
        {
            Update = new RelayCommand(() =>
            {
                try
                {
                    var messageService = new MessageServiceClient();
                    Text = messageService.GetMessage();
                    _logger.Info($"Get message; length = {Text.Length}");
                }
                catch (EndpointNotFoundException)
                {
                    _logger.Error("EndpointNotFoundException");
                    ConnectionError?.Invoke(this, null);
                }
            });

            _logger.Info("Hello! Nice day for checking mail!");
        }
    }
}
