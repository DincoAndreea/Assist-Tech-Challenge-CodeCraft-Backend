
    using TeamFinderModels;

    namespace TeamFinderDataAccess.ProjectRoleDAL
    {
        public interface IProjectRoleAccessor
        {
            void AddProjectRole(ProjectRole projectrole);
            void DeleteProjectRole(string id);
            Task<ProjectRole> GetProjectRole(string id);
            Task<List<ProjectRole>> GetProjectRoles();
            void UpdateProjectRole(ProjectRole projectrole);
        }
    }
    