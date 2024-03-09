using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface ISkillService
    {
        Task<bool> Create(Skill skill);
        Task<bool> Delete(string id);
        Task<IEnumerable<Skill>> Find(string fieldName, string fieldValue);
        Task<Skill> Get(string id);
        Task<IEnumerable<Skill>> GetAll();
        Task<bool> Update(Skill skill);
    }
}