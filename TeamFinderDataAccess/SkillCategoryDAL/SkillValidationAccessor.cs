
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TeamFinderModels;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TeamFinderDataAccess.Data;

    namespace TeamFinderDataAccess.SkillCategoryDAL
    {
        public class SkillCategoryAccessor : ISkillCategoryAccessor
        {
            private readonly IMongoCollection<SkillCategory> _skillCategorys;

            public SkillCategoryAccessor(MongoDbService mongoDbService)
            {
                _skillCategorys = mongoDbService.Database?.GetCollection<SkillCategory>("SkillCategory");
            }

            public async Task<SkillCategory> GetSkillCategory(string id)
            {
                var filter = Builders<SkillCategory>.Filter.Eq(x => x.Id, id);
                var _skillCategory = await _skillCategorys.Find(filter).FirstOrDefaultAsync();
                return _skillCategory;
            }

            public async Task<List<SkillCategory>> GetSkillCategorys()
            {
                return await _skillCategorys.Find(FilterDefinition<SkillCategory>.Empty).ToListAsync();
            }

            public async void AddSkillCategory(SkillCategory skillCategory)
            {
                await _skillCategorys.InsertOneAsync(skillCategory);
            }

            public async void UpdateSkillCategory(SkillCategory skillCategory)
            {
                var filter = Builders<SkillCategory>.Filter.Eq(x => x.Id, skillCategory.Id);
                await _skillCategorys.ReplaceOneAsync(filter, skillCategory);
            }

            public async void DeleteSkillCategory(string id)
            {
                var filter = Builders<SkillCategory>.Filter.Eq(x => x.Id, id);
                await _skillCategorys.DeleteOneAsync(filter);
            }
        }
    }
    