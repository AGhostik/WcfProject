using System;
using System.ServiceModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Sender.MessageServiceReference;
using Sender.Model;

namespace Sender.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IClient _client;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private ConnectionButtonState _buttonState;
        private bool _isBusy;

        private bool _sendSendButtonEnabled;
        private bool _urlFieldEnabled;
        public EventHandler ConnectionError;
        public EventHandler ServerError;

        public MainViewModel(IClient client)
        {
            _client = client;
            _init();
        }

        public string Url { get; set; }

        public RelayCommand Connect { get; set; }

        public ConnectionButtonState ButtonState
        {
            get => _buttonState;
            set => Set(ref _buttonState, value);
        }

        public bool UrlFieldEnabled
        {
            get => _urlFieldEnabled;
            set => Set(ref _urlFieldEnabled, value);
        }

        public string Text { get; set; }
        public RelayCommand Send { get; set; }

        public bool SendButtonEnabled
        {
            get => _sendSendButtonEnabled;
            set => Set(ref _sendSendButtonEnabled, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private void _init()
        {
            Url = "http://127.0.0.1:8080/";
            SendButtonEnabled = false;
            UrlFieldEnabled = true;
            IsBusy = false;
            ButtonState = ConnectionButtonState.Connect;

            Connect = new RelayCommand(() =>
            {
                switch (ButtonState)
                {
                    case ConnectionButtonState.Connect:
                        _client.Init(Url);
                        SendButtonEnabled = true;
                        UrlFieldEnabled = false;
                        ButtonState = ConnectionButtonState.Disconnect;
                        break;
                    case ConnectionButtonState.Disconnect:
                        _client.Close();
                        SendButtonEnabled = false;
                        UrlFieldEnabled = true;
                        ButtonState = ConnectionButtonState.Connect;
                        break;
                }
            });

            Send = new RelayCommand(async () =>
            {
                SendButtonEnabled = false;
                IsBusy = true;
                try
                {
                    await _client.SetMessageAsync(Text);
                }
                catch (EndpointNotFoundException)
                {
                    _logger.Error("EndpointNotFoundException");
                    ConnectionError?.Invoke(this, null);
                }
                catch (FaultException)
                {
                    _logger.Error("Fault Exception");
                    ServerError?.Invoke(this, null);
                }
                finally
                {
                    SendButtonEnabled = true;
                    IsBusy = false;
                }
            });

            _logger.Info("Im ready");
        }
    }
}