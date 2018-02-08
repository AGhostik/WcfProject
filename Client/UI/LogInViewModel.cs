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
        public EventHandler ConnectionError;

        public EventHandler NavigateHandler;
        public EventHandler ServerError;

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
                    await _client.PingServer(Url);

                    var win = new ChatPage(new ConnectionSettings
                    {
                        Url = Url,
                        Username = Username
                    });
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