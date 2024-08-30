using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        
        private readonly IClientServices _clientService;

        public ClientsController(IClientServices clientService)
        {
            _clientService = clientService;
        }

        [HttpOptions]
        public async Task <IActionResult> GetAll()
        {
            var result = await _clientService.GetAll();
            return new JsonResult(result);

        }

        [HttpPost]
        public async Task <IActionResult> UpdateClient(ClientRequest request)
        {
            var result = await _clientService.UpdateClient(request);
            return new JsonResult(result) { StatusCode = 201};
        }
    }
}
