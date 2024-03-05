
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

    namespace TeamFinderDataAccess.DeallocationProposalDAL
    {
        public class DeallocationProposalAccessor : IDeallocationProposalAccessor
        {
            private readonly IMongoCollection<DeallocationProposal> _deallocationproposals;

            public DeallocationProposalAccessor(MongoDbService mongoDbService)
            {
                _deallocationproposals = mongoDbService.Database?.GetCollection<DeallocationProposal>("DeallocationProposal");
            }

            public async Task<DeallocationProposal> GetDeallocationProposal(string id)
            {
                var filter = Builders<DeallocationProposal>.Filter.Eq(x => x.Id, id);
                var _deallocationproposal = await _deallocationproposals.Find(filter).FirstOrDefaultAsync();
                return _deallocationproposal;
            }

            public async Task<List<DeallocationProposal>> GetDeallocationProposals()
            {
                return await _deallocationproposals.Find(FilterDefinition<DeallocationProposal>.Empty).ToListAsync();
            }

            public async void AddDeallocationProposal(DeallocationProposal deallocationproposal)
            {
                await _deallocationproposals.InsertOneAsync(deallocationproposal);
            }

            public async void UpdateDeallocationProposal(DeallocationProposal deallocationproposal)
            {
                var filter = Builders<DeallocationProposal>.Filter.Eq(x => x.Id, deallocationproposal.Id);
                await _deallocationproposals.ReplaceOneAsync(filter, deallocationproposal);
            }

            public async void DeleteDeallocationProposal(string id)
            {
                var filter = Builders<DeallocationProposal>.Filter.Eq(x => x.Id, id);
                await _deallocationproposals.DeleteOneAsync(filter);
            }
        }
    }
    