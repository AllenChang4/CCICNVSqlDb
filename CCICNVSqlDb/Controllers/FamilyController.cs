using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CCICNVSqlDb.Models;
using System.Threading.Tasks;

namespace CCICNVSqlDb.Controllers
{
    public class FamiliesController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Families
        public ActionResult Index()
        {            
            Trace.WriteLine("GET /Families/Index");
            return View(db.Families.ToList());
        }

        // GET: Families/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Families/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // GET: Families/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Families/Create");
            return View(new Family { CreatedDate = DateTime.Now });
        }

        // POST: Families/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FamilyName,Parent,Children,ChineseName,Address,City,State,Zip,Phone,Description,CreatedDate")] Family family, HttpPostedFileBase image)
        {
            Trace.WriteLine("POST /Families/Create");
            try
            { 
                if (ModelState.IsValid)
                {
                    GetFamilyPicture(family,image);
                    db.Families.Add(family);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            { 
            }
            return View(family);
        }

        public void GetFamilyPicture(Family family, HttpPostedFileBase image)
        {
            try
            { 
                if (image != null && image.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(image.FileName);
                    using (var reader = new System.IO.BinaryReader(image.InputStream))
                    {
                        family.FamilyPicture = reader.ReadBytes(image.ContentLength);
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }

        // GET: Families/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Families/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Family family = db.Families.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }
        // POST: Families/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamilyName,Parent,Children,ChineseName,Address,City,State,Zip,Phone,FamilyPicture,Description,CreatedDate")] Family family, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        GetFamilyPicture(family, image);
                        db.Entry(family).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        db.Entry(family).State = EntityState.Modified;
                        db.Entry(family).Property(f => f.FamilyPicture).IsModified = false;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return View(family);
        }


        // GET: Families/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Families/Delete/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family Family = db.Families.Find(id);
            if (Family == null)
            {
                return HttpNotFound();
            }
            return View(Family);
        }

        // POST: Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trace.WriteLine("POST /Families/Delete/" + id);
            Family Family = db.Families.Find(id);
            db.Families.Remove(Family);
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
