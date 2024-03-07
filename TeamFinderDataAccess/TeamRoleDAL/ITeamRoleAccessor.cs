
    using TeamFinderModels;

    namespace TeamFinderDataAccess.TeamRoleDAL
    {
        public interface ITeamRoleAccessor
        {
            void AddTeamRole(TeamRole teamrole);
            void DeleteTeamRole(string id);
            Task<TeamRole> GetTeamRole(string id);
            Task<List<TeamRole>> GetTeamRoles();
            void UpdateTeamRole(TeamRole teamrole);
        }
    }
    