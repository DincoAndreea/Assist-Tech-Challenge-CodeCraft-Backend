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

        public AssignmentProposalService(IRepository<AssignmentProposal> repository)
        {
            _repository = repository;
        }

        public async Task<AssignmentProposal> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<AssignmentProposal>> GetAll()
        {
            return await _repository.GetAll();
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
