
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;
using MongoDB.Bson;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentProposalController : ControllerBase
    {
        private IAssignmentProposalService _assignmentProposalService;

        public AssignmentProposalController(IAssignmentProposalService assignmentProposalService)
        {
            _assignmentProposalService = assignmentProposalService;
        }

        [HttpGet]
        public async Task<IEnumerable<AssignmentProposal>> GetAssignmentProposals()
        {
            return await _assignmentProposalService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentProposal>> GetAssignmentProposal(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            AssignmentProposal assignmentProposal = await _assignmentProposalService.Get(id);

            if (assignmentProposal == null)
            {
                return NotFound();
            }

            return Ok(assignmentProposal);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssignmentProposal(AssignmentProposal assignmentProposal)
        {
            bool success = await _assignmentProposalService.Create(assignmentProposal);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetAssignmentProposal), new { id = assignmentProposal.Id }, assignmentProposal);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAssignmentProposal(AssignmentProposal assignmentProposal)
        {
            bool success = await _assignmentProposalService.Update(assignmentProposal);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignmentProposal(string id)
        {
            bool success = await _assignmentProposalService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

