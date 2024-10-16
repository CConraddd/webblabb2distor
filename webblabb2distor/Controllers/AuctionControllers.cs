using Microsoft.AspNetCore.Mvc;
using webblabb2distor.Core;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Models.Auctions;

namespace webblabb2distor.Controllers
{
    public class AuctionControllers : Controller
    {
        private IAuctionService _auctionService;

        public AuctionControllers(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        // GET: AuctionControllers
        public ActionResult Index()
        {
            var auctions = _auctionService.GetAllActiveAuctions();
            var auctionsVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }

        // GET: AuctionControllers/Details/5
        public ActionResult Details(int id)
        {
            throw new NotImplementedException("Not implemented");
        }

        // GET: AuctionControllers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionControllers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuctionVm auctionVm)
        {
            throw new NotImplementedException("Not implemented");
        }

        // GET: AuctionControllers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionControllers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionControllers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionControllers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
