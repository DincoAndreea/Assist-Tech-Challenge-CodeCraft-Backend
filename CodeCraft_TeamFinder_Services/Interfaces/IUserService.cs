using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Get(string id);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> Find(string fieldName, string fieldValue);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(string id);
        Task<IEnumerable<User>> GetUsersByOrganization(string id);
        Task<IEnumerable<User>> GetUsersByDepartment(string id);
        Task<IEnumerable<User>> GetUsersByProject(string id);
        Task<IEnumerable<User>> GetUsersBySkill(string id);
        Task<IEnumerable<TeamFinderResponseDTO>> TeamFinder(TeamFinderRequestDTO teamFinderRequestDTO);
        Task<IEnumerable<TeamFinderResponseDTO>> TeamFinderOpenAI(TeamFinderOpenAI teamFinderOpenAI);
        Task<IEnumerable<User>> GetDepartmentManagers(DepartmentManagersDTO departmentManagersDTO);
        Task<IEnumerable<User>> GetProjectManagers(string id);
        Task<IEnumerable<User>> GetEmployees(string id);
        Task<IEnumerable<User>> GetEmployeesWithoutDepartment(string id);
        Task<IEnumerable<User>> GetOrganizationAdmins(string id);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<bool> RegisterAdmin(RegisterAdminRequestDTO registerAdminRequest);
        Task<bool> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest);
    }
}