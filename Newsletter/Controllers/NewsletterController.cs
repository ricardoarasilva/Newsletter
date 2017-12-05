using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newsletter.Models;

namespace Newsletter.Controllers
{
    public class NewsletterController : Controller
    {
        private NewsletterDbContext db = new NewsletterDbContext();

        // GET: Newsletter
        public ActionResult Index()
        {
            return View(db.NewsLetterSigns.ToList());
        }

        // GET: Newsletter/Details/5
        public ActionResult Details(int? id)
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

        // GET: Newsletter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Newsletter/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,HowHeardAboutUs,ReasonToSigningUp")] NewsLetterSign newsLetterSign)
        {
            if (ModelState.IsValid)
            {
                db.NewsLetterSigns.Add(newsLetterSign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsLetterSign);
        }

        // GET: Newsletter/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Newsletter/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,HowHeardAboutUs,ReasonToSigningUp")] NewsLetterSign newsLetterSign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsLetterSign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsLetterSign);
        }

        // GET: Newsletter/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Newsletter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsLetterSign newsLetterSign = db.NewsLetterSigns.Find(id);
            db.NewsLetterSigns.Remove(newsLetterSign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult DoesEmailExists(string email)
        {

            var sign = db.NewsLetterSigns.FirstOrDefault(a=>a.Email == email);

            return Json(sign == null);
        }
    }
}
