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
        Task<IEnumerable<Skill>> GetSkillsByOrganization(string id);
        Task<IEnumerable<Skill>> GetSkillsBySkillCategory(string id);
        Task<IEnumerable<Skill>> GetSkillsByDepartment(string id);
        Task<IEnumerable<Skill>> GetSkillsByAuthor(string id);
        Task<SkillStatisticsResponseDTO> GetSkillStatistics(SkillStatisticsRequestDTO skillStatisticsRequest);
        Task<bool> Update(Skill skill);
    }
}