using ClientRegistration.ViewModels;

namespace ClientRegistration.Views
{
    public partial class ClientListPage : ContentPage
    {
        private ClientListViewModel _viewModel;

        public ClientListPage(ClientListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadClients();
        }
    }
}