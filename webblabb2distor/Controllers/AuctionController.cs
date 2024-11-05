using Microsoft.AspNetCore.Mvc;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Models.Auctions;

namespace webblabb2distor.Controllers
{
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
                    return BadRequest();
                }

                var auctionDetailsVm = AuctionDetailsVm.FromAuction(auction);
                auctionDetailsVm.Bids = auction.Bids.OrderByDescending(b => b.Amount).Select(BidVm.FromBid).ToList();
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
            var auctions = _bidService.GetBidsByUsername(User.Identity.Name)
                .Where(b => b.Auction.EndDateTime > DateTime.Now)
                .Select(b => b.Auction)
                .Distinct();
            var auctionVms = auctions.Select(AuctionVm.FromAuction).ToList();
            return View(auctionVms);
        }

        // GET: AuctionController/MyWonAuctions
        public ActionResult MyWonAuctions()
        {
            var wonAuctions = _auctionService.GetWonAuctions(User.Identity.Name);
            var auctionVms = wonAuctions.Select(AuctionVm.FromAuction).ToList();
            return View(auctionVms);
        }
    }
}