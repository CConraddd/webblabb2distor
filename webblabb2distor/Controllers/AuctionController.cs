using Microsoft.AspNetCore.Mvc;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Models.Auctions;
using Microsoft.AspNetCore.Authorization;

namespace webblabb2distor.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;

        public AuctionController(IAuctionService auctionService, IBidService bidService)
        {
            _auctionService = auctionService;
            _bidService = bidService;
        }
        // GET: AuctionControllers
        public ActionResult Index()
        {
            var activeAuctions = _auctionService.GetAllActiveAuctions();
            var auctionVms = new List<AuctionVm>();
            foreach (var auction in activeAuctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }

            return View(auctionVms);
        }


        // GET: AuctionControllers/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Console.WriteLine($"Fetching details for Auction ID: {id}");
                var auction = _auctionService.GetDetails(id);

                if (auction == null)
                {
                    Console.WriteLine($"Auction with ID {id} not found.");
                    return NotFound("Auction not found");
                }

                Console.WriteLine($"Auction details found: {auction.Name}, with {auction.Bids.Count} bids.");

                var auctionDetailsVm = AuctionDetailsVm.FromAuction(auction);
                auctionDetailsVm.Bids = auction.Bids.OrderByDescending(b => b.Bidamount).Select(BidVm.FromBid).ToList();

                return View(auctionDetailsVm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Details: {ex.Message}");
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
            ModelState.Remove(nameof(auctionVm.SellerUsername));

            auctionVm.SellerUsername = User.Identity.Name;
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine("Validation Error: " + error.ErrorMessage);
                    }
                }
                return View(auctionVm);
            }
            try
            {
                _auctionService.CreateAuction(
                    auctionVm.Name, 
                    auctionVm.Description, 
                    auctionVm.StartingPrice, 
                    auctionVm.EndDateTime, 
                    auctionVm.SellerUsername);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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
            var userBids = _bidService.GetBidsByUsername(User.Identity.Name);
            foreach (var bid in userBids)
            {
                Console.WriteLine($"Bid ID: {bid.Id}, Auction ID: {bid.AuctionId}, Amount: {bid.Bidamount}, Bidder: {bid.Biddername}");
            }
            var bidVms = userBids.Select(b => BidVm.FromBid(b)).ToList();
            return View(bidVms);
        }


//returns my won auctions to the user
        public ActionResult MyWonAuctions()
        {
            var wonAuctions = _auctionService.GetWonAuctions(User.Identity.Name);
            var auctionVms = wonAuctions.Select(AuctionVm.FromAuction).ToList();
            return View(auctionVms);
        }
        
        //places bid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceBid(int auctionId, decimal bidAmount)
        {
            try
            {
                _bidService.PlaceBid(auctionId, User.Identity.Name, bidAmount);
                return RedirectToAction("Details", new { id = auctionId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}