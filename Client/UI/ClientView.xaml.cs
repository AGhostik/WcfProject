using System.Windows.Navigation;

namespace Client.UI
{
    public partial class ClientView
    {
        public ClientView()
        {
            this.Navigate(new LogInPage());
            InitializeComponent();
        }
    }
}
