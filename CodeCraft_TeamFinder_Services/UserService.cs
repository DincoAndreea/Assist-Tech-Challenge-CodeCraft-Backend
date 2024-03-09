using AutoMapper;
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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IOrganizationService _organizationService;
        private readonly ISystemRoleService _systemRoleService;

        public UserService(IRepository<User> repository, IOrganizationService organizationService, ISystemRoleService systemRoleService)
        {
            _repository = repository;
            _organizationService = organizationService;
            _systemRoleService = systemRoleService;
        }

        public async Task<User> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Create(User user)
        {
            return await _repository.Create(user);
        }

        public async Task<IEnumerable<User>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Update(User user)
        {
            return await _repository.Update(user);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<User>> GetUsersByOrganization(string id)
        {
            return await _repository.Find("OrganizationID", id);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            IEnumerable<User> users = await _repository.GetAll();

            var user = users.Where(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password).FirstOrDefault();

            if (user != null)
            {
                LoginResponseDTO loginResponse = new LoginResponseDTO { Id = user.Id, Email = user.Email, Name = user.Name, OrganizationID = user.OrganizationID, DepartmentID = user.DepartmentID, ProjectIDs = user.ProjectIDs, Skills = user.Skills, SystemRoleIDs = user.SystemRoleIDs };

                return loginResponse;
            }

            return new LoginResponseDTO();
        }

        public async Task<bool> RegisterAdmin(RegisterAdminRequestDTO registerAdminRequest)
        {
            Organization organization = new Organization { Name = registerAdminRequest.OrganizationName, Address = registerAdminRequest.OrganizationAddress };

            bool organizationCreated = await _organizationService.Create(organization);

            var systemRole = (await _systemRoleService.Find("Name", "Organization Administrator")).First();

            if (organizationCreated)
            {
                User user = new User { Name = registerAdminRequest.Name, Email = registerAdminRequest.Email, Password = registerAdminRequest.Password, OrganizationID = organization.Id, SystemRoleIDs = new string[] { systemRole.Id } };
                bool adminCreated = await _repository.Create(user);

                return adminCreated;
            }

            return false;
        }

        public async Task<bool> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest)
        {
            var systemRole = (await _systemRoleService.Find("Name", "Employee")).First();

            User user = new User { Name = registerEmployeeRequest.Name, Email = registerEmployeeRequest.Email, Password = registerEmployeeRequest.Password, OrganizationID = registerEmployeeRequest.OrganizationID, SystemRoleIDs = new string[] { systemRole.Id } };
            bool employeeCreated = await _repository.Create(user);

            return employeeCreated;
        }
    }
}
