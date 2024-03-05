
    using TeamFinderModels;

    namespace TeamFinderDataAccess.AssignmentProposalDAL
    {
        public interface IAssignmentProposalAccessor
        {
            void AddAssignmentProposal(AssignmentProposal assignmentproposal);
            void DeleteAssignmentProposal(string id);
            Task<AssignmentProposal> GetAssignmentProposal(string id);
            Task<List<AssignmentProposal>> GetAssignmentProposals();
            void UpdateAssignmentProposal(AssignmentProposal assignmentproposal);
        }
    }
    