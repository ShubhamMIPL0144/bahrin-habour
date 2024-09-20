using Bahrin.Harbour.Model.ClientModel;
using Bahrin.Harbour.Service.ClientService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bahrin_Harbour.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("[area]/[controller]/[action]")]
  //  [AdminAuthorize]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Clients()
        {
            return View();
        }
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }
        public async Task<IActionResult> GetClientById(string Id)
        {
            var clientViewModel = await _clientService.GetClientByIdAsync(Id);
            if (clientViewModel == null)
            {
                return NotFound();
            }
            return Ok(clientViewModel);
        }
        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientViewModel clientView)
        {
            if (ModelState.IsValid)
            {
                var success = await _clientService.AddClientAsync(clientView);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to create client.");
            }
            return View(clientView);
        }
        public async Task<IActionResult> UpdateClient(string Id)
        {
            var clientViewModel = await _clientService.GetClientByIdAsync(Id);
            if (clientViewModel == null)
            {
                return NotFound();
            }
            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(ClientViewModel clientView)
        {
            if (ModelState.IsValid)
            {
                var success = await _clientService.UpdateClientAsync(clientView);
                if (success)
                {
                    return RedirectToAction("Administration/Client/Clients");
                }
                ModelState.AddModelError("", "Failed to update client.");
            }
            return View(clientView);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(string Id)
        {
            var success = await _clientService.DeleteClientAsync(Id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return NotFound(); 
        }
    }
}
