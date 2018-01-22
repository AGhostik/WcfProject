using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sender.MessageServiceReference;

namespace Sender.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MessageServiceClient _messageService;

        public string Text { get; set; }
        public RelayCommand Send { get; set; }

        public MainViewModel()
        {
            _messageService = new MessageServiceClient();
            _init();
        }

        private void _init()
        {
            Send = new RelayCommand(() =>
            {
                _messageService.SetMessage(Text);
            });
        }
    }
}
