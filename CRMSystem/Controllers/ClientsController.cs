using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;


namespace CRMSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        
        private readonly IClientServices _clientService;

        public ClientsController(IClientServices clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var result = await _clientService.GetAll();
            return new JsonResult(result);

        }

        [HttpPost]
        public async Task <IActionResult> CreateClient(ClientRequest request)
        {
               var result = await _clientService.CreateClient(request);
               return new JsonResult(result) { StatusCode = 201 };
          
        }
    }
}
