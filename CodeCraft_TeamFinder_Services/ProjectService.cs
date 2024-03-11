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
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;

        public ProjectService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<Project> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Project>> GetProjectByOrganization(string id)
        {
            return await _repository.Find("OrganizationID", id);
        }

        public async Task<IEnumerable<Project>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Project project)
        {
            return await _repository.Create(project);
        }

        public async Task<bool> Update(Project project)
        {
            return await _repository.Update(project);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
