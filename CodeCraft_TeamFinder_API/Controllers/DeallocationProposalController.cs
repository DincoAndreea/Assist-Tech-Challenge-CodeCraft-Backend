
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;

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
            DeallocationProposal deallocationProposal = await _deallocationProposalService.Get(id);

            if (deallocationProposal == null)
            {
                return NotFound();
            }

            return Ok(deallocationProposal);
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

