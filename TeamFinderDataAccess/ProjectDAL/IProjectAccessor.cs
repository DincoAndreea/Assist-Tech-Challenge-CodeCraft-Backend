
    using TeamFinderModels;

    namespace TeamFinderDataAccess.ProjectDAL
    {
        public interface IProjectAccessor
        {
            void AddProject(Project project);
            void DeleteProject(string id);
            Task<Project> GetProject(string id);
            Task<List<Project>> GetProjects();
            void UpdateProject(Project project);
        }
    }
    