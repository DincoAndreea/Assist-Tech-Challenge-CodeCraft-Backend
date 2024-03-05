
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TeamFinderRepository;
    using TeamFinderModels;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TeamFinderDataAccess.Data;

    namespace TeamFinderDataAccess.DepartmentDAL
    {
        public class DepartmentAccessor : IDepartmentAccessor
        {
            private readonly IMongoCollection<Department> _departments;

            public DepartmentAccessor(MongoDbService mongoDbService)
            {
                _departments = mongoDbService.Database?.GetCollection<Department>("Department");
            }

            public async Task<Department> GetDepartment(string id)
            {
                var filter = Builders<Department>.Filter.Eq(x => x.Id, id);
                var _department = await _departments.Find(filter).FirstOrDefaultAsync();
                return _department;
            }

            public async Task<List<Department>> GetDepartments()
            {
                return await _departments.Find(FilterDefinition<Department>.Empty).ToListAsync();
            }

            public async void AddDepartment(Department department)
            {
                await _departments.InsertOneAsync(department);
            }

            public async void UpdateDepartment(Department department)
            {
                var filter = Builders<Department>.Filter.Eq(x => x.Id, department.Id);
                await _departments.ReplaceOneAsync(filter, department);
            }

            public async void DeleteDepartment(string id)
            {
                var filter = Builders<Department>.Filter.Eq(x => x.Id, id);
                await _departments.DeleteOneAsync(filter);
            }
        }
    }
    