using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;

namespace Client.UI
{
    public class LogInViewModel : ViewModelBase
    {
        private readonly AuthorizationClient _client;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private bool _isBusy;
        public EventHandler CommunicationError;

        public EventHandler NavigateHandler;
        public EventHandler DisposedError;

        public LogInViewModel(AuthorizationClient client)
        {
            _client = client;
            _init();
        }

        public string Url { get; set; }
        public string Username { get; set; }
        public RelayCommand LogIn { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private void _init()
        {
            Url = "http://127.0.0.1:8080";
            Username = "Username";

            LogIn = new RelayCommand(async () =>
            {
                await _doClientWork(async () =>
                {
                    var connect = await _client.Login(Url, Username, "no pass");

                    var win = new ChatPage(connect);
                    NavigateHandler(win, null); //я знаю что это полное говно, но так проще
                });
            });
        }

        private async Task _doClientWork(Func<Task> asyncAction)
        {
            IsBusy = true;
            try
            {
                await asyncAction();
            }
            catch (CommunicationException)
            {
                _logger.Error("CommunicationException");
                CommunicationError?.Invoke(this, null);
            }
            catch (ObjectDisposedException)
            {
                _logger.Error("ObjectDisposedException");
                DisposedError?.Invoke(this, null);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}