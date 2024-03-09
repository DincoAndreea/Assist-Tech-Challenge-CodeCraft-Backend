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

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<Department> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _repository.GetAll();
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

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
