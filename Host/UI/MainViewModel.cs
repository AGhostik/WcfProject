using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Host.Model;

namespace Host.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IHostService _hostService;
        private bool _buttonEnabled;

        private bool _readOnly;
        private bool _isBusy;

        public MainViewModel(IHostService service)
        {
            _hostService = service;
            _init();
        }

        public RelayCommand StartService { get; set; }

        public int MessagesLimit { get; set; }
        public int ChatsLimit { get; set; }
        public int UsersLimit { get; set; }

        public bool ReadOnly
        {
            get => _readOnly;
            set => Set(ref _readOnly, value);
        }

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => Set(ref _buttonEnabled, value);
        }
        public bool IsBusy { get => _isBusy; set => Set(ref _isBusy, value); }

        private void _init()
        {
            ReadOnly = false;
            ButtonEnabled = true;
            IsBusy = false;

            StartService = new RelayCommand(() =>
            {
                _hostService.Open();
                ReadOnly = true;
                ButtonEnabled = false;
            });
        }
    }
}