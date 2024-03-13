using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IProjectTeamService> _projectTeamService;
        private readonly Lazy<ITeamRoleService> _teamRoleService;

        public ProjectService(IRepository<Project> repository, Lazy<IUserService> userService, Lazy<IProjectTeamService> projectTeamService, Lazy<ITeamRoleService> teamRoleService)
        {
            _repository = repository;
            _userService = userService;
            _projectTeamService = projectTeamService;
            _teamRoleService = teamRoleService;
        }

        public async Task<Project> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Project>> GetProjectsByOrganization(string id)
        {
            return await _repository.Find("OrganizationID", id);
        }

        public async Task<EmployeeProjectsDTO> GetEmployeeProjects(string id)
        {
            var currentProjectsList = new List<ProjectInformation>();

            var pastProjectsList = new List<ProjectInformation>();

            var projectIDs = (await _userService.Value.Get(id)).ProjectIDs;

            if (projectIDs != null)
            {
                foreach (var projectID in projectIDs)
                {
                    var project = await _repository.Get(projectID);

                    var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                    var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == id).FirstOrDefault();

                    var teamRolesList = new List<string>();

                    foreach (var teamRoleID in teamMember.TeamRoleIDs ??  Enumerable.Empty<string>())
                    {
                        var teamRole = await _teamRoleService.Value.Get(teamRoleID);

                        teamRolesList.Add(teamRole.Name);
                    }

                    ProjectInformation currentProjectInformation = new ProjectInformation { ProjectID = projectID, ProjectName = project.Name, TechnologyStack = project.TechnologyStack, Roles = teamRolesList };

                    if (teamMember.Active)
                    {
                        currentProjectsList.Add(currentProjectInformation);
                    }
                    else
                    {
                        pastProjectsList.Add(currentProjectInformation);
                    }
                }
            }

            EmployeeProjectsDTO employeeProjectsDTO = new EmployeeProjectsDTO { CurrentProjects = currentProjectsList, PastProjects = pastProjectsList };

            return employeeProjectsDTO;
        }

        public async Task<IEnumerable<DepartmentProjectDTO>> GetDepartmentProjects(string id)
        {
            var departmentProjects = new List<DepartmentProjectDTO>();  

            var usersByDepartment = await _userService.Value.GetUsersByDepartment(id);

            foreach (var user in usersByDepartment ?? Enumerable.Empty<User>())
            {
                var projectInformations = (await GetEmployeeProjects(user.Id)).CurrentProjects;

                foreach (var projectInformation in projectInformations)
                {
                    var projectTeam = await _projectTeamService.Value.GetProjectTeamMembers(projectInformation.ProjectID);

                    var project = await _repository.Get(projectInformation.ProjectID);

                    DepartmentProjectDTO departmentProjectDTO = new DepartmentProjectDTO { Name = project.Name, DeadlineDate = project.DeadlineDate, Status = project.Status, TeamMembers = projectTeam };

                    departmentProjects.Add(departmentProjectDTO);
                }
            }

            return departmentProjects;
        }

        public async Task<ProjectDetailsDTO> GetProjectDetails(string id)
        {
            var project = await _repository.Get(id);

            var projectTeam = await _projectTeamService.Value.GetProjectTeamMembers(id);

            TeamMembersList teamMembersList = new TeamMembersList { CurrentMembers = projectTeam.ActiveMembers, PastMembers = projectTeam.PastMembers };

            ProjectDetailsDTO projectDetailsDTO = new ProjectDetailsDTO { Name = project.Name, Period = project.Period, StartDate = project.StartDate, DeadlineDate = project.DeadlineDate, Status = project.Status, Description = project.Description, TechnologyStack = project.TechnologyStack, TeamMembers = teamMembersList };

            return projectDetailsDTO;
        }

        public async Task<IEnumerable<Project>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Project project)
        {
            return await _repository.Create(project);
        }

        public async Task<bool> Update(Project project)
        {
            return await _repository.Update(project);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
