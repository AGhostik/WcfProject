using System;
using System.Windows;

namespace Sender.UI
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            InitializeComponent();
            _init();
        }

        private void _init()
        {
            _viewModel.ConnectionError += ShowMessage;
        }

        private void ShowMessage(object sender, EventArgs args)
        {
            MessageBox.Show("Connection failed");
        }
    }
}