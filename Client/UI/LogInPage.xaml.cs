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
            _viewModel.NavigateHandler += Navigate;
            DataContext = _viewModel;
            InitializeComponent();
            _init();
        }

        private void Navigate(object sender, EventArgs args)
        {
            if (NavigationService == null) throw new Exception("this.NavigationService == null");
            NavigationService.Navigate(sender);
        }

        private void _init()
        {
            _viewModel.CommunicationError += CommunicationErrorMessage;
            _viewModel.DisposedError += DisposedErrorMessage;
        }

        private void CommunicationErrorMessage(object sender, EventArgs args)
        {
            MessageBox.Show("Communication error");
        }

        private void DisposedErrorMessage(object sender, EventArgs args)
        {
            MessageBox.Show("Server error");
        }
    }
}