// Controllers/ProjectOwnersController.cs
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Services;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectOwnersController : ControllerBase
    {
        private readonly IProjectOwnerService _projectOwnerService;

        public ProjectOwnersController(IProjectOwnerService projectOwnerService)
        {
            _projectOwnerService = projectOwnerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectOwnerDto>>> GetProjectOwners()
        {
            var owners = await _projectOwnerService.GetAllProjectOwnersAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectOwnerDto>> GetProjectOwner(int id)
        {
            var owner = await _projectOwnerService.GetProjectOwnerByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectOwnerDto>> CreateProjectOwner(ProjectOwnerDto projectOwnerDto)
        {
            var createdOwner = await _projectOwnerService.CreateProjectOwnerAsync(projectOwnerDto);
            return CreatedAtAction(nameof(GetProjectOwner), new { id = createdOwner.Id }, createdOwner);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectOwnerDto>> UpdateProjectOwner(int id, ProjectOwnerDto projectOwnerDto)
        {
            try
            {
                projectOwnerDto.Id = id;
                var updatedOwner = await _projectOwnerService.UpdateProjectOwnerAsync(id, projectOwnerDto);
                return Ok(updatedOwner);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectOwner(int id)
        {
            var result = await _projectOwnerService.DeleteProjectOwnerAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
