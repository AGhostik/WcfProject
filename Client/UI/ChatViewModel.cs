using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Client.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Client.MessageServiceReference;
using NLog;
using System.ServiceModel;

namespace Client.UI
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly MessageClient _messageClient;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private bool _isBusy;
        private string _text;
        public EventHandler ConnectionError;
        public EventHandler ServerError;

        public ChatViewModel(MessageClient messageClient)
        {
            _messageClient = messageClient;
            _init();
        }

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public RelayCommand SendMessage { get; set; }

        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private void _init()
        {
            IsBusy = false;

            SendMessage = new RelayCommand(async () =>
            {
                IsBusy = true;
                try
                {
                    var newMessage = await _messageClient.AddMessage(Text);
                    Messages.Add(newMessage);
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
                    IsBusy = false;
                }
            });

            _logger.Info("Hello! Nice day for checking mail!");
        }
    }
}