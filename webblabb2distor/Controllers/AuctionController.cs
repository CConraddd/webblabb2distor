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
                var auction = _auctionService.GetDetails(id);

                if (auction == null)
                {
                    return NotFound("Auction not found");
                }
                
                var auctionDetailsVm = AuctionDetailsVm.FromAuction(auction);
                auctionDetailsVm.Bids = auction.Bids.OrderByDescending(b => b.Bidamount).Select(BidVm.FromBid).ToList();

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
        public IActionResult Create(CreateAuctionVm createAuctionVm)
        {
            if (!ModelState.IsValid)
            {
                return View(createAuctionVm);
            }

            try
            {
                _auctionService.CreateAuction(
                    createAuctionVm.Name,
                    createAuctionVm.Description,
                    createAuctionVm.StartingPrice,
                    createAuctionVm.EndDateTime,
                    User.Identity.Name
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating auction: {ex.Message}");
                return View(createAuctionVm);
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
                _auctionService.EditDescription(id, auctionVm.Description, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(auctionVm);
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
                _auctionService.DeleteAuction(id, User.Identity.Name);
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
            var bidVms = userBids.Select(b => BidVm.FromBid(b)).ToList();
            return View(bidVms);
        }


        //returns my won auctions to the user
        public ActionResult MyWonAuctions()
        {
            try
            {
                var wonAuctions = _auctionService.GetWonAuctions(User.Identity.Name);
                var auctionVms = wonAuctions.Select(AuctionVm.FromAuction).ToList();
                return View(auctionVms);
            }
            catch (Exception)
            {
                return View(new List<AuctionVm>());
            }
        }
        
        //places bid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(int auctionId, decimal bidAmount)
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