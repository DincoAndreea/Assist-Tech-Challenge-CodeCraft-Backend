using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> Create(Department department);
        Task<bool> Delete(string id);
        Task<IEnumerable<Department>> Find(string fieldName, string fieldValue);
        Task<Department> Get(string id);
        Task<IEnumerable<Department>> GetAll();
        Task<IEnumerable<DepartmentDTO>> GetDepartmentsByOrganization(string id);
        Task<bool> AssignDepartmentManager(AssignDepartmentManagerDTO assignDepartmentManagerDTO);
        Task<bool> AddDepartmentMember(AddDepartmentMemberDTO addDepartmentMemberDTO);
        Task<bool> Update(Department department);
    }
}