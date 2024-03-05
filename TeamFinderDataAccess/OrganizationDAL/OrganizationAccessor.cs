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

namespace TeamFinderDataAccess.OrganizationDAL
{
    public class OrganizationAccessor : IOrganizationAccessor
    {
        private readonly IMongoCollection<Organization> _organizations;

        public OrganizationAccessor(MongoDbService mongoDbService)
        {
            _organizations = mongoDbService.Database?.GetCollection<Organization>("Organization");
        }

        public async Task<Organization> GetOrganization(string id)
        {
            var filter = Builders<Organization>.Filter.Eq(x => x.Id, id);
            var organization = await _organizations.Find(filter).FirstOrDefaultAsync();
            return organization;
        }

        public async Task<List<Organization>> GetOrganizations()
        {
            return await _organizations.Find(FilterDefinition<Organization>.Empty).ToListAsync();
        }

        public async void AddOrganization(Organization organization)
        {
            await _organizations.InsertOneAsync(organization);
        }

        public async void UpdateOrganization(Organization organization)
        {
            var filter = Builders<Organization>.Filter.Eq(x => x.Id, organization.Id);
            await _organizations.ReplaceOneAsync(filter, organization);
        }

        public async void DeleteOrganization(string id)
        {
            var filter = Builders<Organization>.Filter.Eq(x => x.Id, id);
            await _organizations.DeleteOneAsync(filter);
        }
    }
}