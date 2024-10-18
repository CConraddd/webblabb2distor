using Microsoft.AspNetCore.Mvc;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Models.Auctions;

namespace webblabb2distor.Controllers
{
    public class AuctionControllers : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly string _testUserName = "testuser"; //hårdkodad user

        public AuctionControllers(IAuctionService auctionService, IBidService bidService)
        {
            _auctionService = auctionService;
            _bidService = bidService;
        }
        // GET: AuctionControllers
        public ActionResult Index()
        {
            var auctions = _auctionService.GetAllActiveAuctions();
            if (auctions == null)
            {
                auctions = new List<Auction>();
            }

            var auctionsVms = auctions.Select(AuctionVm.FromAuction).ToList();
            return View(auctionsVms);
        }


        // GET: AuctionControllers/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var auction = _auctionService.GetDetails(id);
                if (auction == null)
                {
                    return NotFound("Auction not found.");
                }

                var auctionDetailsVm = AuctionDetailsVm.FromAuction(auction);
                auctionDetailsVm.Bids = auction.Bids.OrderByDescending(b => b.Amount)
                    .Select(BidVm.FromBid).ToList();
                return View(auctionDetailsVm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: AuctionControllers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuctionVm auctionVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = User.Identity?.Name ?? _testUserName;  // Använd hårdkodad användare om ingen är inloggad
                    _auctionService.CreateAuction(auctionVm.Name, auctionVm.Description, auctionVm.StartingPrice, auctionVm.EndDateTime, username);
                    return RedirectToAction(nameof(Index));
                }
                return View(auctionVm);
            }
            catch
            {
                return View();
            }
        }
        
        
        // GET: AuctionController/Edit/5
        public ActionResult Edit(int id)
        {
            var auction = _auctionService.GetDetails(id);
            if (auction == null)
            {
                return NotFound();
            }
            var auctionVm = AuctionVm.FromAuction(auction);
            return View(auctionVm);
        }

        // POST: AuctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuctionVm auctionVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _auctionService.EditDescription(id, auctionVm.Description);
                    return RedirectToAction(nameof(Index));
                }
                return View(auctionVm);
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionController/Delete/5
        public ActionResult Delete(int id)
        {
            var auction = _auctionService.GetDetails(id);
            if (auction == null)
            {
                return NotFound();
            }
            var auctionVm = AuctionVm.FromAuction(auction);
            return View(auctionVm);
        }

        // POST: AuctionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _auctionService.DeleteAuction(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionController/MyBids
        public ActionResult MyBids()
        {
            var auctions = _bidService.GetBidsByUsername(User.Identity.Name)
                .Where(b => b.Auction.EndDateTime > DateTime.Now)
                .Select(b => b.Auction)
                .Distinct();
            var auctionVms = auctions.Select(a => AuctionVm.FromAuction(a)).ToList();
            return View(auctionVms);
        }

        // GET: AuctionController/MyWonAuctions
        public ActionResult MyWonAuctions()
        {
            var wonAuctions = _auctionService.GetWonAuctions(User.Identity.Name);
            var auctionVms = wonAuctions.Select(a => AuctionVm.FromAuction(a)).ToList();
            return View(auctionVms);
        }
    }
}
