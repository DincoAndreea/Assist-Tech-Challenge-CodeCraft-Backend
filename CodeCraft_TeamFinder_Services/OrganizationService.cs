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
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organization> _repository;

        public OrganizationService(IRepository<Organization> repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Organization>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Organization organization)
        {
            return await _repository.Create(organization);
        }

        public async Task<bool> Update(Organization organization)
        {
            return await _repository.Update(organization);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}

