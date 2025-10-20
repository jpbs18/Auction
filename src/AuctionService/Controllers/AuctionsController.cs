using AuctionService.Data.DTOs;
using AuctionService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController(IAuctionService auctionService) : ControllerBase
    {
        private readonly IAuctionService _auctionService = auctionService;

        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(Guid id)
        {
            var auction = await _auctionService.GetAuctionById(id);
            return Ok(auction);
        }

        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction(CreateAuctionDTO dto)
        {
            var auction = await _auctionService.CreateAuction(dto);
            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, auction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuctionDTO>> UpdateAuction(Guid id, UpdateAuctionDTO dto)
        {
            var auction = await _auctionService.UpdateAuction(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            await _auctionService.DeleteAuctionById(id);
            return Ok();
        }
    }
}
