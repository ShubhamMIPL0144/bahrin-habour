using Bahrin.Harbour.Data.ClientDA;
using Bahrin.Harbour.Model.ClientModel;
using Bahrin.Harbour.Model.ProjectSession;
using System.Drawing.Imaging;
using System.Drawing;
using QRCoder;
using Bahrin.Harbour.Helper;

namespace Bahrin.Harbour.Service.ClientService
{
    public class ClientService : IClientService
    {
        private readonly IClientDA _clientDA;
        private readonly IImageService _image;
        private readonly string imageFolderName = Constants.ClientProfileImages;

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
                Status = client.Status,
                ClientProfileImageLink = _image.GenerateImageUrl(imageFolderName, client.ClientProfileImageFileName),
                ClientQrCodeImageLink = _image.GenerateImageUrl(Constants.QrCodeImages, client.QrImageName)
            }).ToList();

            return ListClientViewModel;
        }

        public async Task<bool> AddClientAsync(ClientViewModel clientViewModel)
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
                Status = clientViewModel.Status,
                Created_at = DateTime.Now,
                ModifiedBy = ProjectSessionModel.admin._id,
            };

            if (clientViewModel.Id == Guid.Empty)
            {
                client.id = Guid.NewGuid();
                client.QrImageName = await _image.GenerateQrCodeAsync(client.id);
            }

            if (clientViewModel.ImageFile != null)
            {
                client.ClientProfileImageFileName = await _image.SaveImageAsync(clientViewModel.ImageFile, imageFolderName);
                client.ImageFolderName = imageFolderName;
            }

            return await _clientDA.AddClientAsync(client);
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            var client = await GetClientByIdAsync(id);
            if (client != null)
            {
                _image.DeleteImage(imageFolderName, client.ClientProfileImageLink);
                _image.DeleteImage(Constants.QrCodeImages, client.ClientQrCodeImageLink);
            }
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
                Status = client.Status,
                ClientProfileImageLink = _image.GenerateImageUrl(imageFolderName, client.ClientProfileImageFileName),
                ClientQrCodeImageLink = _image.GenerateImageUrl(Constants.QrCodeImages, client.QrImageName)
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

            if (clientViewModel.ImageFile != null)
            {
                client.ClientProfileImageFileName = await _image.UpdateImageAsync(clientViewModel.ImageFile, imageFolderName, client.ClientProfileImageFileName);
                client.ImageFolderName = imageFolderName;
            }

            return await _clientDA.UpdateClientAsync(client);
        }
    }
}
