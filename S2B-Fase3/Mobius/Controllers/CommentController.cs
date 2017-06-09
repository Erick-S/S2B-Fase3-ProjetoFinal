using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobius.Models;
using System.Data.Entity.Infrastructure;

namespace Mobius.Controllers
{
    public class CommentController : Controller
    {
        private ProductDbContext db = new ProductDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View(db.Comments.Where(c => (c.UserEmail == User.Identity.Name) || (c.AnnouncerEmail == User.Identity.Name)).ToList());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comment/Create
        [Authorize]
        public ActionResult Create(int id, string productName, string announcerEmail)
        {
            ViewBag.ProductName = productName;
            ViewBag.ProductID = id;
            ViewBag.AnnoucerEmail = announcerEmail;
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,CommentDate,Comentario,Resposta,UserEmail")] Comment comment, int id, string announcerEmail)
        {
            if (ModelState.IsValid)
            {
                comment.ProductID = id;
                comment.UserEmail = User.Identity.Name;
                comment.AnnouncerEmail = announcerEmail;
                comment.CommentDate = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // TODO Fix comments 
        // https://stackoverflow.com/questions/23201907/asp-net-mvc-attaching-an-entity-of-type-modelname-failed-because-another-ent
        // GET: Comment/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResposta(Comment comment)
        {
            if (ModelState.IsValid)
            {
                bool saveFailed;
                do
                {
                    saveFailed = false;

                    try
                    {
                        db.Entry(comment).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update original values from the database 
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }

                } while (saveFailed);
            }
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComentario(Comment comment)
        {
            if (ModelState.IsValid)
            {
                bool saveFailed;
                do
                {
                    saveFailed = false;

                    try
                    {
                        db.Entry(comment).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update original values from the database 
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }

                } while (saveFailed);
            }
            return View(comment);
        }

        // GET: Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            if (comment.UserEmail == User.Identity.Name)
            {
                return View(comment);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
    }
}
