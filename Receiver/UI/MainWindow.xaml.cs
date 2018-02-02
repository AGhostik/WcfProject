using System;
using System.ComponentModel;
using System.Windows;
using Receiver.Model;

namespace Receiver.UI
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel(new Client());
            DataContext = _viewModel;
            InitializeComponent();
            _init();
        }

        private void _init()
        {
            _viewModel.ConnectionError += ConnectionFailedMessage;
            _viewModel.ServerError += ServerErrorMessage;
        }

        private void ConnectionFailedMessage(object sender, EventArgs args)
        {
            MessageBox.Show("Connection failed");
        }

        private void ServerErrorMessage(object sender, EventArgs args)
        {
            MessageBox.Show("Server error");
        }
    }
}