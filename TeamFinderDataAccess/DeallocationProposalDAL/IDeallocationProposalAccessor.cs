
    using TeamFinderModels;

    namespace TeamFinderDataAccess.DeallocationProposalDAL
    {
        public interface IDeallocationProposalAccessor
        {
            void AddDeallocationProposal(DeallocationProposal deallocationproposal);
            void DeleteDeallocationProposal(string id);
            Task<DeallocationProposal> GetDeallocationProposal(string id);
            Task<List<DeallocationProposal>> GetDeallocationProposals();
            void UpdateDeallocationProposal(DeallocationProposal deallocationproposal);
        }
    }
    