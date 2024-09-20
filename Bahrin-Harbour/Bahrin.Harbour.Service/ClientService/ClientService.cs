using Bahrin.Harbour.Data.ClientDA;
using Bahrin.Harbour.Model.ClientModel;
using Bahrin.Harbour.Model.ProjectSession;

namespace Bahrin.Harbour.Service.ClientService
{
    public class ClientService : IClientService
    {
        private readonly IClientDA _clientDA;

        public ClientService(IClientDA clientDA)
        {
            _clientDA = clientDA;
        }
        public async Task<List<ClientViewModel>> GetAllClientsAsync()
        {
            List<Client> clients = await _clientDA.GetAllClientsAsync();

            List<ClientViewModel> ListClientViewModel = clients.Select(client => new ClientViewModel
            {
                Id = client.id,
                ClientName = client.ClientName,
                ClientId = client.ClientId,
                Name = client.Name,
                EmailAddress = client.EmailAddress,
                Phone = client.Phone,
                PhoneNumber = client.PhoneNumber,
                Country = client.Country,
                City = client.City,
                State = client.State,
                Postcode = client.Postcode,
                Address = client.Address,
                Properties = client.Properties,
                TypeOfProperty = client.TypeOfProperty,
                PropertyLocation = client.PropertyLocation,
                PropertyPrice = client.PropertyPrice,
                AvailedDiscount = client.AvailedDiscount,
                Street = client.Street,
                SaveProperty = client.SaveProperty,
                LastVisit = client.LastVisit,
                Status = client.Status
            }).ToList();

            return ListClientViewModel;
        }

        public async Task<bool> AddClientAsync(ClientViewModel clientViewModel)
        {
            
           clientViewModel.Id = clientViewModel.Id == Guid.Empty ? Guid.NewGuid() : clientViewModel.Id;
           Client client = new Client
            {
                id = clientViewModel.Id,
                ClientName = clientViewModel.ClientName,
                ClientId = clientViewModel.ClientId,
                Name = clientViewModel.Name,
                EmailAddress = clientViewModel.EmailAddress,
                Phone = clientViewModel.Phone,
                PhoneNumber = clientViewModel.PhoneNumber,
                Country = clientViewModel.Country,
                City = clientViewModel.City,
                State = clientViewModel.State,
                Postcode = clientViewModel.Postcode,
                Address = clientViewModel.Address,
                Properties = clientViewModel.Properties,
                TypeOfProperty = clientViewModel.TypeOfProperty,
                PropertyLocation = clientViewModel.PropertyLocation,
                PropertyPrice = clientViewModel.PropertyPrice,
                AvailedDiscount = clientViewModel.AvailedDiscount,
                Street = clientViewModel.Street,
                SaveProperty = clientViewModel.SaveProperty,
                LastVisit = clientViewModel.LastVisit,
                Status = clientViewModel.Status,
                Created_at = DateTime.Now,
                ModifiedBy = ProjectSessionModel.admin._id
            };

            return await _clientDA.AddClientAsync(client);
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            return await _clientDA.DeleteClientAsync(Guid.Parse(id));
        }

        public async Task<ClientViewModel> GetClientByIdAsync(string id)
        {
            Client client = await _clientDA.GetClientByIdAsync(Guid.Parse(id));

            if (client == null)
            {
                return null;
            }

            ClientViewModel clientViewModel = new ClientViewModel
            {
                Id = client.id,
                ClientName = client.ClientName,
                ClientId = client.ClientId,
                Name = client.Name,
                EmailAddress = client.EmailAddress,
                Phone = client.Phone,
                PhoneNumber = client.PhoneNumber,
                Country = client.Country,
                City = client.City,
                State = client.State,
                Postcode = client.Postcode,
                Address = client.Address,
                Properties = client.Properties,
                TypeOfProperty = client.TypeOfProperty,
                PropertyLocation = client.PropertyLocation,
                PropertyPrice = client.PropertyPrice,
                AvailedDiscount = client.AvailedDiscount,
                Street = client.Street,
                SaveProperty = client.SaveProperty,
                LastVisit = client.LastVisit,
                Status = client.Status
            };

            return clientViewModel;
        }

        public async Task<bool> UpdateClientAsync(ClientViewModel clientViewModel)
        {
            Client client = new Client
            {
                id = clientViewModel.Id,
                ClientName = clientViewModel.ClientName,
                ClientId = clientViewModel.ClientId,
                Name = clientViewModel.Name,
                EmailAddress = clientViewModel.EmailAddress,
                Phone = clientViewModel.Phone,
                PhoneNumber = clientViewModel.PhoneNumber,
                Country = clientViewModel.Country,
                City = clientViewModel.City,
                State = clientViewModel.State,
                Postcode = clientViewModel.Postcode,
                Address = clientViewModel.Address,
                Properties = clientViewModel.Properties,
                TypeOfProperty = clientViewModel.TypeOfProperty,
                PropertyLocation = clientViewModel.PropertyLocation,
                PropertyPrice = clientViewModel.PropertyPrice,
                AvailedDiscount = clientViewModel.AvailedDiscount,
                Street = clientViewModel.Street,
                SaveProperty = clientViewModel.SaveProperty,
                LastVisit = clientViewModel.LastVisit,
                Status = clientViewModel.Status
            };

            return await _clientDA.UpdateClientAsync(client);
        }

    }
}
