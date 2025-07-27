using ClientRegistration.Models;
using SQLite;
using System.Diagnostics;

namespace ClientRegistration.Services
{
    public class ClientDatabase : IClientDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        private const string DB_NAME = "clients.db3";

        public ClientDatabase()
        {
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<Client>().Wait();
                Debug.WriteLine($"Banco de dados inicializado em: {dbPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao inicializar banco de dados: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _database.Table<Client>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter clientes: {ex.Message}");
                return new List<Client>();
            }
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            try
            {
                return await _database.Table<Client>().Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter cliente por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<int> SaveClientAsync(Client client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client), "Cliente não pode ser nulo");

                if (client.Id != 0)
                {
                    return await _database.UpdateAsync(client);
                }
                else
                {
                    return await _database.InsertAsync(client);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar cliente: {ex.Message}");
                throw;
            }
        }

        public async Task<int> DeleteClientAsync(int id)
        {
            try
            {
                return await _database.DeleteAsync<Client>(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao excluir cliente: {ex.Message}");
                throw;
            }
        }
    }
}