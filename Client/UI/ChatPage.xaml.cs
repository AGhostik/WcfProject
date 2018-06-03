using System;
using System.Windows;
using Client.MessageServiceReference;
using Client.Model;

namespace Client.UI
{
    public partial class ChatPage
    {
        private readonly ChatViewModel _viewModel;

        public ChatPage(MessageClient client)
        {
            _viewModel = new ChatViewModel(client);
            _viewModel.NavigateHandler += Navigate;
            DataContext = _viewModel;
            InitializeComponent();
            _init();
        }

        private void Navigate(object sender, EventArgs args)
        {
            if (NavigationService == null) throw new Exception("this.NavigationService == null");
            NavigationService.GoBack();
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