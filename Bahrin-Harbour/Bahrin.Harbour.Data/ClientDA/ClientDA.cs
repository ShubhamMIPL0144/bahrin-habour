using Bahrin.Harbour.Data.DataContext;
using Bahrin.Harbour.Model.ClientModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bahrin.Harbour.Data.ClientDA
{
    public class ClientDA : IClientDA
    {
        private readonly BahrinHarbourContext _context;
        private readonly ILogger<ClientDA> _logger;

        public ClientDA(BahrinHarbourContext context, ILogger<ClientDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding client with ID {ClientId}", client.ClientId);
                throw;
            }
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _context.Clients.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all clients.");
                throw;
            }
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            try
            {
                return await _context.Clients.FirstOrDefaultAsync(c => c.id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving client with ID {ClientId}", id);
                throw;
            }
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            try
            {
                var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == client.ClientId);
                if (existingClient != null)
                {
                    existingClient.ClientName = client.ClientName;
                    existingClient.Properties = client.Properties;
                    existingClient.Phone = client.Phone;
                    existingClient.LastVisit = client.LastVisit;
                    existingClient.Status = client.Status;

                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Client with ID {ClientId} not found for update.", client.ClientId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with ID {ClientId}", client.ClientId);
                throw;
            }
        }
        public async Task<bool> DeleteClientAsync(Guid Id)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.id == Id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Client with ID {ClientId} not found for deletion.", Id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting client with ID {ClientId}", Id);
                throw;
            }
        }
    }
}


