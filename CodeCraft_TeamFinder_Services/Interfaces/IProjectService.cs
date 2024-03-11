using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IProjectService
    {
        Task<bool> Create(Project project);
        Task<bool> Delete(string id);
        Task<IEnumerable<Project>> Find(string fieldName, string fieldValue);
        Task<Project> Get(string id);
        Task<IEnumerable<Project>> GetAll();
        Task<IEnumerable<Project>> GetProjectByOrganization(string id);
        Task<bool> Update(Project project);
    }
}