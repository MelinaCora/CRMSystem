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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;


        public ProjectsController(IProjectService service)
        {
            _service = service;
        }


        [HttpPost]
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
            catch (Exception ex)
            {
                // Para cualquier otro error no manejado, retornar un error 500
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetProjects(
            string? Name,
            int? campaign,
            int? client,
            int? offset,
            int? size)
        {
            var result = await _service.GetProjectsAsync(Name, campaign, client, offset, size);

            if (result.Items.Count == 0)
            {
                return NotFound("No projects found.");
            }

            return Ok(result);
        }
        //get project by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            try
            {
                var project = await _service.GetProjectByIdAsync(id);                
                return Ok(project);
            }
            catch (ObjectNotFoundException ex)            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
        //Add interaction
        [HttpPatch("{id}/interactions")]
        public async Task<IActionResult> AddInteraction(Guid id, [FromBody] CreateInteractionRequest request)
        {
            
            try
            {
                var result = await _service.AddInteractionAsync(id,request);

                if (result)
                {
                    return Ok("Interaction added successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while adding the interaction.");
                }
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //AddTask
        [HttpPatch("{id}/tasks")]
        public async Task<IActionResult> AddTaskToProject(Guid id, [FromBody] TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.AddTaskToProject(id, request);

                if (result)
                {
                    return Ok("Task added successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while adding the task.");
                }
            }
            catch (ObjectNotFoundException ex)
            {
               return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("tasks/{Taskid}")]
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
        } 
    }
}
