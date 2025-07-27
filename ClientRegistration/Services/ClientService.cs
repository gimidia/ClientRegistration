using ClientRegistration.Models;
using System.Diagnostics;

namespace ClientRegistration.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientDatabase _database;

        public ClientService(IClientDatabase database)
        {
            _database = database;
        }



        public async Task<List<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _database.GetAllClientsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter todos os clientes: {ex.Message}");
                return new List<Client>();
            }
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            try
            {
                return await _database.GetClientByIdAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter cliente por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<int> AddClientAsync(Client client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client), "Cliente não pode ser nulo");
                
                return await _database.SaveClientAsync(client);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
                throw; 
            }
        }

        public async Task<int> UpdateClientAsync(Client client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client), "Cliente não pode ser nulo");
                
                var existingClient = await _database.GetClientByIdAsync(client.Id);
                if (existingClient != null)
                {
                    return await _database.SaveClientAsync(client);
                }
                else
                {
                    throw new InvalidOperationException($"Cliente com ID {client.Id} não encontrado");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                throw; 
            }
        }

        public async Task<int> DeleteClientAsync(int id)
        {
            try
            {
                var client = await _database.GetClientByIdAsync(id);
                if (client != null)
                {
                    return await _database.DeleteClientAsync(id);
                }
                else
                {
                    throw new InvalidOperationException($"Cliente com ID {id} não encontrado");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao excluir cliente: {ex.Message}");
                throw; 
            }
        }
    }
}