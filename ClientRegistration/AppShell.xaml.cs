using ClientRegistration.Views;

namespace ClientRegistration
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
        }
    }
}
