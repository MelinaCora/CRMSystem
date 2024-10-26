using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;


        public ProjectController(IProjectService service)
        {
            _service = service;
        }

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjects(
            string? name,
            int? campaign,
            int? client,
            int? offset,
            int? size)
        {
            try
            {
                var result = await _service.GetProjectsAsync(name, campaign, client, offset, size);

                if (result.Count == 0)
                {
                    return NotFound("No projects found.");
                }

                return Ok(result);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (StaticParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }


        }
              
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            try
            {
                var result = await _service.CreateProject(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ProjectNameAlredyExistException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (ObjectNotFoundException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (RequiredParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (StaticParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }

              
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            try
            {
                var project = await _service.GetProjectByIdAsync(id);                
                return Ok(project);
            }
            catch (ObjectNotFoundException ex)            {
                
                return NotFound(new { message = ex.Message });
            }
        }
                        
        [HttpPatch("{id}/interactions")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddInteraction(Guid id, [FromBody] CreateInteractionRequest request)
        {
            try
            {                
                var interactionResponse = await _service.AddInteractionAsync(id, request);               
                return Ok(interactionResponse);               
            }
            catch (ObjectNotFoundException ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
            catch (RequiredParameterException ex)
            {               
                return BadRequest(new { message = ex.Message });
            }
            catch (StaticParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
               
        [HttpPatch("{id}/tasks")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTaskToProject(Guid id, [FromBody] TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var taskResponse = await _service.AddTaskToProject(id, request);
                return Ok(taskResponse);
                
            }
            catch (ObjectNotFoundException ex)
            {
               return BadRequest(new { message = ex.Message });
            }
            catch (RequiredParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (StaticParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
               
        [HttpPut("/api/v1/Tasks/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTask = await _service.UpdateTaskAsync(id, request);
                return Ok(updatedTask);

            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (StaticParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (RequiredParameterException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        } 
    }
}
