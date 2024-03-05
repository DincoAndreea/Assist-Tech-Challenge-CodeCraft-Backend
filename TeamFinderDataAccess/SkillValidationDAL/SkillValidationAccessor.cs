
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

    namespace TeamFinderDataAccess.SkillValidationDAL
    {
        public class SkillValidationAccessor : ISkillValidationAccessor
        {
            private readonly IMongoCollection<SkillValidation> _skillvalidations;

            public SkillValidationAccessor(MongoDbService mongoDbService)
            {
                _skillvalidations = mongoDbService.Database?.GetCollection<SkillValidation>("SkillValidation");
            }

            public async Task<SkillValidation> GetSkillValidation(string id)
            {
                var filter = Builders<SkillValidation>.Filter.Eq(x => x.Id, id);
                var _skillvalidation = await _skillvalidations.Find(filter).FirstOrDefaultAsync();
                return _skillvalidation;
            }

            public async Task<List<SkillValidation>> GetSkillValidations()
            {
                return await _skillvalidations.Find(FilterDefinition<SkillValidation>.Empty).ToListAsync();
            }

            public async void AddSkillValidation(SkillValidation skillvalidation)
            {
                await _skillvalidations.InsertOneAsync(skillvalidation);
            }

            public async void UpdateSkillValidation(SkillValidation skillvalidation)
            {
                var filter = Builders<SkillValidation>.Filter.Eq(x => x.Id, skillvalidation.Id);
                await _skillvalidations.ReplaceOneAsync(filter, skillvalidation);
            }

            public async void DeleteSkillValidation(string id)
            {
                var filter = Builders<SkillValidation>.Filter.Eq(x => x.Id, id);
                await _skillvalidations.DeleteOneAsync(filter);
            }
        }
    }
    