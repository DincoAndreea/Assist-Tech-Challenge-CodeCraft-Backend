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
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> _repository;

        public SkillService(IRepository<Skill> repository)
        {
            _repository = repository;
        }

        public async Task<Skill> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Skill>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Skill skill)
        {
            return await _repository.Create(skill);
        }

        public async Task<bool> Update(Skill skill)
        {
            return await _repository.Update(skill);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
