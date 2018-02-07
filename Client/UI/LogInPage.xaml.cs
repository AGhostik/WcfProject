using System;
namespace Client.UI
{
    public partial class LogInPage
    {
        public LogInPage()
        {
            var viewModel = new LogInViewModel();
            viewModel.NavigateHandler += Nav;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Nav(object sender, EventArgs args)
        {
            this.NavigationService.Navigate(sender);
        }
    }
}
