using System.Windows;

namespace Receiver.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
