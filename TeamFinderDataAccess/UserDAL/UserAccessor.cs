
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

    namespace TeamFinderDataAccess.UserDAL
    {
        public class UserAccessor : IUserAccessor
        {
            private readonly IMongoCollection<User> _users;

            public UserAccessor(MongoDbService mongoDbService)
            {
                _users = mongoDbService.Database?.GetCollection<User>("User");
            }

            public async Task<User> GetUser(string id)
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                var _user = await _users.Find(filter).FirstOrDefaultAsync();
                return _user;
            }

            public async Task<List<User>> GetUsers()
            {
                return await _users.Find(FilterDefinition<User>.Empty).ToListAsync();
            }

            public async void AddUser(User user)
            {
                await _users.InsertOneAsync(user);
            }

            public async void UpdateUser(User user)
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
                await _users.ReplaceOneAsync(filter, user);
            }

            public async void DeleteUser(string id)
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                await _users.DeleteOneAsync(filter);
            }
        }
    }
    