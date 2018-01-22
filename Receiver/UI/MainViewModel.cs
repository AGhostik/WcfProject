using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Receiver.MessageServiceReference;

namespace Receiver.UI
{
    public class MainViewModel : ViewModelBase
    {
        private string _text;
        private readonly MessageServiceClient _messageService;

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public RelayCommand Update { get; set; }

        public MainViewModel()
        {
            _messageService = new MessageServiceClient();
            _init();
        }

        private void _init()
        {
            Update = new RelayCommand(() =>
            {
                Text = _messageService.GetMessage();
            });
        }
    }
}
