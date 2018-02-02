using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Host.Model;

namespace Host.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IHostService _hostService;
        
        private bool _startEnabled;
        
        private bool _stopEnabled;

        public MainViewModel(IHostService service)
        {
            _hostService = service;
            _init();
        }

        public RelayCommand StartService { get; set; }
        public RelayCommand StopService { get; set; }
        
        public bool StartEnabled
        {
            get => _startEnabled;
            set => Set(ref _startEnabled, value);
        }

        public bool StopEnabled
        {
            get => _stopEnabled;
            set => Set(ref _stopEnabled, value);
        }

        private void _init()
        {
            StartEnabled = true;
            StopEnabled = false;

            StartService = new RelayCommand(() =>
            {
                _hostService.Open();
                StartEnabled = false;
                StopEnabled = true;
            });
            StopService = new RelayCommand(() =>
            {
                _hostService.Close();
                StartEnabled = true;
                StopEnabled = false;
            });
        }
    }
}