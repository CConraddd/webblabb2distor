using AutoMapper;
using webblabb2distor.Core;
using webblabb2distor.Persistence;

namespace webblabb2distor.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Auction, AuctionDB>()
            .ForMember(dest => dest.BidsDbs, opt => opt.MapFrom(src => src.Bids))
            .ReverseMap()
            .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.BidsDbs));
        
        CreateMap<Bid, BidsDb>()
            .ForMember(dest => dest.Auction, opt => opt.Ignore())
            .ReverseMap();
    }
}