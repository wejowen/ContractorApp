using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContractorApp
{
    public class RecruitmentSystem
    {
        //Lists
        List<Job> jobs = new List<Job>();
        List<Contractor> contractors = new List<Contractor>();

        public RecruitmentSystem()
        {
            contractors = new List<Contractor>();
            jobs = new List<Job>();
        }

        //Test data
        public void SetData()
        {
            contractors.Add(new Contractor("Jeremy", "Backster", DateTime.Parse("2023-01-01"), 20.00));
            contractors.Add(new Contractor("Holt", "Inthaname", DateTime.Parse("2023-06-15"), 25.33));
            contractors.Add(new Contractor("Baroque", "Worker", DateTime.Parse("2023-10-23"), 35.50));
            jobs.Add(new Job("Extermination", DateTime.Parse("2023-10-21"), 6000.30, false, ""));
            jobs.Add(new Job("Construction", DateTime.Parse("2023-12-31"), 4000.26, false, ""));
            jobs.Add(new Job("Cleaning", DateTime.Parse("2023-09-08"), 2000.41, false, ""));
        }

        public void AddContractor(string firstName, string lastName, DateTime startDate, double hourlyWage)
        {
            Contractor newContractor = new Contractor(firstName, lastName, startDate, hourlyWage);
            contractors.Add(newContractor);
        }
        //Removes based on list position
        public void RemoveContractor(int contractorIndex)
        {
            if (contractorIndex >= 0 && contractorIndex < contractors.Count)
            {
                contractors.RemoveAt(contractorIndex);
            }
        }
        //Adds job 
        public void AddJob(string title, DateTime date, double cost, bool completed, string contractorAssigned)
        {
            Job newJob = new Job(title, date, cost, completed, contractorAssigned);
            jobs.Add(newJob);
        }
        //Assigns job based on two list positions
        public string AssignJob(int jobIndex, int contractorIndex)
        {
            if (jobIndex >= 0 && jobIndex < jobs.Count && contractorIndex >= 0 && contractorIndex < contractors.Count)
            {
                // Assigning contractors name to the job
                jobs[jobIndex].ContractorAssigned = $"{contractors[contractorIndex].FirstName} {contractors[contractorIndex].LastName}";
                return "Assignment successful";
            }
            else
            {
                return $"Assignment failed: Invalid indices {jobIndex} {contractorIndex}";
            }
        }
        //Setting job completion to True
        public void CompleteJob(int jobIndex)
        {
            if (jobIndex >= 0 && jobIndex < jobs.Count)
            {
                jobs[jobIndex].Completed = true;
                jobs[jobIndex].ContractorAssigned = "";
            }
        }
        //Returning contractors
        public List<Contractor> GetContractors()
        {
            return contractors;
        }

        // Simple return of the list of all jobs
        public List<Job> GetJobs()
        {
            
            return jobs;
        }

        //Returns only available contractors (by checking job assigned names). Used on the assign job button
        public List<Contractor> GetAvailableContractors()
        {
            List<Contractor> availableContractors = new List<Contractor>();

            foreach (var contractor in contractors)
            {
                bool isAssigned = false;
                foreach (Job job in jobs)
                {
                    if (job.ContractorAssigned == $"{contractor.FirstName} {contractor.LastName}")
                    {
                        isAssigned = true;
                        break;
                    }
                }

                if (!isAssigned)
                {
                    availableContractors.Add(contractor);
                }
            }

            return availableContractors;
        }
        //Returns uncompleted and unassigned jobs, by checking whether or contractor assigned has any value, and is completed = false
        public List<Job> GetUnassignedJobs()
        {
            List<Job> jobsWithoutContractors = new List<Job>();

            foreach (Job job in jobs)
            {
                if (string.IsNullOrEmpty(job.ContractorAssigned) && job.Completed == false)
                {
                    jobsWithoutContractors.Add(job);
                }
            }

            return jobsWithoutContractors;
        }
        //Checks for job costs between or equal to lowCost and highCost, returns the list 
        public List<Job> GetJobByCost(double lowCost, double highCost)
        {
            List<Job> matchingJobs = new List<Job>();

            foreach (Job job in jobs)
            {
                if (job.Cost >= lowCost && job.Cost <= highCost)
                {
                    matchingJobs.Add(job);
                }
            }

            return matchingJobs;
        }

    }
}
