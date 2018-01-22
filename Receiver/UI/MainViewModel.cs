using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Receiver.UI
{
    public class MainViewModel : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public RelayCommand Update { get; set; }

        public MainViewModel()
        {
            Update = new RelayCommand(() =>
            {

            });
        }
    }
}
