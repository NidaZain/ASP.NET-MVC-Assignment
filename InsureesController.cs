[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include =
"Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,CoverageType")]
Insuree insuree)
{
    if (ModelState.IsValid)
    {
        decimal quote = 50m;

        // AGE
        int age = DateTime.Today.Year - insuree.DateOfBirth.Year;

        if (insuree.DateOfBirth.Date > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        if (age <= 18)
        {
            quote += 100m;
        }
        else if (age >= 19 && age <= 25)
        {
            quote += 50m;
        }
        else
        {
            quote += 25m;
        }

        // CAR YEAR
        if (insuree.CarYear < 2000)
        {
            quote += 25m;
        }

        if (insuree.CarYear > 2015)
        {
            quote += 25m;
        }

        // PORSCHE
        if (insuree.CarMake.ToLower() == "porsche")
        {
            quote += 25m;

            if (insuree.CarModel.ToLower() == "911 carrera")
            {
                quote += 25m;
            }
        }

        // SPEEDING TICKETS
        quote += insuree.SpeedingTickets * 10m;

        // DUI
        if (insuree.DUI)
        {
            quote *= 1.25m;
        }

        // FULL COVERAGE
        if (insuree.CoverageType)
        {
            quote *= 1.50m;
        }

        insuree.Quote = quote;

        db.Insurees.Add(insuree);
        db.SaveChanges();

        return RedirectToAction("Index");
    }

    return View(insuree);
}
