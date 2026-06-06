using System.Data.Entity;

namespace CarInsurance.Models
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext()
            : base("CarInsuranceDB")
        {
        }

        public DbSet<Insuree> Insurees { get; set; }
    }
}
