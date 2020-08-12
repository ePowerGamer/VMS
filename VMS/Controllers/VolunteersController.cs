using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    public class VolunteersController : Controller
    {
        private VolunteerContext db = new VolunteerContext();

        // GET: Volunteers
        public ActionResult Index()
        {
            return View(db.Volunteers.ToList());
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerModel volunteerModel = db.Volunteers.Find(id);
            if (volunteerModel == null)
            {
                return HttpNotFound();
            }
            return View(volunteerModel);
        }

        // GET: Volunteers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,FirstName,LastName,Username,Password,Center,Interests,Availability,Address,PhoneNumber,Education,Licenses,EMCname,EMCphone,EMCemail,EMCaddress,DriversLicenseOnFile,SocialCardOnFile,ApprovalStatus")] VolunteerModel volunteerModel)
        {
            if (ModelState.IsValid)
            {
                db.Volunteers.Add(volunteerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteerModel);
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerModel volunteerModel = db.Volunteers.Find(id);
            if (volunteerModel == null)
            {
                return HttpNotFound();
            }
            return View(volunteerModel);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,FirstName,LastName,Username,Password,Center,Interests,Availability,Address,PhoneNumber,Education,Licenses,EMCname,EMCphone,EMCemail,EMCaddress,DriversLicenseOnFile,SocialCardOnFile,ApprovalStatus")] VolunteerModel volunteerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerModel);
        }

        // GET: Volunteers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerModel volunteerModel = db.Volunteers.Find(id);
            if (volunteerModel == null)
            {
                return HttpNotFound();
            }
            return View(volunteerModel);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VolunteerModel volunteerModel = db.Volunteers.Find(id);
            db.Volunteers.Remove(volunteerModel);
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
