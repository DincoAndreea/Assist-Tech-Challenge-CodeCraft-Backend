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
    public class ProjectTeamService : IProjectTeamService
    {
        private readonly IRepository<ProjectTeam> _repository;
        private readonly Lazy<IAssignmentProposalService> _assignmentProposalService;
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<IUserService> _userService;

        public ProjectTeamService(IRepository<ProjectTeam> repository,
                                  Lazy<IAssignmentProposalService> assignmentProposalService,
                                  Lazy<IProjectService> projectService,
                                  Lazy<IUserService> userService)
        {
            _repository = repository;
            _assignmentProposalService = assignmentProposalService;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<ProjectTeam> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<ProjectTeam>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<ProjectTeam>> GetProjectTeamByProject(string id)
        {
            return await _repository.Find("ProjectID", id);
        }

        public async Task<ProjectTeamMembersDTO> GetProjectTeamMembers(string id)
        {
            var pastMembersList = new List<User>();
            var activeMembersList = new List<User>();
            var proposedMembersList = new List<User>();

            var project = (await this.GetProjectTeamByProject(id)).FirstOrDefault();

            var assignmentProposals = await _assignmentProposalService.Value.GetAssignmentProposalsByProject(id);

            foreach (var teamMember in project.TeamMembers ?? Enumerable.Empty<TeamMembers>())
            {
                var user = await _userService.Value.Get(teamMember.UserID);

                if (teamMember.Active)
                {

                    activeMembersList.Add(user);
                }
                else
                {
                    pastMembersList.Add(user);
                }
            }

            foreach (var assignmentProposal in assignmentProposals ?? Enumerable.Empty<AssignmentProposal>())
            {
                if (!assignmentProposal.Accepted)
                {
                    var user = await _userService.Value.Get(assignmentProposal.UserID);

                    proposedMembersList.Add(user);
                }
            }

            ProjectTeamMembersDTO projectTeamMembersDTO = new ProjectTeamMembersDTO { ActiveMembers = activeMembersList, PastMembers = pastMembersList, ProposedMembers = proposedMembersList };

            return projectTeamMembersDTO;
        }

        public async Task<IEnumerable<ProjectTeam>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(ProjectTeam projectTeam)
        {
            return await _repository.Create(projectTeam);
        }

        public async Task<bool> Update(ProjectTeam projectTeam)
        {
            return await _repository.Update(projectTeam);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
