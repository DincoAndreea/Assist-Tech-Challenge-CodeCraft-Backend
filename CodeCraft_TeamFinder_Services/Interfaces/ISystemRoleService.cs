using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface ISystemRoleService
    {
        Task<bool> Create(SystemRole systemRole);
        Task<bool> Delete(string id);
        Task<SystemRole> Get(string id);
        Task<IEnumerable<SystemRole>> GetAll();
        Task<IEnumerable<SystemRole>> Find(string fieldName, string fieldValue);
        Task<bool> Update(SystemRole systemRole);
    }
}