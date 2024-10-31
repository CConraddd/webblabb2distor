using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Persistence;

namespace webblabb2distor.Persistence
{
    public class AuctionPersistence : IAuctionPersistence
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Auction> GetActiveAuctions()
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.Enddate > DateTime.Now)
                .ToList();

            return auctionDbs.Select(a => _mapper.Map<Auction>(a)).ToList();
        }

        public Auction GetAuctionById(int auctionId, string username)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == auctionId && a.Sellername == username)
                .Include(a => a.BidsDbs)
                .FirstOrDefault();

            if (auctionDb == null) throw new DataException("Auction not found");

            var auction = _mapper.Map<Auction>(auctionDb);
            auction.Bids = auctionDb.BidsDbs.Select(b => _mapper.Map<Bid>(b)).ToList();

            return auction;
        }

        public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName)
        {
            var auctionDb = new AuctionDB
            {
                name = name,
                description = description,
                price = startingPrice,
                Enddate = endDate,
                Sellername = userName
            };

            _dbContext.AuctionDbs.Add(auctionDb);
            _dbContext.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            var auctionDb = _mapper.Map<AuctionDB>(auction);
            _dbContext.AuctionDbs.Update(auctionDb);
            _dbContext.SaveChanges();
        }

        public void DeleteAuction(int auctionId)
        {
            var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auctionId);
            if (auctionDb != null)
            {
                _dbContext.AuctionDbs.Remove(auctionDb);
                _dbContext.SaveChanges();
            }
        }
    }
}
