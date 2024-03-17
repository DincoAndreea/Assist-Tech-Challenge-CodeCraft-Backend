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

namespace CodeCraft_TeamFinder_Services
{
    public class AssignmentProposalService : IAssignmentProposalService
    {
        private readonly IRepository<AssignmentProposal> _repository;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IProjectTeamService> _projectTeamService;
        private readonly Lazy<IDepartmentService> _departmentService;

        public AssignmentProposalService(IRepository<AssignmentProposal> repository, Lazy<IUserService> userService, Lazy<IProjectTeamService> projectTeamService, Lazy<IDepartmentService> departmentService)
        {
            _repository = repository;
            _userService = userService;
            _projectTeamService = projectTeamService;
            _departmentService = departmentService;
        }

        public async Task<AssignmentProposal> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<AssignmentProposal>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<AssignmentProposal>> GetAssignmentProposalsByDepartmentManager(string id)
        {
            var manager = await _userService.Value.Get(id);

            var assignmentProposals = await _repository.GetAll();

            var assignmentProposalsByDepartmentManager = new List<AssignmentProposal>();

            if (manager != null)
            {
                var usersByDepartment = await _userService.Value.GetUsersByDepartment(manager.DepartmentID);

                foreach (var user in usersByDepartment ?? Enumerable.Empty<User>())
                {
                    foreach (var assignmentProposal in assignmentProposals ?? Enumerable.Empty<AssignmentProposal>())
                    {
                        if (assignmentProposal.UserID == user.Id)
                        {
                            assignmentProposalsByDepartmentManager.Add(assignmentProposal);
                        }
                    }
                }
            }

            return assignmentProposalsByDepartmentManager;
        }

        public async Task<IEnumerable<AssignmentProposal>> GetAssignmentProposalsByProject(string id)
        {
            return await _repository.Find("ProjectID", id);
        }

        public async Task<bool> AcceptAssignmentProposal(string id)
        {
            var assignmentProposal = await _repository.Get(id);

            if (assignmentProposal != null)
            {
                assignmentProposal.Accepted = true;

                bool assignmentProposalUpdated = await _repository.Update(assignmentProposal);

                var user = await _userService.Value.Get(assignmentProposal.UserID);

                if (user != null)
                {
                    if (user.ProjectIDs == null)
                    {
                        user.ProjectIDs = new List<string>() { assignmentProposal.ProjectID };
                    }

                    user.ProjectIDs.Add(assignmentProposal.ProjectID);

                    bool userUpdated = await _userService.Value.Update(user);

                    var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(assignmentProposal.ProjectID)).FirstOrDefault();

                    if (projectTeam != null)
                    {
                        TeamMembers newTeamMember = new TeamMembers { UserID = assignmentProposal.UserID, TeamRoleIDs = assignmentProposal.TeamRoleIDs, Active = true, WorkHours = assignmentProposal.WorkHours };

                        if (projectTeam.TeamMembers == null)
                        {
                            projectTeam.TeamMembers = new List<TeamMembers> { newTeamMember };
                        }

                        var userExists = projectTeam.TeamMembers.Select(x => x.UserID == assignmentProposal.UserID).FirstOrDefault();

                        if (!userExists)
                        {
                            projectTeam.TeamMembers.Add(newTeamMember);

                            bool projectTeamUpdated = await _projectTeamService.Value.Update(projectTeam);

                            if (assignmentProposalUpdated && userUpdated && projectTeamUpdated)
                            {
                                return true;
                            }
                        }
                        
                    }
                }
            }

            return false;
        }

        public async Task<IEnumerable<AssignmentProposal>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(AssignmentProposal assignmentProposal)
        {
            bool success = await _repository.Create(assignmentProposal);

            var user = await _userService.Value.Get(assignmentProposal.UserID);

            if (user.DepartmentID != null && success)
            {
                var department = await _departmentService.Value.Get(user.DepartmentID);

                if (department != null)
                {
                    var manager = department.ManagerID != null ? await _userService.Value.Get(department.ManagerID) : null;

                    if (manager != null)
                    {
                        string fromAddress = "dincoandreea@gmail.com";
                        string toAddress = "dincoandreea@gmail.com";
                        string subject = "Assignment Proposal";
                        string body = "A member of your department has been proposed to be assigned to a project. Check the proposal in the app.";

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

            return success;
        }

        public async Task<bool> Update(AssignmentProposal assignmentProposal)
        {
            return await _repository.Update(assignmentProposal);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
