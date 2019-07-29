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

    public partial class moreDetails : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();

        public moreDetails()
        {
            InitializeComponent();
            try
            {
                this.mostPopularBranch.Text = mybl.mostPopularBranch();
                this.mostPopularDish.Text = mybl.mostPopularDish();
                this.mostPopularClient.Text = mybl.mostPopularClient();
                this.sumProfit.Text = mybl.sumProfit().ToString();
                this.numberOfBranches.Text = mybl.numberOfBranches().ToString();
                this.numberOfClients.Text = mybl.numberOfClients().ToString();
                this.numberOfDishes.Text = mybl.numberOfDishes().ToString();
                this.mostExpensiveDish.Text = mybl.mostExpensiveDish();
            }
            catch (Exception )
            {

                MessageBox.Show("לא ניתן לצפות בחלון הסטטיסטיקות אם אין לפחות הזמנה ,מנה,מסעדה ולקוח אחד");
                this.Close();
            }


           
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            profitDataGrid.Visibility = (Visibility.Visible);
            switch (int.Parse((showProfitBy.SelectedIndex.ToString())))
            {
                case 0:
                    profitDataGrid.ItemsSource= mybl.getProfitByCustomerAdress();
                    orderby.Header = "כתובת";
                    resualt.Header = "רווח";

                    break;
                case 1:
                   profitDataGrid.ItemsSource= mybl.getProfitByDate();
                    orderby.Header = "כתובת";
                    resualt.Header = "רווח";

                    break;

                case 2:
                    profitDataGrid.ItemsSource= mybl.getProfitByKindOfDish();
                    orderby.Header = "שם מנה";
                    resualt.Header = "רווח";
                    break;

                 case 3:
                    Predicate<BE.Order> cond = order => order.price > 50;
                    profitDataGrid.ItemsSource = mybl.getOrderByCond(cond);
                    orderby.Header = "שם מזמין";
                    resualt.Header = "מחיר";
                    break;

            }
        }
    }
}
