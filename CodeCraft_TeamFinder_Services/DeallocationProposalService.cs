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
    public class DeallocationProposalService : IDeallocationProposalService
    {
        private readonly IRepository<DeallocationProposal> _repository;
        private readonly Lazy<IUserService> _userService;

        public DeallocationProposalService(IRepository<DeallocationProposal> repository, Lazy<IUserService> userService)
        {
            _repository = repository;
            _userService = userService;
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

        public async Task<IEnumerable<DeallocationProposal>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(DeallocationProposal deallocationProposal)
        {
            return await _repository.Create(deallocationProposal);
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
