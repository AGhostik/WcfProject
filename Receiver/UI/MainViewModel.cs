using System;
using System.ServiceModel;
using ClientLibrary.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;

namespace Receiver.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IClient _client;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private ConnectionButtonState _buttonState;

        private bool _isBusy;
        private string _text;
        private bool _updateButtonEnabled;
        private bool _urlFieldEnabled;
        public EventHandler ConnectionError;
        public EventHandler ServerError;

        public MainViewModel(IClient client)
        {
            _client = client;
            _init();
        }

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public string Url { get; set; }

        public RelayCommand Update { get; set; }
        public RelayCommand Connect { get; set; }

        public bool UpdateButtonEnabled
        {
            get => _updateButtonEnabled;
            set => Set(ref _updateButtonEnabled, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

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

        private void _init()
        {
            Url = "http://127.0.0.1:8080/";
            UpdateButtonEnabled = false;
            UrlFieldEnabled = true;
            IsBusy = false;
            ButtonState = ConnectionButtonState.Connect;

            Connect = new RelayCommand(async () =>
            {
                switch (ButtonState)
                {
                    case ConnectionButtonState.Connect:
                        try
                        {
                            await _client.Init(Url);
                            UpdateButtonEnabled = true;
                            UrlFieldEnabled = false;
                            ButtonState = ConnectionButtonState.Disconnect;
                        }
                        catch (EndpointNotFoundException)
                        {
                            _logger.Error("EndpointNotFoundException");
                            ConnectionError?.Invoke(this, null);
                        }
                        break;
                    case ConnectionButtonState.Disconnect:
                        _client.Close();
                        UpdateButtonEnabled = false;
                        UrlFieldEnabled = true;
                        ButtonState = ConnectionButtonState.Connect;
                        Text = string.Empty;
                        break;
                }
            });

            Update = new RelayCommand(async () =>
            {
                UpdateButtonEnabled = false;
                IsBusy = true;
                Text = string.Empty;
                try
                {
                    Text = await _client.GetMessageAsync();
                }
                catch (EndpointNotFoundException)
                {
                    _logger.Error("EndpointNotFound Exception");
                    ConnectionError?.Invoke(this, null);
                }
                catch (FaultException)
                {
                    _logger.Error("Fault Exception");
                    ServerError?.Invoke(this, null);
                }
                finally
                {
                    UpdateButtonEnabled = true;
                    IsBusy = false;
                }
            });

            _logger.Info("Hello! Nice day for checking mail!");
        }
    }
}