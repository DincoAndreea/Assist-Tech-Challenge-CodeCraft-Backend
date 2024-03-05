
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

    namespace TeamFinderDataAccess.SkillDAL
    {
        public class SkillAccessor : ISkillAccessor
        {
            private readonly IMongoCollection<Skill> _skills;

            public SkillAccessor(MongoDbService mongoDbService)
            {
                _skills = mongoDbService.Database?.GetCollection<Skill>("Skill");
            }

            public async Task<Skill> GetSkill(string id)
            {
                var filter = Builders<Skill>.Filter.Eq(x => x.Id, id);
                var _skill = await _skills.Find(filter).FirstOrDefaultAsync();
                return _skill;
            }

            public async Task<List<Skill>> GetSkills()
            {
                return await _skills.Find(FilterDefinition<Skill>.Empty).ToListAsync();
            }

            public async void AddSkill(Skill skill)
            {
                await _skills.InsertOneAsync(skill);
            }

            public async void UpdateSkill(Skill skill)
            {
                var filter = Builders<Skill>.Filter.Eq(x => x.Id, skill.Id);
                await _skills.ReplaceOneAsync(filter, skill);
            }

            public async void DeleteSkill(string id)
            {
                var filter = Builders<Skill>.Filter.Eq(x => x.Id, id);
                await _skills.DeleteOneAsync(filter);
            }
        }
    }
    