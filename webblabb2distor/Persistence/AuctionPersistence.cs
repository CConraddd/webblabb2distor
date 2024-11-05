using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;

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

        public Auction GetAuctionById(int auctionId)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Include(a => a.BidsDbs)
                .FirstOrDefault(a => a.Id == auctionId);

            if (auctionDb == null) throw new DataException("Auction not found");

            var auction = _mapper.Map<Auction>(auctionDb);
            auction.Bids = auctionDb.BidsDbs.Select(b => _mapper.Map<Bid>(b)).ToList();

            return auction;
        }


        public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName)
        {
            try
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
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error while creating auction: " + ex.Message);
                throw new DataException("Could not save the auction to the database.", ex);
            }
        }

        public void UpdateAuction(Auction auction)
        {
            try
            {
                var auctionDb = _mapper.Map<AuctionDB>(auction);
                _dbContext.AuctionDbs.Update(auctionDb);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error while updating auction: " + ex.Message);
                throw new DataException("Could not update the auction in the database.", ex);
            }
        }

        public void DeleteAuction(int auctionId)
        {
            var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auctionId);
            if (auctionDb != null)
            {
                try
                {
                    _dbContext.AuctionDbs.Remove(auctionDb);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Error while deleting auction: " + ex.Message);
                    throw new DataException("Could not delete the auction from the database.", ex);
                }
            }
        }
    }
}
