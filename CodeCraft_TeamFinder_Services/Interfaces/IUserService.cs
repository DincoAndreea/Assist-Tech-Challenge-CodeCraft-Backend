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
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<bool> RegisterAdmin(RegisterAdminRequestDTO registerAdminRequest);
        Task<bool> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest);
    }
}