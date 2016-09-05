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
    public class StokPengecerController : Controller
    {
        MainDbContext db = new MainDbContext();

        // GET: /StokPengecers/
        public ActionResult Index()
        {
            var stokpengecers = db.StokPengecers.Include(s => s.Assign);
            return View(stokpengecers.ToList());

        }
        [HttpPost]
        public ActionResult Index(FormCollection c)
        {
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;

            int i = 0;
            if (ModelState.IsValid)
            {
                var StokId = c.GetValues("item.StokPengecer_Id");
                var StokSP36 = c.GetValues("item.SP36");
                var StokNPK = c.GetValues("item.NPK");
                var StokZA = c.GetValues("item.ZA");
                var StokOrganik = c.GetValues("item.Organik");
                var StokUrea = c.GetValues("item.Urea");

                for (i = 0; i < StokId.Count(); i++)
                {
                    StokPengecers stkp = db.StokPengecers.Find(Convert.ToInt32(StokId[i]));
                    stkp.Urea = Convert.ToInt32(StokUrea[i]);
                    stkp.ZA = Convert.ToInt32(StokZA[i]);
                    stkp.SP36 = Convert.ToInt32(StokSP36[i]);
                    stkp.Organik = Convert.ToInt32(StokOrganik[i]);
                    stkp.NPK = Convert.ToInt32(StokNPK[i]);
                    stkp.Time_Edited = timeToday;
                    stkp.Date_Edited = dateToday;
                    stkp.Edited_by = userName;
                    db.Entry(stkp).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            var stokpengecers = db.StokPengecers.Include(s => s.Assign);
            return View(stokpengecers.ToList());

        }
       

        public ActionResult Insert(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokPengecers StokPengecers = db.StokPengecers.Find(id);
            if (StokPengecers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Assign_Id = new SelectList(db.Assigns, "Assign_Id", "Aktif", StokPengecers.Assign_Id);
            return View(StokPengecers);
        }

        // GET: /StokPengecers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokPengecers StokPengecers = db.StokPengecers.Find(id);
            if (StokPengecers == null)
            {
                return HttpNotFound();
            }
            return View(StokPengecers);
        }

        // GET: /StokPengecers/Create
        public ActionResult Create()
        {
            ViewBag.Assign_Id = new SelectList(db.Assigns, "Assign_Id", "Pengecer_Id");
            return View();
        }

        // POST: /StokPengecers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StokPengecer_Id,Assign_Id,Urea,NPK,SP36,ZA,Organik")] StokPengecers StokPengecers)
        {
            //if (ModelState.IsValid)
            //{
            //    db.StokPengecers.Add(StokPengecers);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;

            if (ModelState.IsValid)
            {
                var dbstokpengecer = db.StokPengecers.Create();
                dbstokpengecer.Assign_Id = StokPengecers.Assign_Id;
                dbstokpengecer.Urea = StokPengecers.Urea;
                dbstokpengecer.NPK = StokPengecers.NPK;
                dbstokpengecer.ZA = StokPengecers.ZA;
                dbstokpengecer.SP36 = StokPengecers.SP36;
                dbstokpengecer.Organik = StokPengecers.Organik;
                dbstokpengecer.Date_Edited = dateToday;
                dbstokpengecer.Edited_by = userName;
                dbstokpengecer.Time_Edited = timeToday;
                db.StokPengecers.Add(dbstokpengecer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Assign_Id = new SelectList(db.Assigns, "Assign_Id", "Aktif", StokPengecers.Assign_Id);
            return View(StokPengecers);
        }

        // GET: /StokPengecers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokPengecers StokPengecers = db.StokPengecers.Find(id);
            if (StokPengecers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Assign_Id = new SelectList(db.Assigns, "Assign_Id", "Aktif", StokPengecers.Assign_Id);
            return View(StokPengecers);
        }

        // POST: /StokPengecers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StokPengecer_Id,Assign_Id,Date_Edited,Time_Edited,Urea,NPK,SP36,ZA,Organik")] StokPengecers StokPengecers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(StokPengecers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Assign_Id = new SelectList(db.Assigns, "Assign_Id", "Aktif", StokPengecers.Assign_Id);
            return View(StokPengecers);
        }

        // GET: /StokPengecers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokPengecers StokPengecers = db.StokPengecers.Find(id);
            if (StokPengecers == null)
            {
                return HttpNotFound();
            }
            return View(StokPengecers);
        }

        // POST: /StokPengecers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokPengecers StokPengecers = db.StokPengecers.Find(id);
            db.StokPengecers.Remove(StokPengecers);
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
