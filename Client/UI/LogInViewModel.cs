using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Client.UI
{
    public class LogInViewModel : ViewModelBase
    {
        public LogInViewModel()
        {
            _init();
        }

        public EventHandler NavigateHandler;
        
        public string Url { get; set; }
        public string Username { get; set; }
        public RelayCommand LogIn { get; set; }
        public bool IsBusy { get => _isBusy; set => Set(ref _isBusy, value); }

        private bool _isBusy;

        private void _init()
        {
            Url = "http://127.0.0.1:8080";
            Username = "Father Gascoine";

            LogIn = new RelayCommand(() =>
            {
                var win = new ChatPage(new ConnectionSettings()
                {
                    Url = Url,
                    Username = Username
                });
                NavigateHandler(win, null); //я знаю что это полное говно, но так проще
            });
        }
    }
}
