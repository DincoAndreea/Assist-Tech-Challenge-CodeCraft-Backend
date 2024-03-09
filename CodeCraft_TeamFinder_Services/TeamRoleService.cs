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
    public class TeamRoleService : ITeamRoleService
    {
        private readonly IRepository<TeamRole> _repository;

        public TeamRoleService(IRepository<TeamRole> repository)
        {
            _repository = repository;
        }

        public async Task<TeamRole> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<TeamRole>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<TeamRole>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(TeamRole teamRole)
        {
            return await _repository.Create(teamRole);
        }

        public async Task<bool> Update(TeamRole teamRole)
        {
            return await _repository.Update(teamRole);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
