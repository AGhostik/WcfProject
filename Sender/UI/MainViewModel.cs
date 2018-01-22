using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Sender.UI
{
    public class MainViewModel : ViewModelBase
    {
        public string Text { get; set; }
        public RelayCommand Send { get; set; }

        public MainViewModel()
        {
            Send = new RelayCommand(() =>
            {

            });
        }
    }
}
