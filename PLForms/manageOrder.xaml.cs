using System;
using System.Collections.Generic;
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

namespace PLForms
{
    /// <summary>
    /// Interaction logic for manageOrder.xaml
    /// </summary>
    public partial class manageOrder : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();

        public manageOrder()
        {

            InitializeComponent();
            orderDataGrid.ItemsSource = mybl.getListOfAllOrders();


        }

        /// <summary>
        /// ריענון הטבלה שמציגה את ההזמנות, שאיבה שוב ממאגר נתוני ההזמנות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void refreshTable(object sender = null, EventArgs e = null)
        {
            orderDataGrid.ItemsSource = null;
            orderDataGrid.ItemsSource = mybl.getListOfAllOrders();
        }



        /// <summary>
        /// מחיקת הזמנה, כוללת מחיקת כל ההזמנות שנבחרו ומחיקתכל המנותהמוזמנות עבור כל הזמנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteOrder_Click(object sender, RoutedEventArgs e)
        {

            var cellInfos = orderDataGrid.SelectedItems;
            int count = orderDataGrid.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                BE.Order d = (BE.Order)orderDataGrid.SelectedItems[i];
                try
                {
                    mybl.deleteOrder(d.orderId);//מחיקת ההזמנה

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);
                }
            }
            refreshTable();

        }





        private void clickOnTableForUpdate(object sender, MouseButtonEventArgs e)
        {
            BE.Order order  = new BE.Order();
            BE.OrderedDish orderDish= new BE.OrderedDish();
            order = (BE.Order)(orderDataGrid.SelectedItem);

            Window AddOrderWindows = new addOrderWindows(order.orderId);

            (AddOrderWindows as addOrderWindows).refresh += refreshTable;//

            
            AddOrderWindows.Show();

        }

        /// <summary>
        /// פונקציה לפתיחת חלון הוספת מנה,תופעל כאשר נלחץ על כפתור "פלוס"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addClientClick(object sender, RoutedEventArgs e)
        {
            Window AddOrderWindows = new addOrderWindows();
            
            (AddOrderWindows as addOrderWindows).refresh += refreshTable;//הרשמה לאירוע של חלון הוספת הזמנות


            AddOrderWindows.Show();
        }




    }
}


