using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstWebsite.Models;
using System.Security.Claims;
using System.Threading;
using System.Data.Entity;
using System.Net;

namespace MyFirstWebsite.Controllers
{
    public class AssignController : Controller
    {
        MainDbContext db = new MainDbContext();

        // GET: /Assigns/
        public ActionResult Index()
        {
            var assigns = db.Assigns.Include(a => a.User).Include(a => a.Pengecer);
            return View(assigns.ToList());
        }

        // GET: /Assigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigns assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // GET: /Assigns/Create
        [Authorize(Roles = "1,2")]
        public ActionResult Create()
        {
            Claim sessionRole = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role);
            string userRole = sessionRole.Value;
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name");
            ViewBag.Pengecer_Id = new SelectList(db.Pengecers, "Pengecer_Id", "Name");
            if (userRole.Equals("2"))
            { 
                Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid);
                int userId = Convert.ToInt32(sessionUsername.Value);
                ViewBag.User_Id = new SelectList(db.Users.Where(m => m.Id_Atasan == userId || m.User_Id == userId), "User_Id", "Name");
            }
            
            return View();
        }

        // POST: /Assigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Assign_Id,User_Id,Aktif,Pengecer_Id,Urea,NPK,SP36,ZA,Organik")] Assigns assign)
        {
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;

            if (ModelState.IsValid)
            {
                var dbassign = db.Assigns.Create();
                dbassign.Assign_Id = assign.Assign_Id;
                dbassign.User_Id = assign.User_Id;
                dbassign.Aktif = assign.Aktif;
                dbassign.Pengecer_Id = assign.Pengecer_Id;
                dbassign.Edited_by = userName;
                dbassign.Date_Edited = dateToday;
                dbassign.Time_Edited = timeToday;
                dbassign.Urea = assign.Urea;
                dbassign.NPK = assign.NPK;
                dbassign.SP36 = assign.SP36;
                dbassign.ZA = assign.ZA;
                dbassign.Organik = assign.Organik;
                
                db.Assigns.Add(dbassign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", assign.User_Id);
            ViewBag.Pengecer_Id = new SelectList(db.Pengecers, "Pengecer_Id", "Name", assign.Pengecer_Id);
            return View(assign);
        }

        // GET: /Assigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigns assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", assign.User_Id);
            ViewBag.Pengecer_Id = new SelectList(db.Pengecers, "Pengecer_Id", "Name", assign.Pengecer_Id);
            return View(assign);
        }

        // POST: /Assigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Assign_Id,User_Id,Aktif,Pengecer_Id,Urea,NPK,SP36,ZA,Organik")] Assigns assign)
        {
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;
            if (ModelState.IsValid)
            {
                assign.Time_Edited = timeToday;
                assign.Date_Edited = dateToday;
                assign.Edited_by = userName;
                db.Entry(assign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", assign.User_Id);
            ViewBag.Pengecer_Id = new SelectList(db.Pengecers, "Pengecer_Id", "Name", assign.Pengecer_Id);
            return View(assign);
        }

        // GET: /Assigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigns assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // POST: /Assigns/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assigns assign = db.Assigns.Find(id);
            db.Assigns.Remove(assign);
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
