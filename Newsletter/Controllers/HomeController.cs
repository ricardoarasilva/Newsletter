using Newsletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Newsletter.Controllers
{
    public class HomeController : Controller
    {
        private NewsletterDbContext db = new NewsletterDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Email,HowHeardAboutUs,ReasonToSigningUp")] NewsLetterSign newsLetterSign)
        {
            if (ModelState.IsValid)
            {
                db.NewsLetterSigns.Add(newsLetterSign);
                db.SaveChanges();
                return RedirectToAction("Success",new { id = newsLetterSign.Id});
            }

            return View(newsLetterSign);
        }

        public ActionResult Success(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetterSign newsLetterSign = db.NewsLetterSigns.Find(id);
            if (newsLetterSign == null)
            {
                return HttpNotFound();
            }
            return View(newsLetterSign);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}