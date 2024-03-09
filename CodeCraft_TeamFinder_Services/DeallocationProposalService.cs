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

        public DeallocationProposalService(IRepository<DeallocationProposal> repository)
        {
            _repository = repository;
        }

        public async Task<DeallocationProposal> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<DeallocationProposal>> GetAll()
        {
            return await _repository.GetAll();
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
