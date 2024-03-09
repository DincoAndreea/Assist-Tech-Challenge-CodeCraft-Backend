using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface ISkillCategoryService
    {
        Task<bool> Create(SkillCategory skillCategory);
        Task<bool> Delete(string id);
        Task<IEnumerable<SkillCategory>> Find(string fieldName, string fieldValue);
        Task<SkillCategory> Get(string id);
        Task<IEnumerable<SkillCategory>> GetAll();
        Task<bool> Update(SkillCategory skillCategory);
    }
}