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
    public class AssignmentProposalService : IAssignmentProposalService
    {
        private readonly IRepository<AssignmentProposal> _repository;
        private readonly Lazy<IUserService> _userService;

        public AssignmentProposalService(IRepository<AssignmentProposal> repository, Lazy<IUserService> userService)
        {
            _repository = repository;
            _userService = userService;
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

        public async Task<IEnumerable<AssignmentProposal>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(AssignmentProposal assignmentProposal)
        {
            return await _repository.Create(assignmentProposal);
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
