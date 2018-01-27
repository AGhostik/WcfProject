using System;
using System.ServiceModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Receiver.MessageServiceReference;

namespace Receiver.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _buttonEnabled;
        private bool _isBusy;
        private string _text;
        public EventHandler ConnectionError;

        public MainViewModel()
        {
            _init();
        }

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public RelayCommand Update { get; set; }

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => Set(ref _buttonEnabled, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private void _init()
        {
            ButtonEnabled = true;
            IsBusy = false;

            Update = new RelayCommand(async () =>
            {
                ButtonEnabled = false;
                IsBusy = true;
                try
                {
                    var messageService = new MessageServiceClient();
                    Text = await messageService.GetMessageAsync();
                    _logger.Info($"Get message; length = {Text?.Length}");
                }
                catch (EndpointNotFoundException)
                {
                    _logger.Error("EndpointNotFoundException");
                    ConnectionError?.Invoke(this, null);
                }
                finally
                {
                    ButtonEnabled = true;
                    IsBusy = false;
                }
            });

            _logger.Info("Hello! Nice day for checking mail!");
        }
    }
}