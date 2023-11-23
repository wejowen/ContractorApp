using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorApp
{
    class Job
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public bool Completed { get; set; }
        public string ContractorAssigned { get; set; }


        public Job()
        {
            Title = "";
            Date = DateTime.Now;
            Cost = 0;
            Completed = false;
            ContractorAssigned = "";
        }
        public Job(string title, DateTime date, double cost, bool completed, string contractorAssigned)
        {
            Title = title;
            Date = date;
            Cost = cost;
            Completed = completed;
            ContractorAssigned = contractorAssigned;
        }
    }
}
