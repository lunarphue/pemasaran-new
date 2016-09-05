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
    public class DistributorAssignController : Controller
    {
        MainDbContext db = new MainDbContext();

        // GET: /DistributorAssigns/
        public ActionResult Index()
        {
            var distributorassigns = db.DistributorAssigns.Include(d => d.User).Include(d => d.Distributor);
            return View(distributorassigns.ToList());
        }

        // GET: /DistributorAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributorAssigns distributorassign = db.DistributorAssigns.Find(id);
            if (distributorassign == null)
            {
                return HttpNotFound();
            }
            return View(distributorassign);
        }

        // GET: /DistributorAssigns/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name");
            ViewBag.Distributor_Id = new SelectList(db.Distributors, "Distributor_Id", "Name");
            return View();
        }

        // POST: /DistributorAssigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DistributorAssign_Id,User_Id,Aktif,Distributor_Id,Urea,NPK,SP36,ZA,Organik")] DistributorAssigns distributorassign)
        {
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;

            if (ModelState.IsValid)
            {
                var dbdistributorassign = db.DistributorAssigns.Create();
                dbdistributorassign.DistributorAssign_Id = distributorassign.DistributorAssign_Id;
                dbdistributorassign.User_Id = distributorassign.User_Id;
                dbdistributorassign.Distributor_Id = distributorassign.Distributor_Id;
                dbdistributorassign.Aktif = distributorassign.Aktif;
                dbdistributorassign.Urea = distributorassign.Urea;
                dbdistributorassign.NPK = distributorassign.NPK;
                dbdistributorassign.SP36 = distributorassign.SP36;
                dbdistributorassign.ZA = distributorassign.ZA;
                dbdistributorassign.Organik = distributorassign.Organik;
                dbdistributorassign.Time_Edited = timeToday;
                dbdistributorassign.Date_Edited = dateToday;
                dbdistributorassign.Edited_by = userName;
                db.DistributorAssigns.Add(dbdistributorassign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", distributorassign.User_Id);
            ViewBag.Distributor_Id = new SelectList(db.Distributors, "Distributor_Id", "Name", distributorassign.Distributor_Id);
            return View(distributorassign);
        }

        // GET: /DistributorAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributorAssigns distributorassign = db.DistributorAssigns.Find(id);
            if (distributorassign == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", distributorassign.User_Id);
            ViewBag.Distributor_Id = new SelectList(db.Distributors, "Distributor_Id", "Name", distributorassign.Distributor_Id);
            return View(distributorassign);
        }

        // POST: /DistributorAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DistributorAssign_Id,User_Id,Aktif,Distributor_Id,Urea,NPK,SP36,ZA,Organik")] DistributorAssigns distributorassign)
        {
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;
            if (ModelState.IsValid)
            {
                distributorassign.Date_Edited = dateToday;
                distributorassign.Time_Edited = timeToday;
                distributorassign.Edited_by = userName;
                db.Entry(distributorassign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", distributorassign.User_Id);
            ViewBag.Distributor_Id = new SelectList(db.Distributors, "Distributor_Id", "Name", distributorassign.Distributor_Id);
            return View(distributorassign);
        }

        // GET: /DistributorAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributorAssigns distributorassign = db.DistributorAssigns.Find(id);
            if (distributorassign == null)
            {
                return HttpNotFound();
            }
            return View(distributorassign);
        }

        // POST: /DistributorAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DistributorAssigns distributorassign = db.DistributorAssigns.Find(id);
            db.DistributorAssigns.Remove(distributorassign);
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
