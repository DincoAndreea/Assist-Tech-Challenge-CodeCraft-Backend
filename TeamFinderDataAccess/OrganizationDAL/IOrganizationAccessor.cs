
using TeamFinderModels;

namespace TeamFinderDataAccess.OrganizationDAL
{
    public interface IOrganizationAccessor
    {
        void AddOrganization(Organization organization);
        void DeleteOrganization(string id);
        Task<Organization> GetOrganization(string id);
        Task<List<Organization>> GetOrganizations();
        void UpdateOrganization(Organization organization);
    }
}
