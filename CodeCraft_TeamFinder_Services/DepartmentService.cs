using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly Lazy<IUserService> _userService;

        public DepartmentService(IRepository<Department> repository, Lazy<IUserService> userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Department> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsByOrganization(string id)
        {
            List<DepartmentDTO> departmentDTO = new List<DepartmentDTO>();

            var departments = await _repository.Find("OrganizationID", id);

            foreach (Department d in departments ?? Enumerable.Empty<Department>())
            {
                DepartmentDTO department = new DepartmentDTO { Id = d.Id, Name = d.Name, ManagerID = d.ManagerID, OrganizationID = d.OrganizationID };

                if (d.ManagerID != null)
                {
                    var manager = await _userService.Value.Get(d.ManagerID);

                    department.ManagerName = manager.Name;
                }

                departmentDTO.Add(department);
            }
            return departmentDTO;
        }

        public async Task<IEnumerable<Department>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Department department)
        {
            return await _repository.Create(department);
        }

        public async Task<bool> Update(Department department)
        {
            return await _repository.Update(department);
        }

        public async Task<bool> AssignDepartmentManager(AssignDepartmentManagerDTO assignDepartmentManagerDTO)
        {
            var departmentUpdate = await _repository.Get(assignDepartmentManagerDTO.DepartmentID);

            var managerUpdate = await _userService.Value.Get(assignDepartmentManagerDTO.ManagerID);

            departmentUpdate.ManagerID = assignDepartmentManagerDTO.ManagerID;

            bool updatedDepartment = await _repository.Update(departmentUpdate);

            managerUpdate.DepartmentID = assignDepartmentManagerDTO.DepartmentID;

            bool updatedManager = await _userService.Value.Update(managerUpdate);

            return updatedDepartment && updatedManager;
        }

        public async Task<bool> AddDepartmentMember(AddDepartmentMemberDTO addDepartmentMemberDTO)
        {
            var userUpdate = await _userService.Value.Get(addDepartmentMemberDTO.UserID);

            userUpdate.DepartmentID = addDepartmentMemberDTO.DepartmentID;

            bool updatedUser = await _userService.Value.Update(userUpdate);

            return updatedUser;
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
