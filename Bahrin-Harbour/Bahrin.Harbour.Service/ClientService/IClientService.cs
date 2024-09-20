using Bahrin.Harbour.Model.ClientModel;

namespace Bahrin.Harbour.Service.ClientService
{
    public interface IClientService
    {
        Task<bool> AddClientAsync(ClientViewModel clientViewModel);
        Task<bool> DeleteClientAsync(string id);
        Task<List<ClientViewModel>> GetAllClientsAsync();
        Task<ClientViewModel> GetClientByIdAsync(string id);
        Task<bool> UpdateClientAsync(ClientViewModel clientViewModel);
    }
}