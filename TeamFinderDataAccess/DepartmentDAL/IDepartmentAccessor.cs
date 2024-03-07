
    using TeamFinderModels;

    namespace TeamFinderDataAccess.DepartmentDAL
    {
        public interface IDepartmentAccessor
        {
            void AddDepartment(Department department);
            void DeleteDepartment(string id);
            Task<Department> GetDepartment(string id);
            Task<List<Department>> GetDepartments();
            void UpdateDepartment(Department department);
        }
    }
    