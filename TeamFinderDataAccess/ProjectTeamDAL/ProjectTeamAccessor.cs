
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TeamFinderModels;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TeamFinderDataAccess.Data;

    namespace TeamFinderDataAccess.ProjectTeamDAL
    {
        public class ProjectTeamAccessor : IProjectTeamAccessor
        {
            private readonly IMongoCollection<ProjectTeam> _projectTeams;

            public ProjectTeamAccessor(MongoDbService mongoDbService)
            {
                _projectTeams = mongoDbService.Database?.GetCollection<ProjectTeam>("ProjectTeam");
            }

            public async Task<ProjectTeam> GetProjectTeam(string id)
            {
                var filter = Builders<ProjectTeam>.Filter.Eq(x => x.Id, id);
                var _projectrole = await _projectTeams.Find(filter).FirstOrDefaultAsync();
                return _projectrole;
            }

            public async Task<List<ProjectTeam>> GetProjectTeams()
            {
                return await _projectTeams.Find(FilterDefinition<ProjectTeam>.Empty).ToListAsync();
            }

            public async void AddProjectTeam(ProjectTeam projectrole)
            {
                await _projectTeams.InsertOneAsync(projectrole);
            }

            public async void UpdateProjectTeam(ProjectTeam projectrole)
            {
                var filter = Builders<ProjectTeam>.Filter.Eq(x => x.Id, projectrole.Id);
                await _projectTeams.ReplaceOneAsync(filter, projectrole);
            }

            public async void DeleteProjectTeam(string id)
            {
                var filter = Builders<ProjectTeam>.Filter.Eq(x => x.Id, id);
                await _projectTeams.DeleteOneAsync(filter);
            }
        }
    }
    