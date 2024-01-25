namespace UnitTest
{
    [TestClass]
    public class RecruitmentSystemTests
    {

        RecruitmentSystem system;
        [TestInitialize]
        public void Initialize()
        {
            system = new RecruitmentSystem();
        }

        [TestMethod]
        public void AddContractor_Contractor_Count_Increases()
        {
            int initialContractorCount = system.GetContractors().Count;


            system.AddContractor("Joe", "Bloggs", DateTime.Now, 30.0);


            int updatedContractorCount = system.GetContractors().Count;
            Assert.AreEqual(initialContractorCount + 1, updatedContractorCount);
        }

        [TestMethod]
        public void RemoveContractor_Contractor_Count_Decreases()
        {
            system.AddContractor("Joe", "Bloggs", DateTime.Now, 30.0);
            int initialContractorCount = system.GetContractors().Count;


            system.RemoveContractor(0);


            int updatedContractorCount = system.GetContractors().Count;
            Assert.AreEqual(initialContractorCount - 1, updatedContractorCount);
        }

        [TestMethod]
        public void RemoveContractor_Invalid_Index()
        {
            int initialContractorCount = system.GetContractors().Count;


            system.RemoveContractor(-1);


            int updatedContractorCount = system.GetContractors().Count;
            Assert.AreEqual(initialContractorCount, updatedContractorCount);
        }

        [TestMethod]
        public void AddJob_Job_Count_Increases()
        {
            int initialJobCount = system.GetJobs().Count;


            system.AddJob("New Job", DateTime.Now, 1000.0, false, "");


            int updatedJobCount = system.GetJobs().Count;
            Assert.AreEqual(initialJobCount + 1, updatedJobCount);
        }

        [TestMethod]
        public void AssignJob_Add_Contractor_To_Job()
        {
            system.AddContractor("Joe", "Bloggs", DateTime.Now, 35.40);
            system.AddJob("Tester", DateTime.Now, 650.50, false, "");


            string result = system.AssignJob(0, 0);


            Assert.AreEqual("Assignment successful", result);
            Assert.AreEqual("Joe Bloggs", system.GetJobs()[0].ContractorAssigned);
        }

        [TestMethod]
        public void AssignJob_Invalid_Index_Values()
        {
            int initialJobCount = system.GetJobs().Count;

           
            string result = system.AssignJob(-1, 0);


            Assert.AreEqual("Assignment failed: Invalid indices -1 0", result);
            Assert.AreEqual(initialJobCount, system.GetJobs().Count);
        }

        [TestMethod]
        public void CompleteJob_Job_Equals_Completed()
        {
            RecruitmentSystem system = new RecruitmentSystem();
            system.SetData(); 


            List<Job> jobs = system.GetJobs();
            int initialJobIndex = 0;


            system.CompleteJob(initialJobIndex);


            List<Job> updatedJobs = system.GetJobs();
            Job completedJob = updatedJobs[initialJobIndex];

            Assert.IsTrue(completedJob.Completed);
            Assert.IsTrue(string.IsNullOrEmpty(completedJob.ContractorAssigned));
        }


        [TestMethod]
        public void CompleteJob_Invalid_JobIndex_NoChange()
        {
            int initialJobCount = system.GetJobs().Count;


            system.CompleteJob(-1);


            int updatedJobCount = system.GetJobs().Count;
            Assert.AreEqual(initialJobCount, updatedJobCount);
        }

        [TestMethod]
        public void GetContractors_Return_List_Of_Contractors()
        {
            RecruitmentSystem system = new RecruitmentSystem();
            system.SetData(); 


            List<Contractor> contractors = system.GetContractors();


            Assert.IsNotNull(contractors);
            Assert.IsTrue(contractors.Count > 0);
        }

        [TestMethod]
        public void GetContractors_Return_Contractors()
        {
            system.AddContractor("Joe", "Bloggs", DateTime.Now, 30.0);
            system.AddContractor("John", "Robertson", DateTime.Now, 25.0);
            system.AddContractor("Rob", "Johnson", DateTime.Now, 35.0);


            List<Contractor> contractors = system.GetContractors();


            Assert.IsNotNull(contractors);
            Assert.AreEqual(3, contractors.Count);
            Assert.AreEqual("Joe", contractors[0].FirstName);
            Assert.AreEqual("John", contractors[1].FirstName);
            Assert.AreEqual("Rob", contractors[2].FirstName);
        }

        [TestMethod]
        public void GetJobs_Returns_List_Of_Jobs()
        {
            RecruitmentSystem system = new RecruitmentSystem();
            system.SetData();


            List<Job> jobs = system.GetJobs();


            Assert.IsNotNull(jobs);
            Assert.IsTrue(jobs.Count > 0);
        }

        [TestMethod]
        public void GetJobs_Return_Jobs()
        {
            system.AddJob("Test Job 1", DateTime.Now, 100.1, false, "Test Contractor1");
            system.AddJob("Test Job 2", DateTime.Now, 200.2, false, "");
            system.AddJob("Test Job 3", DateTime.Now, 300.3, false, "Test Contractor2");


            List<Job> jobs = system.GetJobs();

            
            Assert.IsNotNull(jobs);
            Assert.AreEqual(3, jobs.Count);
            Assert.AreEqual("Test Job 1", jobs[0].Title);
            Assert.AreEqual("Test Job 2", jobs[1].Title);
            Assert.AreEqual("Test Job 3", jobs[2].Title);
        }

        [TestMethod]
        public void GetAvailableContractors_Return_Unassigned_Contractor()
        {
            system.AddContractor("Joanne", "Blogitha", DateTime.Now, 35.40);
            system.AddContractor("Joe", "Bloggs", DateTime.Now, 35.40);
            system.AddJob("Tester", DateTime.Now, 650.50, false, "Joe Bloggs");


            var availableContractors = system.GetAvailableContractors();


            Assert.AreEqual(1, availableContractors.Count);
            Assert.AreEqual("Joanne Blogitha", $"{availableContractors[0].FirstName} {availableContractors[0].LastName}");
        }

        [TestMethod]
        public void GetAvailableContractors_No_Available_Contractors()
        {
            system.AddJob("Test Job", DateTime.Now, 100.11, false, "Contractor1");


            var availableContractors = system.GetAvailableContractors();


            Assert.AreEqual(0, availableContractors.Count);
        }

        [TestMethod]
        public void GetUnassignedJobs_Return_Jobs_Not_Assigned()
        {
            system.AddJob("Test Job 1", DateTime.Now, 100.1, false, "Test Contractor1");
            system.AddJob("Test Job 2", DateTime.Now, 200.2, false, "");
            system.AddJob("Test Job 3", DateTime.Now, 300.3, false, "Test Contractor2");


            List<Job> unnassignedJobs = system.GetUnassignedJobs();


            Assert.AreEqual(1, unnassignedJobs.Count);
            Assert.AreEqual("Test Job 2", unnassignedJobs[0].Title);
        }

        [TestMethod]
        public void GetUnassignedJobs_No_Unassigned_Jobs()
        {
            system.AddJob("Test Job", DateTime.Now, 100.0, false, "Contractor1");

            
            List<Job> unassignedJobs = system.GetUnassignedJobs();


            Assert.AreEqual(0, unassignedJobs.Count);
        }

        [TestMethod]
        public void GetJobByCost_Return_Jobs_In_Range()
        {
            system.AddJob("Test Job 1", DateTime.Now, 100.1, false, "");
            system.AddJob("Test Job 2", DateTime.Now, 200.2, false, "");
            system.AddJob("Test Job 3", DateTime.Now, 300.3, false, "");


            double lowCost = 120.1;
            double highCost = 240.3;
            List<Job> matchingJobs = system.GetJobByCost(lowCost, highCost);


            Assert.AreEqual(1, matchingJobs.Count);
        }

        [TestMethod]
        public void GetJobByCost_No_Jobs_Match()
        {
            system.AddJob("Test Job 1", DateTime.Now, 25, false, "");
            system.AddJob("Test Job 2", DateTime.Now, 70, false, "");


            double lowCost = 111.11;
            double highCost = 222.22;
            List<Job> matchingJobs = system.GetJobByCost(lowCost, highCost);


            Assert.AreEqual(0, matchingJobs.Count);
        }

    } 
}