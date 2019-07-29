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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLForms
{




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();

        public MainWindow()
        {
            
            InitializeComponent();
            try
            {
                mybl.addDish(new BE.Dish { dishLevelOfKosher = BE.Kosher.GLAT, dishName = "אנטריקות", dishSize = BE.SizeOfDish.MEDIUM, priceOfSingleDish = 10 });
                mybl.addDish(new BE.Dish { dishLevelOfKosher = BE.Kosher.GLAT, dishName = "כבש", dishSize = BE.SizeOfDish.MEDIUM, priceOfSingleDish = 20 });
                mybl.addDish(new BE.Dish { dishLevelOfKosher = BE.Kosher.GLAT, dishName = "סלמי", dishSize = BE.SizeOfDish.MEDIUM, priceOfSingleDish = 30 });
                mybl.addDish(new BE.Dish { dishLevelOfKosher = BE.Kosher.GLAT, dishName = "חביתה", dishSize = BE.SizeOfDish.MEDIUM, priceOfSingleDish = 40 });

                mybl.addBranch(new BE.Branch { adressOfBranch = "כתובת המסעדה 1",  nameOfBranch = "קפה גרג", nameOfResponsible = "ישראל", numberOfFreeCouriers = 2, numberOfWorkers = 33, phoneNumberOfBranch = "0544587412" });
                mybl.addBranch(new BE.Branch { adressOfBranch = "כתובת המסעדה 2",  nameOfBranch = "קפה GO", nameOfResponsible = "ג'ורדן", numberOfFreeCouriers = 1, numberOfWorkers = 33, phoneNumberOfBranch = "0557755" });

                mybl.addClient(new BE.Client { age = 20, addressOfClient = "address1", LocationOfClient = "רוסיה", nameOfClient = "נריה", numberOfCreditCard = 222 });
                mybl.addClient(new BE.Client { age = 20, addressOfClient = "address2", LocationOfClient = "זנגוויל", nameOfClient = "אריה", numberOfCreditCard = 333 });
                mybl.addClient(new BE.Client { age = 20, addressOfClient = "address3", LocationOfClient = "מכון לב", nameOfClient = "דשט", numberOfCreditCard = 111 });


                mybl.addOrder(new BE.Order { branchId = 1, clientID = 1, CourierNeeded = false, price = 170 });
                mybl.addOrder (new BE.Order { branchId=2 , clientID= 2 , CourierNeeded=false , price= 30 });
                mybl.addOrder(new BE.Order { branchId = 1, clientID = 3, CourierNeeded = false, price = 220 });


                mybl.addOrderedDish(new BE.OrderedDish { dishID = 1, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.LARGE, numberOfSameDish = 4, orderId = 1 });
                mybl.addOrderedDish(new BE.OrderedDish { dishID = 2, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.LARGE, numberOfSameDish = 2, orderId = 1 });
                mybl.addOrderedDish(new BE.OrderedDish { dishID = 3, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.LARGE, numberOfSameDish = 3, orderId = 1 });

                mybl.addOrderedDish(new BE.OrderedDish { dishID = 2, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.MEDIUM, numberOfSameDish = 1, orderId = 2 });
                mybl.addOrderedDish(new BE.OrderedDish { dishID = 1, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.MEDIUM, numberOfSameDish = 1, orderId = 2 });

                mybl.addOrderedDish(new BE.OrderedDish { dishID = 4, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.MEDIUM, numberOfSameDish = 4, orderId = 3 });
                mybl.addOrderedDish(new BE.OrderedDish { dishID = 3, dishLevelOfKosher = BE.Kosher.GLAT, dishSize = BE.SizeOfDish.MEDIUM, numberOfSameDish = 2, orderId = 3 });
            }
            catch (Exception s)
            {

                ///MessageBox.Show(s.Message);
            }
            


        }

        private void manageDish_click(object sender, RoutedEventArgs e)
        {
            Window manageDishWindows= new manageDish();
            manageDishWindows.Show();

        }

        private void manageBranch_click(object sender, RoutedEventArgs e)
        {
            Window manageBranchWindows = new manageBranch();
            manageBranchWindows.Show();
        }

        private void manageClient_click(object sender, RoutedEventArgs e)
        {
            Window manageClientWindows = new manageClient();
            manageClientWindows.Show();
        }

        private void manageOrder_click(object sender, RoutedEventArgs e)
        {
            Window manageOrderWindows = new manageOrder();
            manageOrderWindows.Show();
        }

        private void moreDitails_Click(object sender, RoutedEventArgs e)
        {
            Window moreDetailsWindows = new moreDetails();
            try
            {
                moreDetailsWindows.Show();
            }
            catch (Exception)
            {
                
            
            }
           
        }
    }
}
