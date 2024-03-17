using AutoMapper;
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using static System.Environment;
using Azure.AI.OpenAI;
using Azure;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

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
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<ITeamRoleService> _teamRoleService;
        private readonly Lazy<ISkillCategoryService> _skillCategoryService;

        public UserService(IRepository<User> repository, Lazy<IOrganizationService> organizationService, Lazy<ISystemRoleService> systemRoleService, Lazy<IProjectTeamService> projectTeamService, Lazy<IProjectService> projectService, Lazy<ISkillService> skillService, Lazy<IDepartmentService> departmentService, Lazy<ITeamRoleService> teamRoleService, Lazy<ISkillCategoryService> skillCategoryService)
        {
            _repository = repository;
            _organizationService = organizationService;
            _systemRoleService = systemRoleService;
            _projectTeamService = projectTeamService;
            _projectService = projectService;
            _skillService = skillService;
            _departmentService = departmentService;
            _teamRoleService = teamRoleService;
            _skillCategoryService = skillCategoryService;
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
            bool success = await _repository.Create(user);

            if (user.DepartmentID != null && success)
            {
                var department = await _departmentService.Value.Get(user.DepartmentID);

                if (department != null)
                {
                    var manager = department.ManagerID != null ? await _repository.Get(department.ManagerID) : null;

                    if (user.Skills != null && user.Skills.Count() > 0 && manager != null)
                    {
                        foreach (var skill in user.Skills)
                        {
                            string fromAddress = "dincoandreea@gmail.com";
                            string toAddress = "dincoandreea@gmail.com";
                            string subject = "Skill Validation";
                            string body = $"{user.Name} added a skill to their profile that need your approval. Check the request in the app.";

                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromAddress, "epmk ojno vjgh swgn "),
                                EnableSsl = true,
                            };

                            using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body))
                            {
                                smtpClient.Send(mailMessage);
                            }
                        }
                    }
                }
            }

            return success;
        }

        public async Task<IEnumerable<User>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Update(User user)
        {
            bool success = await _repository.Update(user);

            if (user.DepartmentID != null && success)
            {
                var department = await _departmentService.Value.Get(user.DepartmentID);

                if (department != null)
                {
                    var manager = department.ManagerID != null ? await _repository.Get(department.ManagerID) : null;

                    if (user.Skills != null && user.Skills.Count() > 0 && manager != null)
                    {
                        var skillsPending = user.Skills.Where(x => x.Status == "Pending").ToList();

                        foreach (var skills in skillsPending)
                        {
                            string fromAddress = "dincoandreea@gmail.com";
                            string toAddress = "dincoandreea@gmail.com";
                            string subject = "Skill Validation";
                            string body = $"{user.Name} added a skill to their profile that need your approval. Check the request in the app.";

                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromAddress, "epmk ojno vjgh swgn "),
                                EnableSsl = true,
                            };

                            using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body))
                            {
                                smtpClient.Send(mailMessage);
                            }
                        }
                    }
                }
            }

            return success;
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

        private async Task<List<TeamFinderResponseDTO>> GetPartiallyAvailableEmployees(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            var users = await this.GetUsersByOrganization(teamFinderRequestDTO.OrganizationID);

            var usersPartiallyAvailableList = new List<TeamFinderResponseDTO>();

            if (teamFinderRequestDTO.PartiallyAvailable)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0).ToList();

                int matchSkills = 0;

                int matchTeamRolesAndTechnologyOnPastProject = 0;

                var matchSkillRequirements = 0;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var projectIDs = user.ProjectIDs;

                    var skills = user.Skills;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        var level = skill.Level == "Learns" ? 1 : skill.Level == "Knows" ? 2 : skill.Level == "Does" ? 3 : skill.Level == "Helps" ? 4 : 5;

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name))
                            {
                                matchSkills++;
                            }

                            if (teamFinderRequestDTO.SkillRequirements != null)
                            {
                                matchSkillRequirements += teamFinderRequestDTO.SkillRequirements.Where(x => x.SkillID == skillObject.Id
                                                          && (x.MinimumLevel == "Learns" ? 1 : x.MinimumLevel == "Knows" ? 2 : x.MinimumLevel == "Does" ? 3 : x.MinimumLevel == "Helps" ? 4 : 5) <= level).ToList().Count();
                            }
                        }
                    }

                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var project = await _projectService.Value.Get(projectID);

                        if (project != null && projectTeam != null)
                        {
                            if (projectTeam.TeamMembers != null)
                            {
                                var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                                if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                                {
                                    matchTeamRolesAndTechnologyOnPastProject++;
                                }

                                if (teamMember.Active)
                                {
                                    workHours += teamMember.WorkHours;
                                }
                            }
                        }
                    }

                    if (workHours < 8 && (matchSkills > 0 || matchSkillRequirements > 0) && matchTeamRolesAndTechnologyOnPastProject > 0 && teamFinderRequestDTO.PastExperience)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersPartiallyAvailableList.Add(member);
                    }

                    if ((workHours < 8 && (matchSkills > 0 || matchSkillRequirements > 0) && !teamFinderRequestDTO.PastExperience) || (workHours < 8 && matchTeamRolesAndTechnologyOnPastProject > 0 && !teamFinderRequestDTO.PastExperience))
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersPartiallyAvailableList.Add(member);
                    }
                }
            }

            return usersPartiallyAvailableList;
        }

        private async Task<List<TeamFinderResponseDTO>> GetEmployeesOnProjectsCloseToFinish(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            var users = await this.GetUsersByOrganization(teamFinderRequestDTO.OrganizationID);

            var usersOnProjectsCloseToFinishList = new List<TeamFinderResponseDTO>();

            if (teamFinderRequestDTO.ProjectsCloseToFinish)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0).ToList();

                int matchSkills = 0;

                int matchTeamRolesAndTechnologyOnPastProject = 0;

                int projectsCloseToFinish = 0;

                var matchSkillRequirements = 0;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var projectIDs = user.ProjectIDs;

                    var skills = user.Skills;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        var level = skill.Level == "Learns" ? 1 : skill.Level == "Knows" ? 2 : skill.Level == "Does" ? 3 : skill.Level == "Helps" ? 4 : 5;

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name))
                            {
                                matchSkills++;
                            }

                            if (teamFinderRequestDTO.SkillRequirements != null)
                            {
                                matchSkillRequirements += teamFinderRequestDTO.SkillRequirements.Where(x => x.SkillID == skillObject.Id
                                                          && (x.MinimumLevel == "Learns" ? 1 : x.MinimumLevel == "Knows" ? 2 : x.MinimumLevel == "Does" ? 3 : x.MinimumLevel == "Helps" ? 4 : 5) <= level).ToList().Count();
                            }
                        }
                    }

                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var project = await _projectService.Value.Get(projectID);

                        if (project != null && projectTeam != null)
                        {
                            if (projectTeam.TeamMembers != null)
                            {
                                var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                                if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                                {
                                    matchTeamRolesAndTechnologyOnPastProject++;
                                }

                                if (teamMember.Active)
                                {
                                    workHours += teamMember.WorkHours;
                                }
                            }

                            if (project.DeadlineDate > DateTime.UtcNow)
                            {
                                TimeSpan? difference = project.DeadlineDate - DateTime.Now;

                                if (difference.Value.TotalDays / 7 < teamFinderRequestDTO.Weeks)
                                {
                                    projectsCloseToFinish++;
                                }
                            }
                        }
                    }

                    if (projectsCloseToFinish > 0 && (matchSkills > 0 || matchSkillRequirements > 0) && matchTeamRolesAndTechnologyOnPastProject > 0 && teamFinderRequestDTO.PastExperience)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersOnProjectsCloseToFinishList.Add(member);
                    }

                    if ((projectsCloseToFinish > 0 && (matchSkills > 0 || matchSkillRequirements > 0) && !teamFinderRequestDTO.PastExperience) || (projectsCloseToFinish > 0 && matchTeamRolesAndTechnologyOnPastProject > 0 && !teamFinderRequestDTO.PastExperience))
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersOnProjectsCloseToFinishList.Add(member);
                    }
                }
            }

            return usersOnProjectsCloseToFinishList;
        }

        private async Task<List<TeamFinderResponseDTO>> GetUnavailableEmployees(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            var users = await this.GetUsersByOrganization(teamFinderRequestDTO.OrganizationID);

            var usersUnavailableList = new List<TeamFinderResponseDTO>();

            if (teamFinderRequestDTO.Unavailable)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0).ToList();

                int matchSkills = 0;

                int matchTeamRolesAndTechnologyOnPastProject = 0;

                var matchSkillRequirements = 0;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var projectIDs = user.ProjectIDs;

                    var skills = user.Skills;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        var level = skill.Level == "Learns" ? 1 : skill.Level == "Knows" ? 2 : skill.Level == "Does" ? 3 : skill.Level == "Helps" ? 4 : 5;

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name))
                            {
                                matchSkills++;
                            }

                            if (teamFinderRequestDTO.SkillRequirements != null)
                            {
                                matchSkillRequirements += teamFinderRequestDTO.SkillRequirements.Where(x => x.SkillID == skillObject.Id
                                                          && (x.MinimumLevel == "Learns" ? 1 : x.MinimumLevel == "Knows" ? 2 : x.MinimumLevel == "Does" ? 3 : x.MinimumLevel == "Helps" ? 4 : 5) <= level).ToList().Count();
                            }
                        }
                    }


                    foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                    {
                        var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                        var project = await _projectService.Value.Get(projectID);

                        if (project != null && projectTeam != null)
                        {
                            if (projectTeam.TeamMembers != null)
                            {
                                var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                                if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                                {
                                    matchTeamRolesAndTechnologyOnPastProject++;
                                }

                                if (teamMember.Active)
                                {
                                    workHours += teamMember.WorkHours;
                                }
                            }
                        }
                    }

                    if (workHours == 8 && (matchSkills > 0 || matchSkillRequirements > 0) && matchTeamRolesAndTechnologyOnPastProject > 0 && teamFinderRequestDTO.PastExperience)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersUnavailableList.Add(member);
                    }

                    if ((workHours == 8 && (matchSkills > 0 || matchSkillRequirements > 0) && !teamFinderRequestDTO.PastExperience) || (workHours == 8 && matchTeamRolesAndTechnologyOnPastProject > 0 && !teamFinderRequestDTO.PastExperience))
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersUnavailableList.Add(member);
                    }
                }
            }

            return usersUnavailableList;
        }

        private async Task<List<TeamFinderResponseDTO>> GetAvailableEmployees(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            var users = await this.GetUsersByOrganization(teamFinderRequestDTO.OrganizationID);

            var usersAvailableList = new List<TeamFinderResponseDTO>();

            if (teamFinderRequestDTO.Available)
            {
                var usersWorkingOnProjects = users.Where(x => x.ProjectIDs?.Count() > 0 || x.ProjectIDs == null || x.ProjectIDs.Count() == 0).ToList();

                int matchSkills = 0;

                int matchTeamRolesAndTechnologyOnPastProject = 0;

                var matchSkillRequirements = 0;

                foreach (var user in usersWorkingOnProjects ?? Enumerable.Empty<User>())
                {
                    int workHours = 0;

                    var projectIDs = user.ProjectIDs;

                    var skills = user.Skills;

                    foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                    {
                        var skillID = skill.SkillID;

                        var skillObject = await _skillService.Value.Get(skillID);

                        var level = skill.Level == "Learns" ? 1 : skill.Level == "Knows" ? 2 : skill.Level == "Does" ? 3 : skill.Level == "Helps" ? 4 : 5;

                        if (skillObject != null)
                        {
                            if (teamFinderRequestDTO.TechnologyStack.Contains(skillObject.Name))
                            {
                                matchSkills++;
                            }

                            if (teamFinderRequestDTO.SkillRequirements != null)
                            {
                                matchSkillRequirements += teamFinderRequestDTO.SkillRequirements.Where(x => x.SkillID == skillObject.Id
                                                          && (x.MinimumLevel == "Learns" ? 1 : x.MinimumLevel == "Knows" ? 2 : x.MinimumLevel == "Does" ? 3 : x.MinimumLevel == "Helps" ? 4 : 5) <= level).ToList().Count();
                            }                            
                        }
                    }

                    if (projectIDs != null)
                    {
                        foreach (var projectID in projectIDs ?? Enumerable.Empty<string>())
                        {
                            var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectID)).FirstOrDefault();

                            var project = await _projectService.Value.Get(projectID);

                            if (project != null && projectTeam != null)
                            {
                                if (projectTeam.TeamMembers != null)
                                {
                                    var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == user.Id).FirstOrDefault();

                                    if (!teamMember.Active && teamMember.TeamRoleIDs.Intersect(teamFinderRequestDTO.TeamRoleIDs).Count() > 0 && teamFinderRequestDTO.TechnologyStack.Intersect(project.TechnologyStack).Count() > 0)
                                    {
                                        matchTeamRolesAndTechnologyOnPastProject++;
                                    }

                                    if (teamMember.Active)
                                    {
                                        workHours += teamMember.WorkHours;
                                    }
                                }
                            }
                        }
                    }

                    if (workHours == 0 && (matchSkills > 0 || matchSkillRequirements > 0) && matchTeamRolesAndTechnologyOnPastProject > 0 && teamFinderRequestDTO.PastExperience)
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersAvailableList.Add(member);
                    }

                    if ((workHours == 0 && (matchSkills > 0 || matchSkillRequirements > 0) && !teamFinderRequestDTO.PastExperience) || (workHours == 0 && matchTeamRolesAndTechnologyOnPastProject > 0 && !teamFinderRequestDTO.PastExperience))
                    {
                        TeamFinderResponseDTO member = new TeamFinderResponseDTO { User = user, WorkHours = workHours };
                        usersAvailableList.Add(member);
                    }
                }
            }

            return usersAvailableList;
        }

        public async Task<IEnumerable<TeamFinderResponseDTO>> TeamFinder(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            if (teamFinderRequestDTO.PartiallyAvailable)
            {
                if (teamFinderRequestDTO.ProjectsCloseToFinish)
                {
                    return (await this.GetPartiallyAvailableEmployees(teamFinderRequestDTO)).Concat(await this.GetEmployeesOnProjectsCloseToFinish(teamFinderRequestDTO));
                }

                if (teamFinderRequestDTO.Unavailable)
                {
                    return (await this.GetPartiallyAvailableEmployees(teamFinderRequestDTO)).Concat(await this.GetUnavailableEmployees(teamFinderRequestDTO));
                }

                if (teamFinderRequestDTO.ProjectsCloseToFinish && teamFinderRequestDTO.Unavailable)
                {
                    return (await this.GetPartiallyAvailableEmployees(teamFinderRequestDTO)).Concat(await this.GetEmployeesOnProjectsCloseToFinish(teamFinderRequestDTO)).Concat(await this.GetUnavailableEmployees(teamFinderRequestDTO));
                }

                return await this.GetPartiallyAvailableEmployees(teamFinderRequestDTO);
            }

            if (teamFinderRequestDTO.ProjectsCloseToFinish)
            {
                if (teamFinderRequestDTO.Unavailable)
                {
                    return (await this.GetEmployeesOnProjectsCloseToFinish(teamFinderRequestDTO)).Concat(await this.GetUnavailableEmployees(teamFinderRequestDTO));
                }

                return await this.GetEmployeesOnProjectsCloseToFinish(teamFinderRequestDTO);
            }

            if (teamFinderRequestDTO.Unavailable)
            {
                return await this.GetUnavailableEmployees(teamFinderRequestDTO);
            }

            return await this.GetAvailableEmployees(teamFinderRequestDTO);
        }

        public async Task<string> TeamFinderOpenAI(TeamFinderOpenAI teamFinderOpenAI)
        {
            var teamfinder = new List<TeamFinderResponseDTO>();

            string completion = "";

            var teamFinderResponse = Newtonsoft.Json.JsonConvert.SerializeObject(teamfinder);

            var projectJson = Newtonsoft.Json.JsonConvert.SerializeObject(teamFinderOpenAI.Project);

            var users = await this.GetUsersByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var usersJson = Newtonsoft.Json.JsonConvert.SerializeObject(users);

            var projects = await _projectService.Value.GetProjectsByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var projectsJson = Newtonsoft.Json.JsonConvert.SerializeObject(await _projectService.Value.GetProjectsByOrganization(teamFinderOpenAI.Project.OrganizationID));

            var projectTeams = new List<ProjectTeam>();

            foreach (var projectObject in projects ?? Enumerable.Empty<Project>())
            {
                if (projectObject != null)
                {
                    var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(projectObject.Id)).FirstOrDefault();

                    if (projectTeam != null)
                    {
                        projectTeams.Add(projectTeam);
                    }                    
                }                
            }

            var projectTeamsJson = Newtonsoft.Json.JsonConvert.SerializeObject(projectTeams);

            var skills = await _skillService.Value.GetSkillsByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var skillsJson = Newtonsoft.Json.JsonConvert.SerializeObject(skills);

            var teamRoles = await _teamRoleService.Value.GetTeamRolesByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var teamRolesJson = Newtonsoft.Json.JsonConvert.SerializeObject(teamRoles);

            var skillCategories = await _skillCategoryService.Value.GetSkillCategoryByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var skillCategoriesJson = Newtonsoft.Json.JsonConvert.SerializeObject(skillCategories);

            var departments = await _departmentService.Value.GetDepartmentsByOrganization(teamFinderOpenAI.Project.OrganizationID);

            var departmentsJson = Newtonsoft.Json.JsonConvert.SerializeObject(departments);

            var endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT", EnvironmentVariableTarget.User);
            var key = GetEnvironmentVariable("AZURE_OPENAI_API_KEY", EnvironmentVariableTarget.User);

            var prompts = new List<string>();

            prompts.Add("I want to create a team for my project which include these details: " + projectJson +
                        ".\nHere are some additional context I want to add: " + teamFinderOpenAI.AdditionalContext +
                        ".\nBecause I have a limited amount of tokens I will have to split the context for you so please remember all the data I send you and after I send you everything analyze the data and make a list of employees that meet the criteria. " +
                        "Here is the data in my database which has 7 MongoDB collections: ");
            prompts.Add("\nUsers: " + usersJson);
            prompts.Add("\nProjects: " + projectsJson);
            prompts.Add("\nProject teams: " + projectTeamsJson);
            prompts.Add("\nSkills: " + skillsJson);
            prompts.Add("\nTeam roles: " + teamRolesJson);
            prompts.Add("\nSkill categories: " + skillCategoriesJson);
            prompts.Add("\nDepartments: " + departmentsJson);
            prompts.Add("\nI want the response to be a list of employees that fit these criteria and the response should be in JSON format and have this format: \n" + teamFinderResponse);

            completion += prompts;

            if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(key))
            {
                var client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));



                CompletionsOptions completionsOptions = new()
                {
                    DeploymentName = "atc-2024-gpt-35-turbo",
                    Temperature = 0.1f,
                    MaxTokens = 100,
                    FrequencyPenalty = 1.0f,                    
                };

                Response<Completions> completionsResponse = client.GetCompletions(completionsOptions);

                foreach (var prompt in prompts)
                {
                    // Set the prompt in the completion options
                    completionsOptions.Prompts.Clear();
                    completionsOptions.Prompts.Add(prompt);

                    // Send the request to OpenAI and retrieve completions
                    completionsResponse = client.GetCompletions(completionsOptions);

                    // Append completion text to the result
                    completion += completionsResponse.Value.Choices[0].Text;
                }
                
                Console.WriteLine($"Chatbot: {completion}");
            }

            return completion;
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

        public async Task<IEnumerable<User>> GetEmployeesWithoutDepartment(string id)
        {
            var employeesList = new List<User>();

            var users = await GetUsersByOrganization(id);

            if (users != null)
            {
                var systemRole = (await _systemRoleService.Value.Find("Name", "Employee")).First();

                employeesList = users.Where(x => x.SystemRoleIDs.Contains(systemRole.Id) && x.SystemRoleIDs.Count() == 1 && x.DepartmentID == null).ToList();
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

            var emailUsed = await _repository.Find("Email", registerAdminRequest.Email);

            if (emailUsed == null)
            {
                if (organizationCreated)
                {
                    User user = new User { Name = registerAdminRequest.Name, Email = registerAdminRequest.Email, Password = registerAdminRequest.Password, OrganizationID = organization.Id, SystemRoleIDs = new List<string> { systemRole.Id } };

                    bool adminCreated = await _repository.Create(user);

                    return adminCreated;
                }
            }

            return false;
        }

        public async Task<bool> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest)
        {
            var systemRole = (await _systemRoleService.Value.Find("Name", "Employee")).First();

            var emailUsed = await _repository.Find("Email", registerEmployeeRequest.Email);

            if (emailUsed == null)
            {
                User user = new User { Name = registerEmployeeRequest.Name, Email = registerEmployeeRequest.Email, Password = registerEmployeeRequest.Password, OrganizationID = registerEmployeeRequest.OrganizationID, SystemRoleIDs = new List<string> { systemRole.Id } };

                bool employeeCreated = await _repository.Create(user);

                return employeeCreated;
            }

            return false;
        }
    }
}
