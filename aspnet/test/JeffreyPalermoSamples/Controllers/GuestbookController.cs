using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guestbook.Modles;
using JeffreyPalermoSamples.Models;

namespace JeffreyPalermoSamples.Controllers
{
    public class GuestbookController : Controller
    {
        private GuestbookContext _db = new GuestbookContext();

        public ActionResult Index()
        {
            var mostRecentEntries =
            (from entry in _db.Entries
             orderby entry.DateAdded descending
             select entry).Take(20);
            ViewBag.Entries = mostRecentEntries.ToList();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GuestbookEntry entry)
        {
            entry.DateAdded = DateTime.Now;
                _db.Entries.Add(entry);
            _db.SaveChanges();
            //return Content("New entry successfully added.");
            return RedirectToAction("Index");
        }

        public ViewResult Show(int id)
        {
        var entry = _db.Entries.Find(id);
        bool hasPermission = User.Identity.Name == entry.Name;
        ViewData["hasPermission"] = hasPermission;
        return View(entry);
        }


    }
}
