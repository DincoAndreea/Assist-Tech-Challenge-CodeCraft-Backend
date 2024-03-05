
    using TeamFinderModels;

    namespace TeamFinderDataAccess.SystemRoleDAL
    {
        public interface ISystemRoleAccessor
        {
            void AddSystemRole(SystemRole systemrole);
            void DeleteSystemRole(string id);
            Task<SystemRole> GetSystemRole(string id);
            Task<List<SystemRole>> GetSystemRoles();
            void UpdateSystemRole(SystemRole systemrole);
        }
    }
    