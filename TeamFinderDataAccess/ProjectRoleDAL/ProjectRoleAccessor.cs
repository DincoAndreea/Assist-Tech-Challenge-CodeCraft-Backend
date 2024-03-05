
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

    namespace TeamFinderDataAccess.ProjectRoleDAL
    {
        public class ProjectRoleAccessor : IProjectRoleAccessor
        {
            private readonly IMongoCollection<ProjectRole> _projectroles;

            public ProjectRoleAccessor(MongoDbService mongoDbService)
            {
                _projectroles = mongoDbService.Database?.GetCollection<ProjectRole>("ProjectRole");
            }

            public async Task<ProjectRole> GetProjectRole(string id)
            {
                var filter = Builders<ProjectRole>.Filter.Eq(x => x.Id, id);
                var _projectrole = await _projectroles.Find(filter).FirstOrDefaultAsync();
                return _projectrole;
            }

            public async Task<List<ProjectRole>> GetProjectRoles()
            {
                return await _projectroles.Find(FilterDefinition<ProjectRole>.Empty).ToListAsync();
            }

            public async void AddProjectRole(ProjectRole projectrole)
            {
                await _projectroles.InsertOneAsync(projectrole);
            }

            public async void UpdateProjectRole(ProjectRole projectrole)
            {
                var filter = Builders<ProjectRole>.Filter.Eq(x => x.Id, projectrole.Id);
                await _projectroles.ReplaceOneAsync(filter, projectrole);
            }

            public async void DeleteProjectRole(string id)
            {
                var filter = Builders<ProjectRole>.Filter.Eq(x => x.Id, id);
                await _projectroles.DeleteOneAsync(filter);
            }
        }
    }
    