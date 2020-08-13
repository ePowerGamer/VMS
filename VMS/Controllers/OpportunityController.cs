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
    public class OpportunityController : Controller
    {
        private OpportunityContext db = new OpportunityContext();

        // GET: Opportunity
        public ActionResult Index()
        {
            return View(db.Opportunities.ToList());
        }

        // GET: Opportunity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpportunityModel opportunityModel = db.Opportunities.Find(id);
            if (opportunityModel == null)
            {
                return HttpNotFound();
            }
            return View(opportunityModel);
        }

        // GET: Opportunity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opportunity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Center,Tags,Days,Time")] OpportunityModel opportunityModel)
        {
            if (ModelState.IsValid)
            {
                db.Opportunities.Add(opportunityModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(opportunityModel);
        }

        // GET: Opportunity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpportunityModel opportunityModel = db.Opportunities.Find(id);
            if (opportunityModel == null)
            {
                return HttpNotFound();
            }
            return View(opportunityModel);
        }

        // POST: Opportunity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Center,Tags,Days,Time")] OpportunityModel opportunityModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opportunityModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(opportunityModel);
        }

        // GET: Opportunity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpportunityModel opportunityModel = db.Opportunities.Find(id);
            if (opportunityModel == null)
            {
                return HttpNotFound();
            }
            return View(opportunityModel);
        }

        // POST: Opportunity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpportunityModel opportunityModel = db.Opportunities.Find(id);
            db.Opportunities.Remove(opportunityModel);
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
