using System;
using System.Windows;
using Client.Model;

namespace Client.UI
{
    public partial class LogInPage
    {
        private readonly LogInViewModel _viewModel;

        public LogInPage()
        {
            var authCLient = new AuthorizationClient();
            _viewModel = new LogInViewModel(authCLient);
            _viewModel.NavigateHandler += Nav;
            DataContext = _viewModel;
            InitializeComponent();
            _init();
        }

        private void Nav(object sender, EventArgs args)
        {
            if (NavigationService == null) throw new Exception("this.NavigationService == null");
            NavigationService.Navigate(sender);
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