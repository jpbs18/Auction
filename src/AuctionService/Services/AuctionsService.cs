using AuctionService.Data;
using AuctionService.Data.DTOs;
using AuctionService.Entities;
using AuctionService.Middlewares;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Services
{
    public class AuctionsService(AuctionDbContext context, IMapper mapper) : IAuctionService
    {
        private readonly AuctionDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<AuctionDTO>> GetAllAuctions()
        {
            var auctions = await _context.Auctions
                .Include(x => x.Item)
                .OrderBy(x => x.Item.Make)
                .ToListAsync();

            return _mapper.Map<List<AuctionDTO>>(auctions);
        }

        public async Task<AuctionDTO> GetAuctionById(Guid id)
        {
            var auction = await _context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Auction with Id '{id}' was not found.");

            return _mapper.Map<AuctionDTO>(auction);
        }

        public async Task DeleteAuctionById(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id) 
                ?? throw new NotFoundException($"Auction with Id '{id}' was not found."); 

            _context.Auctions.Remove(auction);
            var savedSuccessfully = await _context.SaveChangesAsync() > 0;

            if (!savedSuccessfully) 
            {
                throw new Exception("Failed to delete auction.");
            }
        }

        public async Task<AuctionDTO> CreateAuction(CreateAuctionDTO dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Auction data cannot be null.");
            }           

            var auction = _mapper.Map<Auction>(dto);
            auction.Seller = "UserTest"; // TODO: Replace with authenticated user

            _context.Auctions.Add(auction);
            var savedSuccessfully = await _context.SaveChangesAsync() > 0;

            if (!savedSuccessfully) 
            {
                throw new Exception("Failed to create auction."); 
            }

            return _mapper.Map<AuctionDTO>(auction);
        }

        public async Task<AuctionDTO> UpdateAuction(Guid id, UpdateAuctionDTO dto)
        {
            var auction = await _context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Auction with Id '{id}' was not found.");

            auction.Item.Make = dto.Make ?? auction.Item.Make;
            auction.Item.Model = dto.Model ?? auction.Item.Model;
            auction.Item.Year = dto.Year ?? auction.Item.Year;
            auction.Item.Color = dto.Color ?? auction.Item.Color;
            auction.Item.Mileage = dto.Mileage ?? auction.Item.Mileage;

            var savedSuccessfully = await _context.SaveChangesAsync() > 0;

            if (!savedSuccessfully) 
            { 
                throw new Exception("Failed to update auction."); 
            }

            return _mapper.Map<AuctionDTO>(auction);
        }
    }
}
