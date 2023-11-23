using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorApp
{
    class Contractor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public double HourlyWage { get; set; }

        public Contractor()
        {
            FirstName = "";
            LastName = "";
            StartDate = DateTime.Now;
            HourlyWage = 0;
        }
        public Contractor(string firstName, string lastName, DateTime startDate, double hourlyWage)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate;
            HourlyWage = hourlyWage;
        }
    }
}
