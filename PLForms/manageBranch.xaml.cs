using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace PLForms
{


    public partial class manageBranch : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();


        public manageBranch()
        {

            InitializeComponent();
            branchDataGrid.ItemsSource = mybl.getListOfAllBranches();
        //    this.branchUpdateKosherLevel.ItemsSource = Enum.GetValues(typeof(BE.Kosher));

        }


        public void refreshTable(object sender = null, EventArgs e = null)
        {
            branchDataGrid.ItemsSource = null;
            branchDataGrid.ItemsSource = mybl.getListOfAllBranches();
        }

        private void openAddBranchPage(object sender, RoutedEventArgs e)
        {
            Window AddBranchWindows = new addBranchWindows();
            (AddBranchWindows as addBranchWindows).refresh += refreshTable;
            AddBranchWindows.Show();

        }


        private void deleteBranch_Click(object sender, RoutedEventArgs e)
        {

            var cellInfos = branchDataGrid.SelectedItems;
            int count = branchDataGrid.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                BE.Branch d = (BE.Branch)branchDataGrid.SelectedItems[i];
                try
                {
                    mybl.deleteBranch(d);

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);
                }
            }
            refreshTable();

        }

        /// <summary>
        /// יופעל בעת לחיצה על שורה בטבלה, יפעיל את העריכה של אותו אובייקט
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOnTableForUpdate(object sender, MouseButtonEventArgs e)
        {
            BE.Branch branch = new BE.Branch();
            branch = (BE.Branch)(branchDataGrid.SelectedItem);

            Window AddBranchWindows = new addBranchWindows (branch.branchId); // שליחת מספר המסעדה לחלון עריכת מסעדה

            (AddBranchWindows as addBranchWindows).refresh += refreshTable;//הרשמה לאירוע של חלון העריכה , יפעיל את ריענון הטבלה


            AddBranchWindows.Show();
        }




     
        
        /// <summary>
        /// פונקציה המגבילה קלט למספר בלבד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}