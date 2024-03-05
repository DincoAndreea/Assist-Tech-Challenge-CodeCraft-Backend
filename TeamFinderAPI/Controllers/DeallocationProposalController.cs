
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.DeallocationProposalDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeallocationProposalController : ControllerBase
    {
        private IDeallocationProposalAccessor _deallocationproposalAccessor;

        public DeallocationProposalController(IDeallocationProposalAccessor deallocationproposalAccessor)
        {
            _deallocationproposalAccessor = deallocationproposalAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<DeallocationProposal>> GetDeallocationProposals()
        {
            return await _deallocationproposalAccessor.GetDeallocationProposals();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeallocationProposal?>> GetDeallocationProposal(string id)
        {
            return await _deallocationproposalAccessor.GetDeallocationProposal(id);
        }

        [HttpPost]
        public async Task<ActionResult> Adddeallocationproposal(DeallocationProposal deallocationproposal)
        {
            _deallocationproposalAccessor.AddDeallocationProposal(deallocationproposal);
            return CreatedAtAction(nameof(GetDeallocationProposal), new { id = deallocationproposal.Id }, deallocationproposal);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDeallocationProposal(DeallocationProposal deallocationproposal)
        {
            _deallocationproposalAccessor.UpdateDeallocationProposal(deallocationproposal);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeallocationProposal(string id)
        {
            _deallocationproposalAccessor.DeleteDeallocationProposal(id);
            return Ok();
        }
    }
}

