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
    public class DeallocationProposalService : IDeallocationProposalService
    {
        private readonly IRepository<DeallocationProposal> _repository;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IProjectTeamService> _projectTeamService;
        private readonly Lazy<IDepartmentService> _departmentService;

        public DeallocationProposalService(IRepository<DeallocationProposal> repository, Lazy<IUserService> userService, Lazy<IProjectTeamService> projectTeamService, Lazy<IDepartmentService> departmentService)
        {
            _repository = repository;
            _userService = userService;
            _projectTeamService = projectTeamService;
            _departmentService = departmentService;
        }

        public async Task<DeallocationProposal> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<DeallocationProposal>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<DeallocationProposal>> GetDeallocationProposalsByDepartmentManager(string id)
        {
            var manager = await _userService.Value.Get(id);

            var deallocationProposals = await _repository.GetAll();

            var deallocationProposalsByDepartmentManager = new List<DeallocationProposal>();

            if (manager != null)
            {
                var usersByDepartment = await _userService.Value.GetUsersByDepartment(manager.DepartmentID);

                foreach (var user in usersByDepartment ?? Enumerable.Empty<User>())
                {
                    foreach (var deallocationProposal in deallocationProposals ?? Enumerable.Empty<DeallocationProposal>())
                    {
                        if (deallocationProposal.UserID == user.Id)
                        {
                            deallocationProposalsByDepartmentManager.Add(deallocationProposal);
                        }
                    }
                }
            }

            return deallocationProposalsByDepartmentManager;
        }

        public async Task<IEnumerable<DeallocationProposal>> GetDeallocationProposalsByProject(string id)
        {
            return await _repository.Find("ProjectID", id);
        }

        public async Task<bool> AcceptDealllocationProposal(string id)
        {
            var deallocationProposal = await _repository.Get(id);

            if (deallocationProposal != null)
            {
                deallocationProposal.Accepted = true;

                bool deallocationProposalUpdated = await _repository.Update(deallocationProposal);

                var projectTeam = (await _projectTeamService.Value.GetProjectTeamByProject(deallocationProposal.ProjectID)).FirstOrDefault();

                if (projectTeam != null)
                {
                    if (projectTeam.TeamMembers != null && projectTeam.TeamMembers.Count > 0)
                    {
                        var teamMember = projectTeam.TeamMembers.Where(x => x.UserID == deallocationProposal.UserID).FirstOrDefault();

                        projectTeam.TeamMembers.Remove(teamMember);

                        teamMember.Active = false;

                        projectTeam.TeamMembers.Add(teamMember);

                        bool projectTeamUpdated = await _projectTeamService.Value.Update(projectTeam);

                        if (projectTeamUpdated && deallocationProposalUpdated)
                        {
                            return true;
                        }
                    }                    
                }
            }

            return false;
        }


        public async Task<IEnumerable<DeallocationProposal>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(DeallocationProposal deallocationProposal)
        {
            bool success = await _repository.Create(deallocationProposal);

            var user = await _userService.Value.Get(deallocationProposal.UserID);

            if (user.DepartmentID != null && success)
            {
                var department = await _departmentService.Value.Get(user.DepartmentID);

                if (department.ManagerID != null)
                {
                    var manager = await _userService.Value.Get(department.ManagerID);


                    string fromAddress = "dincoandreea@gmail.com";
                    string toAddress = "dincoandreea@gmail.com";
                    string subject = "Deallocation Proposal";
                    string body = "A member of your department has been proposed to be deallocated from a project. Check the proposal in the app.";

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

            return success;
        }

        public async Task<bool> Update(DeallocationProposal deallocationProposal)
        {
            return await _repository.Update(deallocationProposal);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
