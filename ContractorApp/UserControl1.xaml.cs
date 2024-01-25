using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
    //This was a mistake of a design choice for me. Took me longer than it should have to implement this and get it fully working. That said, I learnt a lot. 
    public partial class UserControl1 : UserControl
    {

        Job job = new Job();
        Contractor contractor = new Contractor();
        private RecruitmentSystem recruitmentSystem;
        public System.DateTimeOffset Date { get; set; }
        public int selectedJobIndex { get; set; }

        internal UserControl1(RecruitmentSystem mainRecruitmentSystem)
        {
            InitializeComponent();
            recruitmentSystem = mainRecruitmentSystem;
        }

        //Checks available contractors, returns the list 
        internal void UpdateAvailableContractorsList(List<Contractor> availableContractors)
        {
            List<string> contractorsToHire = new List<string>();

            foreach (var contractor in availableContractors)
            {
                //Converting DateTime to string of date only in specified format
                string dateFormat = contractor.StartDate.ToString("dd/MM/yyyy");
                contractorsToHire.Add($"Name: {contractor.FirstName} {contractor.LastName}, Start Date: {dateFormat}, Hourly Wage: {contractor.HourlyWage}");
            }

            List_Box_AvailableContractors.ItemsSource = contractorsToHire;
        }
        //Assigns job based on list index 
        private void Button_AssignContractor(object sender, RoutedEventArgs e)
        {

            int selectedContractorIndex = List_Box_AvailableContractors.SelectedIndex;

            if (selectedContractorIndex >= 0 && selectedJobIndex >= 0)
            {
                recruitmentSystem.AssignJob(selectedJobIndex, selectedContractorIndex);
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show($"Please select a contractor to assign", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
    
}
