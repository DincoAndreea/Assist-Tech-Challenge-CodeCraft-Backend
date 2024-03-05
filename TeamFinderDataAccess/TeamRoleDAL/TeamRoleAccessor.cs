
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

    namespace TeamFinderDataAccess.TeamRoleDAL
    {
        public class TeamRoleAccessor : ITeamRoleAccessor
        {
            private readonly IMongoCollection<TeamRole> _teamroles;

            public TeamRoleAccessor(MongoDbService mongoDbService)
            {
                _teamroles = mongoDbService.Database?.GetCollection<TeamRole>("TeamRole");
            }

            public async Task<TeamRole> GetTeamRole(string id)
            {
                var filter = Builders<TeamRole>.Filter.Eq(x => x.Id, id);
                var _teamrole = await _teamroles.Find(filter).FirstOrDefaultAsync();
                return _teamrole;
            }

            public async Task<List<TeamRole>> GetTeamRoles()
            {
                return await _teamroles.Find(FilterDefinition<TeamRole>.Empty).ToListAsync();
            }

            public async void AddTeamRole(TeamRole teamrole)
            {
                await _teamroles.InsertOneAsync(teamrole);
            }

            public async void UpdateTeamRole(TeamRole teamrole)
            {
                var filter = Builders<TeamRole>.Filter.Eq(x => x.Id, teamrole.Id);
                await _teamroles.ReplaceOneAsync(filter, teamrole);
            }

            public async void DeleteTeamRole(string id)
            {
                var filter = Builders<TeamRole>.Filter.Eq(x => x.Id, id);
                await _teamroles.DeleteOneAsync(filter);
            }
        }
    }
    