using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Host.Model;

namespace Host.UI
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand StartService { get; set; }
        public RelayCommand StopService { get; set; }

        private readonly IHostService _hostService;
        
        public MainViewModel()
        {
            _hostService = new HostService();
            _init();
        }

        private void _init()
        {
            StartService = new RelayCommand(() =>
            {
                _hostService.Open();
            });
            StopService = new RelayCommand(() =>
            {
                _hostService.Close();
            });
        }
    }
}
