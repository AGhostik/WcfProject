using System;
using System.Windows;
using Client.Model;

namespace Client.UI
{
    public partial class ChatPage
    {
        private readonly ChatViewModel _viewModel;

        public ChatPage(ConnectionSettings connectionSettings)
        {
            var messageClient = new MessageClient(connectionSettings);
            _viewModel = new ChatViewModel(messageClient);
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