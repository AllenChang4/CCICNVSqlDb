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
            Family Family = db.Families.Find(id);
            if (Family == null)
            {
                return HttpNotFound();
            }
            return View(Family);
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
        public ActionResult Create([Bind(Include = "FamilyName,Parent,Children,ChineseName,Address,City,State,Zip,Phone,Files,Description,CreatedDate")] Family family, HttpPostedFileBase image)
        {
            Trace.WriteLine("POST /Families/Create");
            try
            { 
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var fFile = new File
                        {
                            FileName = System.IO.Path.GetFileName(image.FileName),
                            FileType = FileType.Avatar,
                            ContentType = image.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(image.InputStream))
                        {
                            fFile.Content = reader.ReadBytes(image.ContentLength);
                        }
                        family.Files = new List<File> { fFile };
                    }
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
        //public FileContentResult GetThumbnailImage(int Id)
        //{
        //    Family family = db.Families.FirstOrDefault(p => p.ID == Id);
        //    if (family != null)
        //    {
        //        return File(family.Thumbnail, family.ImageMimeType.ToString());
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        // GET: Families/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Families/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            //Family Family = db.Families.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,FamilyName,Parent,Children,ChineseName,Address,City,State,Zip,Phone,Files,Description,CreatedDate")] Family family, HttpPostedFileBase image)
        {
            if (family == null) return View(family); 
            Trace.WriteLine("POST /Families/Edit/" + family.ID);
            int id = family.ID;
            Family oldfamily = db.Families.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        if (family.Files != null)
                        {
                            if (family.Files.Any(f => f.FileType == FileType.Avatar))
                            {
                                db.Files.Remove(family.Files.First(f => f.FileType == FileType.Avatar));
                            }
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(image.FileName),
                            FileType = FileType.Avatar,
                            ContentType = image.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(image.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(image.ContentLength);
                        }
                        family.Files = new List<File> { avatar };
                    }
                    else
                    {
                        var avatar = oldfamily.Files.First(f => f.FileType == FileType.Avatar);
                        family.Files = new List<File> { avatar };
                    }
                    var trackedEntity = db.Families.Find(family.ID);
                    db.Entry(trackedEntity).State = EntityState.Modified;
                    //db.Entry(family).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
