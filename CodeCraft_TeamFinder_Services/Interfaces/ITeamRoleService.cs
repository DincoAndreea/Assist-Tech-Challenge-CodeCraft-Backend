using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface ITeamRoleService
    {
        Task<bool> Create(TeamRole teamRole);
        Task<bool> Delete(string id);
        Task<IEnumerable<TeamRole>> Find(string fieldName, string fieldValue);
        Task<TeamRole> Get(string id);
        Task<IEnumerable<TeamRole>> GetAll();
        Task<bool> Update(TeamRole teamRole);
    }
}