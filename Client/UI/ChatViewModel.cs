using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.MessageServiceReference;
using Client.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;

namespace Client.UI
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly MessageClient _messageClient;

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

        public RelayCommand SelectionChanged { get; set; }

        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public ObservableCollection<Chat> Chats { get; set; } = new ObservableCollection<Chat>();

        public Chat SelectedChat { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string Username { get; private set; }

        private void _init()
        {
            IsBusy = false;
            Username = _messageClient.Username;
            FillChatListSync();

            SelectionChanged = new RelayCommand(async () =>
            {
                Messages.Clear();
                if (SelectedChat == null) return;
                await _doClientWork(async () =>
                {
                    var messages = await _messageClient.GetChatMessages(SelectedChat.Id);

                    foreach (var message in messages) Messages.Add(message);
                });
            });

            SendMessage = new RelayCommand(async () =>
            {
                if (SelectedChat == null) return;
                await _doClientWork(async () =>
                {
                    var newMessage = await _messageClient.AddMessage(SelectedChat.Id, Text);
                    Messages.Add(newMessage);
                });
            });

            _logger.Info("Hello! Nice day for checking mail!");
        }

        private void FillChatListSync()
        {
            var chats = _messageClient.GetChatsSync();
            foreach (var chat in chats) Chats.Add(chat);
        }

        private async Task _doClientWork(Func<Task> asyncAction)
        {
            IsBusy = true;
            try
            {
                await asyncAction();
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
        }
    }
}