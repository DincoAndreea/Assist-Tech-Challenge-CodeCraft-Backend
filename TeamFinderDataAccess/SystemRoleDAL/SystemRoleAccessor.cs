
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

    namespace TeamFinderDataAccess.SystemRoleDAL
    {
        public class SystemRoleAccessor : ISystemRoleAccessor
        {
            private readonly IMongoCollection<SystemRole> _systemroles;

            public SystemRoleAccessor(MongoDbService mongoDbService)
            {
                _systemroles = mongoDbService.Database?.GetCollection<SystemRole>("SystemRole");
            }

            public async Task<SystemRole> GetSystemRole(string id)
            {
                var filter = Builders<SystemRole>.Filter.Eq(x => x.Id, id);
                var _systemrole = await _systemroles.Find(filter).FirstOrDefaultAsync();
                return _systemrole;
            }

            public async Task<List<SystemRole>> GetSystemRoles()
            {
                return await _systemroles.Find(FilterDefinition<SystemRole>.Empty).ToListAsync();
            }

            public async void AddSystemRole(SystemRole systemrole)
            {
                await _systemroles.InsertOneAsync(systemrole);
            }

            public async void UpdateSystemRole(SystemRole systemrole)
            {
                var filter = Builders<SystemRole>.Filter.Eq(x => x.Id, systemrole.Id);
                await _systemroles.ReplaceOneAsync(filter, systemrole);
            }

            public async void DeleteSystemRole(string id)
            {
                var filter = Builders<SystemRole>.Filter.Eq(x => x.Id, id);
                await _systemroles.DeleteOneAsync(filter);
            }
        }
    }
    