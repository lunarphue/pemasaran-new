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
    public class UserController : Controller
    {
        MainDbContext db = new MainDbContext();

        // GET: /Users/
        
        public ActionResult Index()
        {
            Claim sessionRole = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role);
            Claim sessionName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userRole = sessionRole.Value;
            string userName = sessionName.Value;
            if (userRole.Equals("1"))
            {
                var users = db.Users.Include(u => u.Perusahaans);
                return View(users.ToList());
            }
            else if (userRole.Equals("2"))
            {
                var Iduser = db.Users.Where(u => u.Username == userName).Select(u => u.User_Id );
                int _id = int.Parse(Iduser.First().ToString());
                var users = db.Users.Where(u => u.Id_Atasan.Equals(_id)).Include(u => u.Perusahaans);
                return View(users.ToList());
            }
            
            return View();
        }
    
        // GET: /Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.Perusahaan_Id = new SelectList(db.Perusahaans, "Perusahaan_Id", "Nama");
            ViewBag.Id_Atasan = new SelectList(db.Users, "User_Id", "Name");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="User_Id,Email,Password,Name,Country,Perusahaan_Id,Username,Kecamatan,Telpon,Faks,Kategori,Aktif,Id_Atasan")] Users user)
        {
            //string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            Claim sessionUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            string userName = sessionUsername.Value;
            if (ModelState.IsValid)
            {
                user.Createdon = dateToday;
                user.Createdby = userName;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Perusahaan_Id = new SelectList(db.Perusahaans, "Perusahaan_Id", "Nama", user.Perusahaan_Id);
            return View(user);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Perusahaan_Id = new SelectList(db.Perusahaans, "Perusahaan_Id", "Nama", user.Perusahaan_Id);
            ViewBag.Id_Atasan = new SelectList(db.Users, "User_Id", "Name", user.Id_Atasan);
            return View(user);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="User_Id,Email,Password,Name,Country,Perusahaan_Id,Username,Kecamatan,Telpon,Faks,Kategori,Aktif,Id_Atasan")] Users user)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Perusahaan_Id = new SelectList(db.Perusahaans, "Perusahaan_Id", "Nama", user.Perusahaan_Id);
            ViewBag.Id_Atasan = new SelectList(db.Users, "User_Id", "Name", user.Id_Atasan);
            return View(user);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users user = db.Users.Find(id);
            db.Users.Remove(user);
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
