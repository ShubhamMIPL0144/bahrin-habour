using Bahrin.Harbour.Model.ClientModel;

namespace Bahrin.Harbour.Data.ClientDA
{
    public interface IClientDA
    {
        Task<bool> AddClientAsync(Client client);
        Task<bool> DeleteClientAsync(Guid id);
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid id);
        Task<bool> UpdateClientAsync(Client client);
    }
}