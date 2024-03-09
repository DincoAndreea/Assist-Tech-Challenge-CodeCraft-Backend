using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> Create(Organization organization);
        Task<bool> Delete(string id);
        Task<Organization> Get(string id);
        Task<IEnumerable<Organization>> Find(string fieldName, string fieldValue);
        Task<IEnumerable<Organization>> GetAll();
        Task<bool> Update(Organization organization);
    }
}