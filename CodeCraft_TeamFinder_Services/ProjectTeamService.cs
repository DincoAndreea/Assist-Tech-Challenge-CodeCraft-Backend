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

        public ProjectTeamService(IRepository<ProjectTeam> repository)
        {
            _repository = repository;
        }

        public async Task<ProjectTeam> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<ProjectTeam>> GetAll()
        {
            return await _repository.GetAll();
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
