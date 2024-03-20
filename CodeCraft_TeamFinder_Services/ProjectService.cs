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
        private readonly Lazy<ISkillService> _skillService;
        private readonly Lazy<ISystemRoleService> _systemRoleService;

        public ProjectService(IRepository<Project> repository, Lazy<IUserService> userService, Lazy<IProjectTeamService> projectTeamService, Lazy<ITeamRoleService> teamRoleService, Lazy<ISkillService> skillService, Lazy<ISystemRoleService> systemRoleService)
        {
            _repository = repository;
            _userService = userService;
            _projectTeamService = projectTeamService;
            _teamRoleService = teamRoleService;
            _skillService = skillService;
            _systemRoleService = systemRoleService;
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

        public async Task<IEnumerable<Project>> GetEmployeeProjects(string id)
        {
            var currentProjectsList = new List<Project>();

            var pastProjectsList = new List<Project>();

            var projectIDs = (await _userService.Value.Get(id)).ProjectIDs;

            if (projectIDs != null)
            {
                foreach (var projectID in projectIDs)
                {
                    var project = await _repository.Get(projectID);

                    var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                    if (projectTeam != null)
                    {
                        if (projectTeam.TeamMembers != null && projectTeam.TeamMembers.Count() > 0)
                        {
                            var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == id).FirstOrDefault();

                            if (teamMember.Active)
                            {
                                currentProjectsList.Add(project);
                            }
                            else
                            {
                                pastProjectsList.Add(project);
                            }
                        }                       
                    }                   
                }
            }

            return currentProjectsList.Concat(pastProjectsList);
        }

        public async Task<IEnumerable<Project>> GetDepartmentProjects(string id)
        {
            var departmentProjects = new List<Project>();  

            var usersByDepartment = await _userService.Value.GetUsersByDepartment(id);

            foreach (var user in usersByDepartment ?? Enumerable.Empty<User>())
            {
                var projectInformations = (await GetEmployeeProjects(user.Id)).CurrentProjects;

                foreach (var projectInformation in projectInformations ?? Enumerable.Empty<Project>())
                {
                    departmentProjects.Add(projectInformation);
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

        public async Task<IEnumerable<Project>> GetProjectsByProjectManager(string id)
        {
            return await _repository.Find("ProjectManagerID", id);
        }

        public async Task<IEnumerable<Project>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Project project)
        {
            var successProject = await _repository.Create(project);

            ProjectTeam projectTeam = new ProjectTeam { ProjectID = project.Id, TeamMembers = new List<TeamMembers>() };

            var successprojectTeam = await _projectTeamService.Value.Create(projectTeam);

            return successProject && successprojectTeam;
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
