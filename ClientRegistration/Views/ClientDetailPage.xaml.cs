using ClientRegistration.Extensions;
using ClientRegistration.ViewModels;

namespace ClientRegistration.Views
{
    public partial class ClientDetailPage : ContentPage
    {
        public ClientDetailPage(ClientDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            // Centralizar a janela quando aberta
            if (Window != null)
            {
                Window.CenterWindow(500, 600);
            }
        }
    }
}