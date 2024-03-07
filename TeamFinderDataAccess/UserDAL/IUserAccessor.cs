
    using TeamFinderModels;

    namespace TeamFinderDataAccess.UserDAL
    {
        public interface IUserAccessor
        {
            void AddUser(User user);
            void DeleteUser(string id);
            Task<User> GetUser(string id);
            Task<List<User>> GetUsers();
            void UpdateUser(User user);
        }
    }
    