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
    /// Interaction logic for manageDish.xaml
    /// </summary>
    /// 

    public partial class manageDish : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();


        public manageDish()
        {
            
            InitializeComponent();
            dishDataGrid.ItemsSource = mybl.getListOfAllDishes();//הגדרת מקורות הטבלה לרשימת כל המנות


        }

        /// <summary>
        /// ריענון הטבלה המוצגת בעמוד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void refreshTable(object sender=null, EventArgs e=null)
        {
            dishDataGrid.ItemsSource = null;
            dishDataGrid.ItemsSource = mybl.getListOfAllDishes();
        }

        /// <summary>
        /// יפתח את חלון הוספת מנה חדשה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openAddDishPage(object sender, RoutedEventArgs e)
        {
            Window AddDishWindows = new addDishWindows();
            (AddDishWindows as addDishWindows).refresh+=refreshTable;
            AddDishWindows.Show();

        }

        /// <summary>
        /// מחיקת המנות שנבחרו
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DishDelete_Click(object sender, RoutedEventArgs e)
        {

            var cellInfos = dishDataGrid.SelectedItems;
            int count = dishDataGrid.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                BE.Dish d = (BE.Dish)dishDataGrid.SelectedItems[i];
               try 
	           {	        
		                mybl.deleteDish(d);
	           }
	            catch (Exception error)
	            {

                    MessageBox.Show(error.Message);

	            }
                
              }
            refreshTable();//ריענון הטבלה

        }



        /// <summary>
        /// כאשר ילחצו על שורה בטבלה הפונקציה תפתח את חלון עריכת מנות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DishUpdate_Click(object sender, MouseButtonEventArgs e)
        {

            BE.Dish dish = new BE.Dish();
            dish = (BE.Dish)(dishDataGrid.SelectedItem);

            Window AddDishWindows = new addDishWindows(dish.dishID); // שליחת מספר המנה לחלון עריכת מנה

            (AddDishWindows as addDishWindows).refresh += refreshTable;//


            AddDishWindows.Show();

        }
      
        /// <summary>
        /// פונקציה להגבלת קלט של מספרים בלבד
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


