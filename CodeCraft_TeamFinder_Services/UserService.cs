using AutoMapper;
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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly Lazy<IOrganizationService> _organizationService;
        private readonly Lazy<ISystemRoleService> _systemRoleService;
        private readonly Lazy<IProjectTeamService> _projectTeamService;
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ISkillService> _skillService;

        public UserService(IRepository<User> repository,
                           Lazy<IOrganizationService> organizationService,
                           Lazy<ISystemRoleService> systemRoleService,
                           Lazy<IProjectTeamService> projectTeamService,
                           Lazy<IProjectService> projectService,
                           Lazy<ISkillService> skillService)
        {
            _repository = repository;
            _organizationService = organizationService;
            _systemRoleService = systemRoleService;
            _projectTeamService = projectTeamService;
            _projectService = projectService;
            _skillService = skillService;
        }

        public async Task<User> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Create(User user)
        {
            return await _repository.Create(user);
        }

        public async Task<IEnumerable<User>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Update(User user)
        {
            return await _repository.Update(user);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<User>> GetUsersByOrganization(string id)
        {
            return await _repository.Find("OrganizationID", id);
        }

        public async Task<IEnumerable<User>> GetUsersByDepartment(string id)
        {
            return await _repository.Find("DepartmentID", id);
        }

        public async Task<IEnumerable<User>> GetUsersByProject(string id)
        {
            return await _repository.Find("ProjectIDs", id);
        }

        public async Task<IEnumerable<User>> GetUsersBySkill(string id)
        {
            return await _repository.Find("SkillID", id);
        }

        public async Task<IEnumerable<TeamFinderResponseDTO>> TeamFinder(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            var users = await this.GetUsersByOrganization(teamFinderRequestDTO.OrganizationID);

            var usersPartiallyAvailableList = new List<TeamFinderResponseDTO>();
            var usersOnProjectsCloseToFinishList = new List<TeamFinderResponseDTO>();
            var usersUnavailableList = new List<TeamFinderResponseDTO>();
            var usersAvailableList = new List<TeamFinderResponseDTO>();

            if (teamFinderRequestDTO.PartiallyAvailable || teamFinderRequestDTO.Unavailable)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0);

                bool matchSkills = false;

                bool matchTeamRolesAndTechnologyOnPastProject = false;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var projectIDs = user.ProjectIDs;

                    var skills = user.Skills;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name) && matchSkills)
                            {
                                matchTeamRolesAndTechnologyOnPastProject = true;
                            }
                        }
                    }

                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var project = await _projectService.Value.Get(projectID);

                        var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                        if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                        {
                            matchSkills = true;
                        }

                        if (teamMember.Active)
                        {
                            workHours += teamMember.WorkHours;
                        }

                    }

                    if (workHours < 8 && matchSkills && matchTeamRolesAndTechnologyOnPastProject)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersPartiallyAvailableList.Add(member);
                    }

                    if (workHours == 8 && matchSkills && matchTeamRolesAndTechnologyOnPastProject && teamFinderRequestDTO.Unavailable)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersUnavailableList.Add(member);
                    }
                }
            }

            if (teamFinderRequestDTO.ProjectsCloseToFinish)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0);

                bool matchSkills = false;

                bool matchTeamRolesAndTechnologyOnPastProject = false;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var skills = user.Skills;

                    var projectIDs = user.ProjectIDs;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name) && matchSkills)
                            {
                                matchTeamRolesAndTechnologyOnPastProject = true;
                            }
                        }
                    }

                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var project = await _projectService.Value.Get(projectID);

                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                        if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                        {
                            matchSkills = true;
                        }

                        if (teamMember.Active)
                        {
                            workHours += teamMember.WorkHours;
                        }

                        if (project.DeadlineDate > DateTime.UtcNow)
                        {
                            TimeSpan? difference = project.DeadlineDate - DateTime.Now;

                            if (difference.Value.TotalDays / 7 < teamFinderRequestDTO.Weeks && matchSkills && matchTeamRolesAndTechnologyOnPastProject)
                            {
                                TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                                usersOnProjectsCloseToFinishList.Add(member);
                            }
                        }
                    }
                }
            }

            if (teamFinderRequestDTO.Available)
            {
                var usersAvailable = users.Where(x => x.ProjectIDs?.Count() == 0).ToList();

                foreach (var user in usersAvailable ?? Enumerable.Empty<User>())
                {
                    var skills = user.Skills;

                    var projectIDs = user.ProjectIDs;

                    bool matchSkills = false;

                    bool matchTeamRolesAndTechnologyOnPastProject = false;

                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var project = await _projectService.Value.Get(projectID);

                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                        if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                        {
                            matchSkills = true;
                        }
                    }

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name) && matchSkills)
                            {
                                matchTeamRolesAndTechnologyOnPastProject = true;
                            }
                        }
                    }

                    if (matchSkills && matchTeamRolesAndTechnologyOnPastProject)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = 0 };
                        usersAvailableList.Add(member);
                    }
                }
            }

            if (teamFinderRequestDTO.PartiallyAvailable)
            {
                if (teamFinderRequestDTO.ProjectsCloseToFinish)
                {
                    return usersPartiallyAvailableList.Concat(usersOnProjectsCloseToFinishList);
                }

                if (teamFinderRequestDTO.Unavailable)
                {
                    return usersPartiallyAvailableList.Concat(usersUnavailableList);
                }

                if (teamFinderRequestDTO.ProjectsCloseToFinish && teamFinderRequestDTO.Unavailable)
                {
                    return usersPartiallyAvailableList.Concat(usersOnProjectsCloseToFinishList.Concat(usersUnavailableList));
                }

                return usersPartiallyAvailableList;
            }

            if (teamFinderRequestDTO.ProjectsCloseToFinish)
            {
                if (teamFinderRequestDTO.Unavailable)
                {
                    return usersOnProjectsCloseToFinishList.Concat(usersUnavailableList);
                }

                return usersOnProjectsCloseToFinishList;
            }

            if (teamFinderRequestDTO.Unavailable)
            {
                return usersUnavailableList;
            }

            return usersAvailableList;
        }

        public async Task<IEnumerable<User>> GetDepartmentManagers(DepartmentManagersDTO departmentManagersDTO)
        {
            var departmentManagersList = new List<User>();

            var users = await GetUsersByOrganization(departmentManagersDTO.OrganizationID);

            if (users != null)
            {
                var systemRole = (await _systemRoleService.Value.Find("Name", "Department Manager")).First();

                if (departmentManagersDTO.Assigned)
                {
                    departmentManagersList = users.Where(x => x.DepartmentID != null && x.SystemRoleIDs.Contains(systemRole.Id)).ToList();
                }
                else
                {
                    departmentManagersList = users.Where(x => x.DepartmentID == null && x.SystemRoleIDs.Contains(systemRole.Id)).ToList();
                }
            }

            return departmentManagersList;
        }

        public async Task<IEnumerable<User>> GetProjectManagers(string id)
        {
            var projectManagersList = new List<User>();

            var users = await GetUsersByOrganization(id);

            if (users != null)
            {
                var systemRole = (await _systemRoleService.Value.Find("Name", "Project Manager")).First();

                projectManagersList = users.Where(x => x.SystemRoleIDs.Contains(systemRole.Id)).ToList();
            }

            return projectManagersList;
        }

        public async Task<IEnumerable<User>> GetEmployees(string id)
        {
            var employeesList = new List<User>();

            var users = await GetUsersByOrganization(id);

            if (users != null)
            {
                var systemRole = (await _systemRoleService.Value.Find("Name", "Employee")).First();

                employeesList = users.Where(x => x.SystemRoleIDs.Contains(systemRole.Id) && x.SystemRoleIDs.Count() == 1).ToList();
            }

            return employeesList;
        }

        public async Task<IEnumerable<User>> GetOrganizationAdmins(string id)
        {
            var organizationAdminsList = new List<User>();

            var users = await GetUsersByOrganization(id);

            if (users != null)
            {
                var systemRole = (await _systemRoleService.Value.Find("Name", "Organization Administrator")).First();

                organizationAdminsList = users.Where(x => x.SystemRoleIDs.Contains(systemRole.Id)).ToList();
            }

            return organizationAdminsList;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            IEnumerable<User> users = await _repository.GetAll();

            var user = users.Where(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password).FirstOrDefault();

            if (user != null)
            {
                LoginResponseDTO loginResponse = new LoginResponseDTO { Id = user.Id, Email = user.Email, Name = user.Name, OrganizationID = user.OrganizationID, DepartmentID = user.DepartmentID, ProjectIDs = user.ProjectIDs, Skills = user.Skills, SystemRoleIDs = user.SystemRoleIDs };

                return loginResponse;
            }

            return new LoginResponseDTO();
        }

        public async Task<bool> RegisterAdmin(RegisterAdminRequestDTO registerAdminRequest)
        {
            Organization organization = new Organization { Name = registerAdminRequest.OrganizationName, Address = registerAdminRequest.OrganizationAddress };

            bool organizationCreated = await _organizationService.Value.Create(organization);

            var systemRole = (await _systemRoleService.Value.Find("Name", "Organization Administrator")).First();

            if (organizationCreated)
            {
                User user = new User { Name = registerAdminRequest.Name, Email = registerAdminRequest.Email, Password = registerAdminRequest.Password, OrganizationID = organization.Id, SystemRoleIDs = new string[] { systemRole.Id } };
                bool adminCreated = await _repository.Create(user);

                return adminCreated;
            }

            return false;
        }

        public async Task<bool> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest)
        {
            var systemRole = (await _systemRoleService.Value.Find("Name", "Employee")).First();

            User user = new User { Name = registerEmployeeRequest.Name, Email = registerEmployeeRequest.Email, Password = registerEmployeeRequest.Password, OrganizationID = registerEmployeeRequest.OrganizationID, SystemRoleIDs = new string[] { systemRole.Id } };
            bool employeeCreated = await _repository.Create(user);

            return employeeCreated;
        }
    }
}
