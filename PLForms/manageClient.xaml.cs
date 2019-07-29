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
    /// <summary>
    /// Interaction logic for manageBranch.xaml
    /// </summary>
    /// 
   

    public partial class manageClient : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();


        public manageClient()
        {

            InitializeComponent();


            clientDataGrid.ItemsSource = mybl.getListOfAllClient();//הגדרת מקרות הטבלה להצגת רשימת כל הלקוחות
        }

        /// <summary>
        /// ריענון הטבלה המוצגת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void refreshTable(object sender = null, EventArgs e = null)
        {
            clientDataGrid.ItemsSource = null;
            clientDataGrid.ItemsSource = mybl.getListOfAllClient();
        }

        /// <summary>
        /// פתיחת עמוד הוספת לקוח
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openAddClientPage(object sender, RoutedEventArgs e)
        {
            Window AddClientWindows = new addClientWindows();
            (AddClientWindows as addClientWindows).refresh += refreshTable;//הרשמה לאירוע של ריענון הטבלה
            AddClientWindows.Show();

        }

        /// <summary>
        /// מחיקת כל הקליינטים שנבחרו
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteClient_Click(object sender, RoutedEventArgs e)
        {

            var cellInfos = clientDataGrid.SelectedItems;
            int count = clientDataGrid.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                BE.Client d = (BE.Client)clientDataGrid.SelectedItems[i];
                try
                {
                    mybl.deleteClient(d.idOfClient);

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);
                }
            }
            refreshTable();///ריענון הטבלה

        }








        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        
        }
        /// <summary>
        /// כאשר לוחצים על שורה בטבלה הפונקציה תופעל ויפתח חלון עריכה של הלקוח 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOnTableForUpdate(object sender, MouseButtonEventArgs e)
        {
            BE.Client client = new BE.Client();
            client = (BE.Client)(clientDataGrid.SelectedItem);

            Window AddClientWindows = new addClientWindows(client.idOfClient); // שליחת מספר ההלקוח לחלון עריכת מנה

            (AddClientWindows as addClientWindows).refresh += refreshTable;//הרשמה לאירוע של חלון העריכה , יפעיל את ריענון הטבלה


            AddClientWindows.Show();
        }
    }
}