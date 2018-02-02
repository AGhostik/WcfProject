using System;
using System.ServiceModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Sender.MessageServiceReference;

namespace Sender.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _buttonEnabled;
        private bool _isBusy;
        public EventHandler ConnectionError;

        public MainViewModel()
        {
            _init();
        }

        public string Text { get; set; }
        public RelayCommand Send { get; set; }

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

            Send = new RelayCommand(async () =>
            {
                ButtonEnabled = false;
                IsBusy = true;
                try
                {
                    var messageService = new MessageServiceClient();
                    await messageService.SetMessageAsync(Text);
                    _logger.Info($"Send message; length = {Text?.Length}");
                }
                catch (EndpointNotFoundException)
                {
                    _logger.Error("EndpointNotFoundException");
                    ConnectionError?.Invoke(this, null);
                }
                catch (FaultException)
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

            _logger.Info("Im ready");
        }
    }
}