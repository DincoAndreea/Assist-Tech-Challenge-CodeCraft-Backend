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
    public class SystemRoleService : ISystemRoleService
    {
        private readonly IRepository<SystemRole> _repository;

        public SystemRoleService(IRepository<SystemRole> repository)
        {
            _repository = repository;
        }

        public async Task<SystemRole> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<SystemRole>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<SystemRole>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(SystemRole systemRole)
        {
            return await _repository.Create(systemRole);
        }

        public async Task<bool> Update(SystemRole systemRole)
        {
            return await _repository.Update(systemRole);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
