
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TeamFinderModels;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TeamFinderDataAccess.Data;

    namespace TeamFinderDataAccess.AssignmentProposalDAL
    {
        public class AssignmentProposalAccessor : IAssignmentProposalAccessor
        {
            private readonly IMongoCollection<AssignmentProposal> _assignmentproposals;

            public AssignmentProposalAccessor(MongoDbService mongoDbService)
            {
                _assignmentproposals = mongoDbService.Database?.GetCollection<AssignmentProposal>("AssignmentProposal");
            }

            public async Task<AssignmentProposal> GetAssignmentProposal(string id)
            {
                var filter = Builders<AssignmentProposal>.Filter.Eq(x => x.Id, id);
                var _assignmentproposal = await _assignmentproposals.Find(filter).FirstOrDefaultAsync();
                return _assignmentproposal;
            }

            public async Task<List<AssignmentProposal>> GetAssignmentProposals()
            {
                return await _assignmentproposals.Find(FilterDefinition<AssignmentProposal>.Empty).ToListAsync();
            }

            public async void AddAssignmentProposal(AssignmentProposal assignmentproposal)
            {
                await _assignmentproposals.InsertOneAsync(assignmentproposal);
            }

            public async void UpdateAssignmentProposal(AssignmentProposal assignmentproposal)
            {
                var filter = Builders<AssignmentProposal>.Filter.Eq(x => x.Id, assignmentproposal.Id);
                await _assignmentproposals.ReplaceOneAsync(filter, assignmentproposal);
            }

            public async void DeleteAssignmentProposal(string id)
            {
                var filter = Builders<AssignmentProposal>.Filter.Eq(x => x.Id, id);
                await _assignmentproposals.DeleteOneAsync(filter);
            }
        }
    }
    