
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.AssignmentProposalDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentProposalController : ControllerBase
    {
        private IAssignmentProposalAccessor _assignmentproposalAccessor;

        public AssignmentProposalController(IAssignmentProposalAccessor assignmentproposalAccessor)
        {
            _assignmentproposalAccessor = assignmentproposalAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<AssignmentProposal>> GetAssignmentProposals()
        {
            return await _assignmentproposalAccessor.GetAssignmentProposals();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentProposal?>> GetAssignmentProposal(string id)
        {
            return await _assignmentproposalAccessor.GetAssignmentProposal(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addassignmentproposal(AssignmentProposal assignmentproposal)
        {
            _assignmentproposalAccessor.AddAssignmentProposal(assignmentproposal);
            return CreatedAtAction(nameof(GetAssignmentProposal), new { id = assignmentproposal.Id }, assignmentproposal);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAssignmentProposal(AssignmentProposal assignmentproposal)
        {
            _assignmentproposalAccessor.UpdateAssignmentProposal(assignmentproposal);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignmentProposal(string id)
        {
            _assignmentproposalAccessor.DeleteAssignmentProposal(id);
            return Ok();
        }
    }
}

