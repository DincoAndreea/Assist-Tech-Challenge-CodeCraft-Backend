using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IProjectTeamService
    {
        Task<bool> Create(ProjectTeam projectTeam);
        Task<bool> Delete(string id);
        Task<IEnumerable<ProjectTeam>> Find(string fieldName, string fieldValue);
        Task<ProjectTeam> Get(string id);
        Task<IEnumerable<ProjectTeam>> GetAll();
        Task<bool> Update(ProjectTeam projectTeam);
    }
}