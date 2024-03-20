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
        Task<IEnumerable<Project>> GetProjectsByOrganization(string id);
        Task<IEnumerable<Project>> GetEmployeeProjects(string id);
        Task<EmployeeProjectsDTO> GetEmployeeProjectsLists(string id);
        Task<IEnumerable<Project>> GetDepartmentProjects(string id);
        Task<ProjectDetailsDTO> GetProjectDetails(string id);
        Task<IEnumerable<Project>> GetProjectsByProjectManager(string id);
        Task<bool> Update(Project project);
    }
}