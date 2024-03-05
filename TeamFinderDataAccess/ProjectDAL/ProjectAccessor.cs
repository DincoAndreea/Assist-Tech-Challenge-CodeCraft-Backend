
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

    namespace TeamFinderDataAccess.ProjectDAL
    {
        public class ProjectAccessor : IProjectAccessor
        {
            private readonly IMongoCollection<Project> _projects;

            public ProjectAccessor(MongoDbService mongoDbService)
            {
                _projects = mongoDbService.Database?.GetCollection<Project>("Project");
            }

            public async Task<Project> GetProject(string id)
            {
                var filter = Builders<Project>.Filter.Eq(x => x.Id, id);
                var _project = await _projects.Find(filter).FirstOrDefaultAsync();
                return _project;
            }

            public async Task<List<Project>> GetProjects()
            {
                return await _projects.Find(FilterDefinition<Project>.Empty).ToListAsync();
            }

            public async void AddProject(Project project)
            {
                await _projects.InsertOneAsync(project);
            }

            public async void UpdateProject(Project project)
            {
                var filter = Builders<Project>.Filter.Eq(x => x.Id, project.Id);
                await _projects.ReplaceOneAsync(filter, project);
            }

            public async void DeleteProject(string id)
            {
                var filter = Builders<Project>.Filter.Eq(x => x.Id, id);
                await _projects.DeleteOneAsync(filter);
            }
        }
    }
    