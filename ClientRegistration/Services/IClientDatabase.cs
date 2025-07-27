using ClientRegistration.Models;

namespace ClientRegistration.Services
{
    public interface IClientDatabase
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<int> SaveClientAsync(Client client);
        Task<int> DeleteClientAsync(int id);
    }
}
