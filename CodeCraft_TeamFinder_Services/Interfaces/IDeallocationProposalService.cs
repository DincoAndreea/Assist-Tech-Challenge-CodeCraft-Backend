using CodeCraft_TeamFinder_Models;

namespace CodeCraft_TeamFinder_Services.Interfaces
{
    public interface IDeallocationProposalService
    {
        Task<bool> Create(DeallocationProposal deallocationProposal);
        Task<bool> Delete(string id);
        Task<IEnumerable<DeallocationProposal>> Find(string fieldName, string fieldValue);
        Task<DeallocationProposal> Get(string id);
        Task<IEnumerable<DeallocationProposal>> GetAll();
        Task<IEnumerable<DeallocationProposal>> GetDeallocationProposalsByDepartmentManager(string id);
        Task<IEnumerable<DeallocationProposal>> GetDeallocationProposalsByProject(string id);
        Task<bool> AcceptDealllocationProposal(string id);
        Task<bool> Update(DeallocationProposal deallocationProposal);
    }
}