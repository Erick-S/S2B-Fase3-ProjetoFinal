using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobius.Models;

// TODO project

//User.Identity.Name -> fetchs user identity...

//Crud methods for product & others...

    //create
//Create -> Authorize -> User creates an product... [Product] DONE
//Create -> Authorize -> User creates an comment... [Comment?]

    //read
//View -> Authorize -> Show user's products? [Product] DONE
//Browse -> !Authorize -> Show all products? [Product] TODO ADD FILTERS
//*method* -> show all products, admin action? [Product]
//View -> Authorize -> User view product's comments [Comment?]
//View? -> Authorize -> User view comments directed to own products? [Comment?]

    //update
    //delete
//Delete -> Admin action? [Product]
//Cancel -> Authorize -> User canceling product? (Delete or Update?) [Product] DONE -> Removed delete action... will it be needed?
//Sell -> Authorize -> User sellin product to another? (Delete or Update?) [Product]
//Donate -> Authorize -> User donatin product (Delete or Update?) [Product]
//Update -> Authorize -> User is able to change product? [Product] BROKEN -> Publish/ExpirationDate being returned/updated as '1/1/0001'
//Check expirationDate -> Admin action... belongs here? [Product] DONE
//Update -> Authorize -> User alters comment? [Comment?]
//Delete -> Authorize -> User deletes own comment? [Comment?]
//Delete -> Admin action...admin deletes comment? [Comment?]

    //Sketch pages!
    //Decide on user class (ApplicationUser || MyProfile)
    //Update bootstrap theme DONE
    //Create pages
    //Update controller(s)
    //Add more logic... (Rating...)

namespace Mobius.Controllers
{
    public class ProductController : Controller
    {
        private ProductDbContext db = new ProductDbContext();

        //Return image file
        public ActionResult GetImage(int id)
        {
            Product product = db.Products.Find(id);
            if(product != null && product.ImageFile != null)
            {
                //File used to return a binary content and the contenttype of the returned photo
                return File(product.ImageFile, product.ImageMimeType);
            }
            else
            {
                //Should return a default "Image not found" image
                return new FilePathResult("~/Images/nao-disponivel.jpg", "image/jpeg");
            }
        }

        [Authorize(Users = "admin@s2b.br")]
        public ActionResult CheckExpirationDate()
        {
            var products = db.Products.ToList();
            products.ForEach(p =>
            {
                if(DateTime.Compare(p.ExpirationDate, DateTime.Now) < 0)
                {
                    p.Status = Status.Expired;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
            });

            return RedirectToAction("Index", "Manage", null);
        }

        // GET: Product
        //Browse Method...List all products... TODO Create Filters
        public ActionResult Index()
        {
            return View(db.Products.Where(p => p.Status == Status.Open).ToList());
        }

        // Show User's Products
        [Authorize]
        public ActionResult Browse()
        {
            return View(db.Products.Where(p => p.UserEmail == User.Identity.Name).ToList());
        }

        // GET: Product/Details/5
        // Show Product details to product owner...
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if(User.Identity.Name != product.UserEmail)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(product);
        }

        // GET: Product/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Title,Description,Cost,Address,PublishDate,ExpirationDate,Status,Rating,ImageFile,ImageMimeType,ImageUrl,CategoryID,UserEmail")]
                                    Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //if image object is not empty update the photo attribute, using image info.
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(product.ImageFile, 0, image.ContentLength);
                }
                product.PublishDate = DateTime.Now;
                product.ExpirationDate = DateTime.Now.AddDays(60);
                product.Status = Status.Open;
                product.Rating = 0;
                product.UserEmail = User.Identity.Name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Product/Edit/5
        // TODO User should be able to edit products?
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,Description,Cost,Address,Status,Rating,ImageFile,ImageMimeType,ImageUrl,CategoryID,UserEmail", Exclude = "PublishDate,ExpirationDate")]
                                    Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                // TODO FIXME Dates are being returned/set as '1/1/0001'
                //if image object is not empty update the photo attribute, using image info.
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(product.ImageFile, 0, image.ContentLength);
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Product/Cancel/5
        // Delete Changed to -> Cancel
        [Authorize]
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if(product != null)
            {
                product.Status = Status.Cancelled;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
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
