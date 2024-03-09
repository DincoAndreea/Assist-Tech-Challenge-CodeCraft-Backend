using CodeCraft_TeamFinder_Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Repository
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private readonly IMongoCollection<T> _collections;

        public Repository(MongoDbService mongoDbService)
        {
            _collections = mongoDbService.Database?.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> Get(string id)
        {
            var _entity = await _collections.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collections.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(string fieldName, string fieldValue)
        {
            FilterDefinition<T>? filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
            return await((await _collections.FindAsync(filter)).ToListAsync());
        }

        public async Task<bool> Create(T t)
        {
            try
            {
                await _collections.InsertOneAsync(t);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> Update(T t)
        {
            try
            {
                await _collections.ReplaceOneAsync(x => x.Id == t.Id, t);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await _collections.DeleteOneAsync(x => x.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
