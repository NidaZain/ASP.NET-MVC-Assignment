using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureesController : Controller
    {
        private InsuranceContext db = new InsuranceContext();

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Insuree insuree)
        {
            decimal quote = 50;

            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;

            if (insuree.DateOfBirth > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            // Age Rules
            if (age <= 18)
            {
                quote += 100;
            }
            else if (age >= 19 && age <= 25)
            {
                quote += 50;
            }
            else
            {
                quote += 25;
            }

            // Car Year Rules
            if (insuree.CarYear < 2000)
            {
                quote += 25;
            }

            if (insuree.CarYear > 2015)
            {
                quote += 25;
            }

            // Porsche Rules
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;

                if (insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25;
                }
            }

            // Speeding Tickets
            quote += insuree.SpeedingTickets * 10;

            // DUI
            if (insuree.DUI)
            {
                quote *= 1.25m;
            }

            // Full Coverage
            if (insuree.CoverageType)
            {
                quote *= 1.50m;
            }

            insuree.Quote = Math.Round(quote, 2);

            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insurees/Admin
        public ActionResult Admin()
        {
            var insurees = db.Insurees.ToList();
            return View(insurees);
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
    
