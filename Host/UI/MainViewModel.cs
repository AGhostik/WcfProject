using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Host.Model;
using NLog;

namespace Host.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HostService _hostService;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _startEnabled;
        
        private bool _stopEnabled;

        public MainViewModel()
        {
            _hostService = new HostService();
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

            _logger.Info("App init");
        }
    }
}