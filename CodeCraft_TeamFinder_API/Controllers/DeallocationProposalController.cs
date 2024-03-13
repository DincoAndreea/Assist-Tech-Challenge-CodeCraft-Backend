
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;
using MongoDB.Bson;
using CodeCraft_TeamFinder_Services;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeallocationProposalController : ControllerBase
    {
        private IDeallocationProposalService _deallocationProposalService;

        public DeallocationProposalController(IDeallocationProposalService deallocationProposalService)
        {
            _deallocationProposalService = deallocationProposalService;
        }

        [HttpGet]
        public async Task<IEnumerable<DeallocationProposal>> GetDeallocationProposals()
        {
            return await _deallocationProposalService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeallocationProposal>> GetDeallocationProposal(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            DeallocationProposal deallocationProposal = await _deallocationProposalService.Get(id);

            if (deallocationProposal == null)
            {
                return NotFound();
            }

            return Ok(deallocationProposal);
        }

        [HttpGet("DepartmentManager/{id}")]
        public async Task<ActionResult<IEnumerable<DeallocationProposal>>> GetDeallocationProposalsByDepartmentManager(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var deallocationProposals = await _deallocationProposalService.GetDeallocationProposalsByDepartmentManager(id);

            return Ok(deallocationProposals);
        }

        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<DeallocationProposal>>> GetDeallocationProposalsByProject(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var deallocationProposals = await _deallocationProposalService.GetDeallocationProposalsByProject(id);

            return Ok(deallocationProposals);
        }

        [HttpPost("AcceptDeallocationProposal")]
        public async Task<ActionResult> AcceptDeallocationProposal(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var success = await _deallocationProposalService.AcceptDealllocationProposal(id);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDeallocationProposal(DeallocationProposal deallocationProposal)
        {
            bool success = await _deallocationProposalService.Create(deallocationProposal);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetDeallocationProposal), new { id = deallocationProposal.Id }, deallocationProposal);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDeallocationProposal(DeallocationProposal deallocationProposal)
        {
            bool success = await _deallocationProposalService.Update(deallocationProposal);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeallocationProposal(string id)
        {
            bool success = await _deallocationProposalService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

