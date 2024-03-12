
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;
using MongoDB.Bson;
using CodeCraft_TeamFinder_Services;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            Project project = await _projectService.Get(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByOrganization(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var projects = await _projectService.GetProjectsByOrganization(id);

            return Ok(projects);
        }

        [HttpGet("EmployeeProjects")]
        public async Task<ActionResult<EmployeeProjectsDTO>> GetEmployeeProjects(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var projects = await _projectService.GetEmployeeProjects(id);

            return Ok(projects);
        }

        [HttpGet("DepartmentProjects")]
        public async Task<ActionResult<IEnumerable<DepartmentProjectDTO>>> GetDepartmentProjects(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var projects = await _projectService.GetDepartmentProjects(id);

            return Ok(projects);
        }

        [HttpGet("ProjectDetails")]
        public async Task<ActionResult<ProjectDetailsDTO>> GetProjectDetails(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var project = await _projectService.GetProjectDetails(id);

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(Project project)
        {
            bool success = await _projectService.Create(project);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProject(Project project)
        {
            bool success = await _projectService.Update(project);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(string id)
        {
            bool success = await _projectService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

