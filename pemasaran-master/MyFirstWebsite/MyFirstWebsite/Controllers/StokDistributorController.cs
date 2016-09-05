using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFirstWebsite.Models;
using System.Security.Claims;

namespace MyFirstWebsite.Controllers
{
    public class StokDistributorController : Controller
    {
        MainDbContext db = new MainDbContext();

        // GET: /StokDistributors/
        public ActionResult Index()
        {
            var stokdistributors = db.StokDistributors.Include(s => s.DistributorAssigns);
            return View(stokdistributors.ToList());
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
                var StokId = c.GetValues("item.StokDistributor_Id");
                var StokSP36 = c.GetValues("item.SP36");
                var StokNPK = c.GetValues("item.NPK");
                var StokZA = c.GetValues("item.ZA");
                var StokOrganik = c.GetValues("item.Organik");
                var StokUrea = c.GetValues("item.Urea");

                for (i = 0; i < StokId.Count(); i++)
                {
                    StokDistributors stkd = db.StokDistributors.Find(Convert.ToInt32(StokId[i]));
                    stkd.Urea = Convert.ToInt32(StokUrea[i]);
                    stkd.ZA = Convert.ToInt32(StokZA[i]);
                    stkd.SP36 = Convert.ToInt32(StokSP36[i]);
                    stkd.Organik = Convert.ToInt32(StokOrganik[i]);
                    stkd.NPK = Convert.ToInt32(StokNPK[i]);
                    stkd.Time_Edited = timeToday;
                    stkd.Date_Edited = dateToday;
                    stkd.Edited_by = userName;
                    db.Entry(stkd).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            var stokdistributors = db.StokDistributors.Include(s => s.DistributorAssigns);
            return View(stokdistributors.ToList());

        }

        // GET: /StokDistributors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokDistributors stokdistributor = db.StokDistributors.Find(id);
            if (stokdistributor == null)
            {
                return HttpNotFound();
            }
            return View(stokdistributor);
        }

        // GET: /StokDistributors/Create
        public ActionResult Create()
        {
            ViewBag.AssignDistributor_Id = new SelectList(db.DistributorAssigns, "DistributorAssign_Id", "Aktif");
            return View();
        }

        // POST: /StokDistributors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StokDistributor_Id,DistributorAssign_Id,Date_Edited,Time_Edited,Urea,NPK,SP36,ZA,Organik")] StokDistributors stokdistributor)
        {
            if (ModelState.IsValid)
            {
                db.StokDistributors.Add(stokdistributor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistributorAssign_Id = new SelectList(db.DistributorAssigns, "DistributorAssign_Id", "Aktif", stokdistributor.DistributorAssign_Id);
            return View(stokdistributor);
        }

        // GET: /StokDistributors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokDistributors stokdistributor = db.StokDistributors.Find(id);
            if (stokdistributor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistributorAssign_Id = new SelectList(db.DistributorAssigns, "DistributorAssign_Id", "Aktif", stokdistributor.DistributorAssign_Id);
            return View(stokdistributor);
        }

        // POST: /StokDistributors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StokDistributor_Id,DistributorAssign_Id,Date_Edited,Time_Edited,Urea,NPK,SP36,ZA,Organik")] StokDistributors stokdistributor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokdistributor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistributorAssign_Id = new SelectList(db.DistributorAssigns, "DistributorAssign_Id", "Aktif", stokdistributor.DistributorAssign_Id);
            return View(stokdistributor);
        }

        // GET: /StokDistributors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokDistributors stokdistributor = db.StokDistributors.Find(id);
            if (stokdistributor == null)
            {
                return HttpNotFound();
            }
            return View(stokdistributor);
        }

        // POST: /StokDistributors/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokDistributors stokdistributor = db.StokDistributors.Find(id);
            db.StokDistributors.Remove(stokdistributor);
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
