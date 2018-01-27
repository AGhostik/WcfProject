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

        private int _state;

        public MainViewModel()
        {
            _hostService = new HostService();
            _init();
        }

        public RelayCommand StartService { get; set; }
        public RelayCommand StopService { get; set; }

        public int State
        {
            get => _state;
            set => Set(ref _state, value);
        }

        private void _init()
        {
            StartService = new RelayCommand(() => { _hostService.Open(); });
            StopService = new RelayCommand(() => { _hostService.Close(); });

            _hostService.HostOpened += (sender, args) =>
            {
                State = 1;
                _logger.Info("Service opened");
            };
            _hostService.HostClosed += (sender, args) =>
            {
                State = 0;
                _logger.Info("Service closed");
            };
            _hostService.HostFaulted += (sender, args) =>
            {
                State = -1;
                _logger.Error("Service faulted");
            };

            _logger.Info("App init");
        }
    }
}