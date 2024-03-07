
    using TeamFinderModels;

    namespace TeamFinderDataAccess.ProjectTeamDAL
    {
        public interface IProjectTeamAccessor
        {
            void AddProjectTeam(ProjectTeam projectrole);
            void DeleteProjectTeam(string id);
            Task<ProjectTeam> GetProjectTeam(string id);
            Task<List<ProjectTeam>> GetProjectTeams();
            void UpdateProjectTeam(ProjectTeam projectrole);
        }
    }
    