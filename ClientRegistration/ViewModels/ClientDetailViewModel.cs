using ClientRegistration.Models;
using ClientRegistration.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace ClientRegistration.ViewModels
{
    [QueryProperty(nameof(ClientId), "id")]
    public class ClientDetailViewModel : BaseViewModel
    {
        private readonly IClientService _clientService;
        private int _clientId;
        private string _name;
        private string _lastname;
        private int _age;
        private string _address;
        private bool _isNewClient;

        public int ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                LoadClientById(value);
            }
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public bool IsNewClient
        {
            get => _isNewClient;
            set => SetProperty(ref _isNewClient, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ClientDetailViewModel(IClientService clientService)
        {
            _clientService = clientService;
            IsNewClient = true; 

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            PropertyChanged += (_, __) => (SaveCommand as Command)?.ChangeCanExecute();
        }

        private async void LoadClientById(int clientId)
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                if (clientId <= 0)
                {
                    IsNewClient = true;
                    Title = "Novo Cliente";
                    IsBusy = false;
                    return;
                }

                var client = await _clientService.GetClientByIdAsync(clientId);
                if (client != null)
                {
                    IsNewClient = false;
                    Title = "Editar Cliente";
                    Name = client.Name;
                    Lastname = client.Lastname;
                    Age = client.Age;
                    Address = client.Address;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro", $"Cliente com ID {clientId} não encontrado", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar cliente: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível carregar os dados do cliente: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnSave()
        {
            if (!ValidateSave())
            {
                await Shell.Current.DisplayAlert("Validação", "Por favor, preencha todos os campos corretamente.", "OK");
                return;
            }

            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var client = new Client
                {
                    Id = ClientId,
                    Name = Name,
                    Lastname = Lastname,
                    Age = Age,
                    Address = Address
                };

                if (IsNewClient)
                {
                    await _clientService.AddClientAsync(client);
                    Debug.WriteLine("Cliente adicionado com sucesso");
                }
                else
                {
                    await _clientService.UpdateClientAsync(client);
                    Debug.WriteLine("Cliente atualizado com sucesso");
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar cliente: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível salvar o cliente: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnCancel()
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao cancelar: {ex.Message}");
        }
    }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Lastname)
                && Age > 0
                && !string.IsNullOrWhiteSpace(Address);
        }
    }
}