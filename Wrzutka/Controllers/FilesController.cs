using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wrzutka.Data;
using Wrzutka.Models.Wrzutka;
using SelectPdf;

namespace Wrzutka.Controllers
{
    public class FilesController : Controller
    {
        private WrzutkaContext db = new WrzutkaContext();

        // GET: Files
        public ActionResult Index(string searchString, DateTime? start)
        {

            ViewBag.start = start;

            var files = from a in db.Files
                        select a;


            if (!String.IsNullOrEmpty(searchString))
            {
                files = files.Where(f => f.AuthorUserName.ToUpper().Contains(searchString.ToUpper())
                                    || f.FileName.ToUpper().Contains(searchString.ToUpper())
                                    || f.Description.ToUpper().Contains(searchString.ToUpper()));
            }

            if (start != null)
            {
                files = files.Where(s => s.CreationTime >= start);
            }

            return View(files.ToList());
            //return View(db.Files.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        [Authorize]
        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileID,AuthorUserID,FileName,Description")] File file, HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid && FileUpload != null)
            {
                try
                {
                    
                    file.AuthorUserID = User.Identity.GetUserId();
                    file.AuthorUserName = User.Identity.GetUserName();
                    file.CreationTime = DateTime.Now;
                    db.Files.Add(file);
                    db.SaveChanges();
                    UploadNote(file.FileID, FileUpload);
                    return RedirectToAction("Index");
                }
                catch
                {
                    
                }
            }

            return View(file);
        }
        public FileResult DownloadNote(int id)
        {
            string path = System.IO.Path.Combine(Server.MapPath("~/UploadUsers/NotesAttachements/" + id));
            string[] files = System.IO.Directory.GetFiles(path);
            string filePath = files.FirstOrDefault();
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new FileContentResult(fileBytes, filePath);
            response.FileDownloadName = System.IO.Path.GetFileName(filePath);
            return response;
        }


        private void UploadNote(int id, HttpPostedFileBase FileUpload)
        {
            var pathDir = Server.MapPath("~/UploadUsers/NotesAttachements/" + id.ToString());
            var path = System.IO.Path.Combine(Server.MapPath("~/UploadUsers/NotesAttachements/" + id.ToString()), System.IO.Path.GetFileName(FileUpload.FileName));
            System.IO.Directory.CreateDirectory(pathDir);
            FileUpload.SaveAs(path);
        }
        [Authorize]
        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileID,AuthorUserID,Description")] File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }
        [Authorize]
        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        [Authorize]
        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = db.Files.Find(id);
            db.Files.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void RecalculateAverage(int NoteID)
        {
            var note = db.Files.Where(n => n.FileID == NoteID).FirstOrDefault();
            List<Rating> rats = db.Ratings.Where(n => n.FileID == NoteID).ToList();
            double sum = 0;
            foreach(Rating r in rats)
            {
                sum += r.Value;
            }
            note.Average = (double)(sum / rats.Count());
            db.SaveChanges();
        }
        [Authorize]
        [HttpGet]
        public bool AddRating(int rate, int NoteID)
        {
            var userID = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            Rating rat = db.Ratings.Where(n => n.FileID == NoteID && n.UserID == userID).FirstOrDefault();
            if (rat == null)
            {
                db.Ratings.Add(new Rating { FileID = NoteID, UserID = userID, Value = rate });
            }
            else
            {
                rat.Value = rate;
            }
            db.SaveChanges();
            RecalculateAverage(NoteID);
            return true;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Generowanie PDFów
        public ActionResult GetPdf()
        {
            var converter = new HtmlToPdf();
            var doc = converter.ConvertUrl("http://localhost:56048/Files/Index");
            doc.Save(System.Web.HttpContext.Current.Response, true, "test.pdf");
            doc.Close();
            return null;
        }
    }
}
