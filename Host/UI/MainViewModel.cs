using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Host.Model;

namespace Host.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IHostService _hostService;

        private bool _startEnabled;

        public MainViewModel(IHostService service)
        {
            _hostService = service;
            _init();
        }

        public RelayCommand StartService { get; set; }

        public bool StartEnabled
        {
            get => _startEnabled;
            set => Set(ref _startEnabled, value);
        }

        private void _init()
        {
            StartEnabled = true;

            StartService = new RelayCommand(() =>
            {
                _hostService.Open();
                StartEnabled = false;
            });
        }
    }
}