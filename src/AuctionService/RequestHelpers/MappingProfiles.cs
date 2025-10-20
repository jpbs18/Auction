﻿using AuctionService.Data.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Auction, AuctionDTO>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionDTO>();
            CreateMap<CreateAuctionDTO, Auction>()
                .ForMember(dest => dest.Item, o => o.MapFrom(src => src));
            CreateMap<CreateAuctionDTO, Item>();
        }
    }
}
