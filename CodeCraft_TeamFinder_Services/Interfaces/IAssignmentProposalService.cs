using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IAssignmentProposalService
    {
        Task<bool> Create(AssignmentProposal assignmentProposal);
        Task<bool> Delete(string id);
        Task<IEnumerable<AssignmentProposal>> Find(string fieldName, string fieldValue);
        Task<AssignmentProposal> Get(string id);
        Task<IEnumerable<AssignmentProposal>> GetAll();
        Task<IEnumerable<AssignmentProposal>> GetAssignmentProposalsByDepartmentManager(string id);
        Task<IEnumerable<AssignmentProposal>> GetAssignmentProposalsByProject(string id);
        Task<bool> AcceptAssignmentProposal(string id);
        Task<bool> Update(AssignmentProposal assignmentProposal);
    }
}