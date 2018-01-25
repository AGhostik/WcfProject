using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sender.MessageServiceReference;

namespace Sender.UI
{
    public class MainViewModel : ViewModelBase
    {
        public string Text { get; set; }
        public RelayCommand Send { get; set; }
        public EventHandler ConnectionError;

        public MainViewModel()
        {
            _init();
        }

        private void _init()
        {
            Send = new RelayCommand(() =>
            {
                try
                {
                    var messageService = new MessageServiceClient();
                    messageService.SetMessage(Text);
                }
                catch (EndpointNotFoundException)
                {
                    ConnectionError?.Invoke(this, null);
                }
            });
        }
    }
}
