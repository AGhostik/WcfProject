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