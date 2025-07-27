using ClientRegistration.Models;
using ClientRegistration.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ClientRegistration.ViewModels
{
    public class ClientListViewModel : BaseViewModel
    {
        private readonly IClientService _clientService;
        private ObservableCollection<Client> _clients;
        private Client _selectedClient;

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => SetProperty(ref _clients, value);
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand RefreshCommand { get; }

        public ClientListViewModel(IClientService clientService)
        {
            Title = "Cadastro de Clientes";
            _clientService = clientService;
            
            AddClientCommand = new Command(OnAddClient);
            EditClientCommand = new Command<Client>(OnEditClient);
            DeleteClientCommand = new Command<Client>(OnDeleteClient);
            RefreshCommand = new Command(LoadClients);

            LoadClients();
        }

        public async void LoadClients()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                Clients = new ObservableCollection<Client>(clients);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar clientes: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível carregar a lista de clientes: {ex.Message}", "OK");
                Clients = new ObservableCollection<Client>(); // Inicializa com lista vazia em caso de erro
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddClient()
        {
            try
            {
                await Shell.Current.GoToAsync("ClientDetailPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao navegar para adicionar cliente: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível abrir a página de novo cliente: {ex.Message}", "OK");
            }
        }

        private async void OnEditClient(Client client)
        {
            if (client == null) return;
            
            try
            {
                var route = $"ClientDetailPage?id={client.Id}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao navegar para editar cliente: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível abrir a página de edição: {ex.Message}", "OK");
            }
        }

        private async void OnDeleteClient(Client client)
        {
            if (client == null) return;
            if (IsBusy) return;
            
            try
            {
                bool answer = await Shell.Current.DisplayAlert(
                    "Confirmar Exclusão", 
                    $"Deseja realmente excluir o cliente {client.FullName}?", 
                    "Sim", 
                    "Não");

                if (answer)
                {
                    IsBusy = true;
                    try
                    {
                        await _clientService.DeleteClientAsync(client.Id);                    

                    Clients.Remove(client);

                    await Shell.Current.DisplayAlert("Sucesso", "Cliente excluído com sucesso!", "OK");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao excluir cliente: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível excluir o cliente: {ex.Message}", "OK");
            }
        }

    }
}