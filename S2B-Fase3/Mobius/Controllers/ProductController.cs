using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobius.Models;
using System.Security.Principal;

namespace Mobius.Controllers
{
    public class ProductController : Controller
    {
        private ProductDbContext db = new ProductDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string user = filterContext.HttpContext.User.Identity.Name;
            if(user == null || user == "")
            {
                user = "Visitante";
            }
            //Para não encher o log com "GetImage" & "Relatorios"
            string action = filterContext.ActionDescriptor.ActionName;
            if(!action.Equals("GetImage") && !action.Equals("Relatorios"))
            {
                db.Relatorios.Add(
                    new Relatorio
                    {
                        Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        User = user,
                        Action = action,
                        LogDate = DateTime.Now
                    });
                db.SaveChanges();
            }
        }

        //If action filter does not work...
        private void PlusRating(IPrincipal user)
        {
            var u = user as MyProfile;
            u.Rating += 1;
            //Update user in DB
        }

        [Authorize(Users = "admin@s2b.br")]
        public ActionResult Relatorios()
        {
            return View(db.Relatorios.OrderBy(r => r.LogDate).ToList());
        }

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

        //Show User's negotiations
        [Authorize]
        public ActionResult Negotiations()
        {
            return View(db.Products.Where(
                    p => (p.BuyerEmail == User.Identity.Name) &&
                    ((p.Status == Status.Negotiating) || (p.Status == Status.Sold))
                ).ToList());
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
        public ActionResult Edit(/*[Bind(Include = "ProductID,Title,Description,Cost,Address,Status,Rating,ImageFile,ImageMimeType,ImageUrl,CategoryID,UserEmail",
                                    Exclude = "PublishDate,ExpirationDate")]*/
                                    Product product, HttpPostedFileBase image)
        {
            /*
             https://forums.asp.net/t/1999919.aspx?MVC+readonly+datetime+updates+to+01+01+0001
             Dates wheren't behaving properly...
             fetching product so Publish/Expiration dates won't be set as '1/1/0001' and create an exception...
             */
            var fetchedProduct = db.Products.Find(product.ProductID);
            fetchedProduct.Address = product.Address;
            fetchedProduct.CategoryID = product.CategoryID;
            fetchedProduct.Title = product.Title;
            fetchedProduct.Description = product.Description;
            fetchedProduct.Cost = product.Cost;
            if (ModelState.IsValid)
            {
                //if image object is not empty update the photo attribute, using image info.
                if (image != null)
                {
                    fetchedProduct.ImageMimeType = image.ContentType;
                    fetchedProduct.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(fetchedProduct.ImageFile, 0, image.ContentLength);
                }
                var teste = fetchedProduct.PublishDate;
                var teste2 = fetchedProduct.ExpirationDate;
                db.Entry(fetchedProduct).State = EntityState.Modified;
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
                return RedirectToAction("Browse");
            }
            return View(product);
        }

        // GET: Product/Donate/5
        [Authorize]
        public ActionResult Donate(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Donate/5
        [HttpPost, ActionName("Donate")]
        [ValidateAntiForgeryToken]
        public ActionResult DonationConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                product.Status = Status.Donated;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Browse");
            }
            return View(product);
        }

        // GET: Product/Negotiate/5
        [Authorize]
        public ActionResult Negotiate(int? id)
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

        // POST: Product/Negotiate/5
        [HttpPost, ActionName("Negotiate")]
        [ValidateAntiForgeryToken]
        public ActionResult StartNegotiation(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                product.Status = Status.Negotiating;
                product.BuyerEmail = User.Identity.Name;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/SellTo/5
        [Authorize]
        public ActionResult SellTo(int? id)
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

        // POST: Product/SellTo/5
        [HttpPost, ActionName("SellTo")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmNegotiation(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                product.Status = Status.Sold;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Browse");
            }
            return View(product);
        }

        // GET: Product/Rate/5
        [Authorize]
        public ActionResult Rate(int? id)
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

        // POST: Product/Rate/5
        [HttpPost, ActionName("Rate")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmRate(int id, int rating)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                if(product.Status == Status.Sold)
                {
                    product.Rating = rating;
                    product.Rated = true;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Negotiations");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
