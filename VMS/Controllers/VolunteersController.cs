using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    public class VolunteersController : Controller
    {
        private VolunteerContext db = new VolunteerContext();

        public VolunteersController()
        {

        }

        // GET: Volunteers
        public ActionResult Index()
        {
            Debug.WriteLine("Imagine using POST lol");
            return View(db.Volunteers.ToList());
        }

        // POST: Volunteers
        [HttpPost]
        public ActionResult Index(string filter)
        {
            ViewData["filter"] = filter;
            if(filter == "approved")
            {
                return View(db.Volunteers.Where(o => o.ApprovalStatus == "approved").ToList());
            }
            else if(filter == "pending")
            {
                return View(db.Volunteers.Where(o => o.ApprovalStatus == "pending").ToList());
            }

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

        public ActionResult Matches(string centers, string interests)
        {
            //Properties to match: Centers, Availability, interests
            //Current just interests and Centers are matched with the opportunity tags

            using (OpportunityContext oc = new OpportunityContext()) {
                var interest_list = interests.Split(',');
                var center_list = centers.Split(',');

                List<OpportunityModel> matches = new List<OpportunityModel>();
                //List<OpportunityModel> interest_matches = new List<OpportunityModel>();
                //List<OpportunityModel> center_matches = new List<OpportunityModel>();


                foreach (var item in interest_list)
                {
                    //interest_matches.AddRange(oc.Opportunities.Where(o => o.Tags.Contains(item)).ToList());

                    foreach (var place in center_list)
                    {
                        matches.AddRange(oc.Opportunities.Where(o => (o.Center.Contains(place)) && (o.Tags.Contains(item))).ToList());
                    }
                }

                //matches.AddRange(interest_matches);
                //matches.AddRange(center_matches);

                return View(matches.Distinct());
            }

        }
        public async Task<ActionResult> SearchVol(string searchString)
        {
            var volunteers = from v in db.Volunteers select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                volunteers = volunteers.Where(s => s.FirstName.Contains(searchString));
            }
            return View(await volunteers.ToListAsync());
        }
    }
}
