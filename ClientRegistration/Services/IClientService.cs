using ClientRegistration.Models;

namespace ClientRegistration.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<int> AddClientAsync(Client client);
        Task<int> UpdateClientAsync(Client client);
        Task<int> DeleteClientAsync(int id);
    }
}