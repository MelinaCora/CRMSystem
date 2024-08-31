using Aplication.Interfaces;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;


namespace CRMSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaignTypeController : ControllerBase
    {
        private readonly ICampaignTypeService _service;

        public CampaignTypeController(ICampaignTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return new JsonResult(result);

        }
    }
}
