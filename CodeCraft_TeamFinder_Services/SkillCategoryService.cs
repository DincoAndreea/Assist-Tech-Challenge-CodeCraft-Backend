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
    public class SkillCategoryService : ISkillCategoryService
    {
        private readonly IRepository<SkillCategory> _repository;

        public SkillCategoryService(IRepository<SkillCategory> repository)
        {
            _repository = repository;
        }

        public async Task<SkillCategory> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<SkillCategory>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<SkillCategory>> GetSkillCategoryByOrganization(string id)
        {
            return await _repository.Find("OrganizationID", id);
        }

        public async Task<IEnumerable<SkillCategory>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(SkillCategory skillCategory)
        {
            return await _repository.Create(skillCategory);
        }

        public async Task<bool> Update(SkillCategory skillCategory)
        {
            return await _repository.Update(skillCategory);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
