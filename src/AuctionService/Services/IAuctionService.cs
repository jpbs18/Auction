using AuctionService.Data.DTOs;

namespace AuctionService.Services
{
    public interface IAuctionService
    {
        Task<AuctionDTO> GetAuctionById(Guid id);
        Task<List<AuctionDTO>> GetAllAuctions();
        Task DeleteAuctionById(Guid id);
        Task<AuctionDTO> CreateAuction(CreateAuctionDTO auctionDto);
        Task<AuctionDTO> UpdateAuction(Guid id, UpdateAuctionDTO auctionDto);
    }
}
