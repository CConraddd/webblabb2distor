using AutoMapper;
using webblabb2distor.Core;
using webblabb2distor.Persistence;

namespace webblabb2distor.Mappers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Auction, AuctionDB>().ReverseMap();
        CreateMap<Bid, BidsDb>().ReverseMap();
    }
}