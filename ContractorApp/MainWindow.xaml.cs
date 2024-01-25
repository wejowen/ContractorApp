using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContractorApp
{

    public partial class MainWindow : Window
    {
        private RecruitmentSystem recruitmentSystem;
        Job job = new Job();
        Contractor contractor = new Contractor();
        public System.DateTimeOffset Date { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();

            recruitmentSystem = new RecruitmentSystem();

            recruitmentSystem.SetData();

            RefreshJobList();

            RefreshContractorList();


        }

        private void RefreshContractorList()
        {
            List<Contractor> retrievedContractors = recruitmentSystem.GetContractors();
            List<string> contractorInfoList = new List<string>();

            foreach (var contractor in retrievedContractors)
            {
                //Converting DateTime to string of date only in specified format
                string dateFormat = contractor.StartDate.ToString("dd/MM/yyyy");
                contractorInfoList.Add($"Name: {contractor.FirstName} {contractor.LastName}, Start Date: {dateFormat}, Hourly Wage: {contractor.HourlyWage}");
            }

            // Displaying Data in the list box
            List_Box_Contractor.ItemsSource = contractorInfoList;
        }

        public void RefreshJobList()
        {
            List<Job> retrievedJobs = recruitmentSystem.GetJobs();
            List<string> jobInfoList = new List<string>();
            
            foreach (var job in retrievedJobs)
            {
                string dateFormat = job.Date.ToString("dd/MM/yyyy");
                jobInfoList.Add($"Job Title: {job.Title}, Date: {dateFormat}, Cost: {job.Cost}, Completed: {job.Completed}, Contractor Assigned: {job.ContractorAssigned}");
            }

            List_Box_Jobs.ItemsSource = jobInfoList;
        }

        private void Button_Contractor(object sender, RoutedEventArgs e)
        {
            string firstName = Text_First_Name.Text;
            string lastName = Text_Last_Name.Text;
            DateTime startDate = Text_Start_Date.SelectedDate ?? DateTime.Now; //Date is today if unselected/when opened
            double hourlyWage;

            // parsing the hourly wage
            if (double.TryParse(Text_Hourly_Wage.Text, out hourlyWage))
            {
                recruitmentSystem.AddContractor(firstName, lastName, startDate, hourlyWage);
            }
            else
            {
                MessageBox.Show("Please enter a valid number. Numbers ending in two decimal points only, such as: '12.99'", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RefreshContractorList();
        }
        private void Button_RemoveContractor(object sender, RoutedEventArgs e)
        {
            int selectedContractorIndex = List_Box_Contractor.SelectedIndex;

            if (selectedContractorIndex >= 0)
            {
                recruitmentSystem.RemoveContractor(selectedContractorIndex);

            }
            else
            {
                MessageBox.Show("Please select a contractor to remove.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RefreshContractorList();

        }

        private void Button_Jobs(object sender, RoutedEventArgs e)
        {
            string title = Text_Title.Text;
            DateTime date = Text_Date.SelectedDate ?? DateTime.Now; //Date is today if unselected/when opened
            double cost;
            bool completed = false;
            string contractorAssigned = "Unnasigned";

            // Try parsing the hourly wage
            if (double.TryParse(Text_Cost.Text, out cost))
            {
                // Calling AddJob method to add 
                recruitmentSystem.AddJob(title, date, cost, completed, contractorAssigned);
            }
            else
            {
                MessageBox.Show("Please enter a valid number. Numbers ending in two decimal points only, such as: '12.99'", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RefreshJobList();

        }
      
        public void Button_AssignJob(object sender, RoutedEventArgs e)
        {

            UserControl1 myUserControl = new UserControl1(recruitmentSystem);
            Window userControlWindow = new Window
            {
                Content = myUserControl,
                Title = "Choose a Contractor",
                Width = 600,
                Height = 600,
            };

            List<Contractor> availableContractors = recruitmentSystem.GetAvailableContractors();

            myUserControl.UpdateAvailableContractorsList(availableContractors);


            myUserControl.selectedJobIndex = List_Box_Jobs.SelectedIndex;

            if (myUserControl.selectedJobIndex >= 0)
            {
                userControlWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a job to assign", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RefreshJobList();  
        }

        //Completes job and wipes job assigned data, making contractor available again
        public void Button_CompleteJob(object sender, RoutedEventArgs e)
        {
            int selectedJobIndex = List_Box_Jobs.SelectedIndex;
            if (selectedJobIndex >= 0)
            {
                recruitmentSystem.CompleteJob(selectedJobIndex);
            }
            else
            {
                MessageBox.Show("Please select a job to complete", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RefreshJobList();
        }

        public void Button_AllJobs(object sender, RoutedEventArgs e)
        {
            RefreshJobList();
        }

        public void Button_UnassignedJobs(object sender, RoutedEventArgs e)
        {
            List<Job> retrievedJobs = recruitmentSystem.GetUnassignedJobs();
            List<string> jobInfoList = new List<string>();

            foreach (var job in retrievedJobs)
            {
                string dateFormat = job.Date.ToString("dd/MM/yyyy");
                jobInfoList.Add($"Job Title: {job.Title}, Date: {dateFormat}, Cost: {job.Cost}, Completed: {job.Completed}, Contractor Assigned: {job.ContractorAssigned}");
            }

            List_Box_Jobs.ItemsSource = jobInfoList;
        }

        public void Button_JobByCost(object sender, RoutedEventArgs e)
        {
            double lowCost;
            double highCost;
            List<string> jobInfoList = new List<string>();
            if (double.TryParse(Text_Low.Text, out lowCost) && double.TryParse(Text_High.Text, out highCost))
            {
                if (highCost <= lowCost || lowCost >= highCost)
                {
                    MessageBox.Show("Please ensure the first value is lower than the second value", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    List<Job> retrievedJobs = recruitmentSystem.GetJobByCost(lowCost, highCost);

                    foreach (var job in retrievedJobs)
                    {
                        string dateFormat = job.Date.ToString("dd/MM/yyyy");
                        jobInfoList.Add($"Job Title: {job.Title}, Date: {dateFormat}, Cost: {job.Cost}, Completed: {job.Completed}, Contractor Assigned: {job.ContractorAssigned}");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number. Numbers ending in two decimal points only, such as: '12.99'", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            List_Box_Jobs.ItemsSource = jobInfoList;
        }
    }
}
