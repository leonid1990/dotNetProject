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
    /// Interaction logic for addOrderWindows.xaml
    /// </summary>
    public partial class addOrderWindows : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();
        public event EventHandler refresh;
        internal List<BE.OrderedDish> tempList; //lit of temp order dishes
        public string addOrEdit;
        uint updateOrderID;
        #region constructor for add page & constructor for edit page
        /// <summary>
        /// בנאי העמוד "הוספת הזמנה" מגדיר מקורות לבחירת המנות,לקוחות והכשרויות
        /// </summary>
        public addOrderWindows()
        {
            InitializeComponent();

            tempList = new List<BE.OrderedDish>();
            this.tempList = new List<BE.OrderedDish>();

            this.fill_Combobox_client_branch_dish_kosher_size();//מילוי בחירת הלקוחות
            fillAddPageValue();//יכניס את כותרת העמוד+כפתור שליחת הטופס המתאימים לעמוד הוספת מנה
            addOrEdit = "add";


        }



        /// <summary>
        /// בנאי לעמוד עריכת מנה, 
        /// </summary>
        /// <param name="UpdateorderId">מספר זיהוי ההזמנה אותו נרצה לערוך</param>
        public addOrderWindows(uint UpdateorderId )
        {
            InitializeComponent();
            this.updateOrderID = UpdateorderId;//קבלת מספר ההזמנה שרוצים לעדכן


            this.tempList = new List<BE.OrderedDish>();


            fill_Combobox_client_branch_dish_kosher_size();
            fillEditPageValue(UpdateorderId);

            addOrEdit = "edit";//ככה נזכור שאנו בעמו עריכה
            refresh_addedDishesTable();
        }

       /// <summary>
       /// הפונקציה תופעל ע"י הבנאי שמופעל כאשר רוצים לערוך הזמנה
       /// הפונקציה תעדכן את רשימת המנות המוזמנות הזמנית+שם לקוח+שם מסעדה+ כותרת העמוד +כותרת כפתור השליחה
       /// </summary>
       /// <param name="UpdateorderId">מספר זיהוי ההזמנה אותה נעדן</param>
        private void fillEditPageValue(uint UpdateorderId)
        {
            BE.Order updateOrder = new BE.Order();
            updateOrder = mybl.getOrderById(updateOrderID);

            tempList = mybl.getListOfOrderDishByOrderId(updateOrderID);//רשימת המנות המוזמנות הזמנית
            listOfBranchCombobox.SelectedItem = mybl.getBranchByBranchId(updateOrder.branchId).nameOfBranch;
            listOfClientCombobox.SelectedItem = mybl.getClientByClientId(updateOrder.clientID).nameOfClient;
            totalPrice.Text = updateOrder.price.ToString();
            titleOfPage.Text = "עריכת הזמנה";//עדכון כותרת העמוד
            sendFormBottun.Content = "ערוך הזמנה";
        }

        /// <summary>
        /// יכניס את כותרת העמוד+כפתור שליחת הטופס המתאימים לעמוד הוספת מנה
        /// </summary>
        private void fillAddPageValue()
        {
            this.titleOfPage.Text = "הוספת הזמנה";
            this.sendFormBottun.Content = "הוסף הזמנה";
            this.totalPrice.Text = "0";
        }

        /// <summary>
        /// הפונקציה תמלא את השדות של המנות,מסעדות ולקוחות לפי הנתונים הנמצאים במסד
        /// </summary>
        private void fill_Combobox_client_branch_dish_kosher_size()
        {

            foreach (BE.Client client in mybl.getListOfAllClient())
            {
                listOfClientCombobox.Items.Add(client.nameOfClient);
            }

            foreach (BE.Branch branch in mybl.getListOfAllBranches())
            {
                listOfBranchCombobox.Items.Add(branch.nameOfBranch);
            }

            foreach (BE.Dish dish in mybl.getListOfAllDishes())
            {
                listDishCombobox.Items.Add(dish.dishName);
            }

            this.listKosherCombobox.ItemsSource = Enum.GetValues(typeof(BE.Kosher));
            this.listSizeCombobox.ItemsSource = Enum.GetValues(typeof(BE.SizeOfDish));

        }

        #endregion



        /// <summary>
        /// הפונקציה תוסיף אובייקט "מנסה מוזמנת" לרשימה זמנית של כל המנות המוזמנות, ותציג אותה בטבלה המתאימה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDishToOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.OrderedDish tempOrderDish = new BE.OrderedDish();//object to build the new orderDish accoring to his inseret
                BE.Dish tempDish = mybl.getDishByName(listDishCombobox.SelectedItem.ToString());//get the object dish according to name


                buildOrderDish(tempOrderDish, tempDish.dishID); // build the temp order dish according to the form
                mybl.checkKosherSizeLevel(tempOrderDish, tempDish.dishSize, tempDish.dishLevelOfKosher);//check kosher& size for the new dish 
                double addedPriceUpdate =  mybl.calculatePrice(mybl.getDishById(tempDish.dishID).priceOfSingleDish, tempOrderDish.numberOfSameDish);//חישוב מחיר המנה שהתווסף
                mybl.maxOrderPayment(double.Parse(totalPrice.Text) + addedPriceUpdate);//בדיקה האם המחיר שהיה +החדש גבוה מידי
                updatePriceTextBox(addedPriceUpdate); // update the price on the correct text
                addOrderDishForList(tempOrderDish);//add the order dish to the temp list , to show on the table
                refresh_addedDishesTable(); // refresh the table that show all the dish
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }


        }
        /// <summary>
        /// מוסיפה את המנה הדרושה לרשימה זמנית במידה והכשרות+גדול נכונים, הרשימה הזמנית תוצג בטופס
        /// תת פונקציה של addDishToOrderClick
        /// </summary>
        /// <param name="tempOrderDish">המנה שברצוננו להוסיף</param>
        private void addOrderDishForList(BE.OrderedDish tempOrderDish)
        {
            
            try
            {
                Predicate<BE.OrderedDish> ifOrderDishExist = (localOrderDish) => (
                                         localOrderDish.dishID == tempOrderDish.dishID &&
                                         localOrderDish.dishSize == tempOrderDish.dishSize &&
                                         localOrderDish.dishLevelOfKosher == tempOrderDish.dishLevelOfKosher &&
                                         localOrderDish.numberOfSameDish == tempOrderDish.numberOfSameDish);

                if (tempList.Exists(ifOrderDishExist))
                    throw new Exception("המנה עם אותה כשרות,גודל וכמות כבר קיימת");
                else
                    tempList.Add(tempOrderDish);
                
                refresh_addedDishesTable();

                
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        ///  בניית האובייקט "מנה מוזמנת" על פי הנתונים שהמשתמש הזין
        /// </summary>
        /// <param name="tempOrderDish">האובייקט מנה מוזמנת אותו נעדכן</param>
        /// <param name="dishId">מספר המנה </param>
        private void buildOrderDish(BE.OrderedDish tempOrderDish , uint dishId)
        {
           
            try
            {
                tempOrderDish.dishID = dishId;
                tempOrderDish.dishLevelOfKosher = (BE.Kosher)listKosherCombobox.SelectedItem;
                tempOrderDish.numberOfSameDish = uint.Parse(numberSameDishNumUpDown.textNumber.Text);

                tempOrderDish.dishSize = (BE.SizeOfDish)listSizeCombobox.SelectedItem;

                tempOrderDish.dishID = mybl.getDishByName(listDishCombobox.SelectedItem.ToString()).dishID;
              
            }
            catch (Exception)
            {

                throw new Exception("אנא מלא את כל השדות");
            }

        }

        /// <summary>
        /// הפונקציה תעדכן את שדה המחיר , תופעל גם בהוספת מנות וגם בהסרת מנות
        /// </summary>
        /// <param name="newPrice">תקבל את המחיר שצריך להחסיר או להוסיף</param>
        /// <param name="minusOrPluse">אם נרצה להחסיר מחיר נשלח -1 אחרת לא נשלח כלום </param>
        
        private void updatePriceTextBox(double newPrice, int minusOrPluse=1)
        {
            
            this.totalPrice.Text = (double.Parse(totalPrice.Text) + minusOrPluse*newPrice).ToString();

        }
        /// <summary>
        /// עדכון הטבלה המציגה את רשימת המנות המוזמנות
        /// </summary>
        private void refresh_addedDishesTable()
        {
            addedDishes.ItemsSource = null;
            addedDishes.ItemsSource = tempList;


            listSizeCombobox.SelectedItem = null;
            listKosherCombobox.SelectedItem = null;
            listDishCombobox.SelectedItem = null;
            numberSameDishNumUpDown.textNumber.Text="";
        }

        /// <summary>
        /// בעת לחיצה על הוספת הזמנה, הפונקציה תבדוק שהכל נכון ותוסיף את ההזמנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addOrderEvent(object sender, RoutedEventArgs e)
        {
            uint orderId = updateOrderID;//אם העמוד עריכה,מספר ההזמנה יהיה לפי המספר שהתקבל, במידה וזה עמודה עריכה יתעדכן בהמשך
            BE.Order newOrder = new BE.Order();
            buildOrder(newOrder);//בניית האובייקט הזמנה על פי השדות שהמשתמש הזין

            if(addOrEdit=="add")//אם העמוד הוא עמוד הוספת הזמנה
                orderId = mybl.addOrder(newOrder);//הוספת ההזמנה,וקבלת מספר הזמנה לצורך הוספת המנות המוזמנות

            else // אם אנחנו בעמוד עריכה, עדכן את ההזמנה ומחק את המנות הישנות
            {

                newOrder.orderId = orderId;//שמירת מספר ההזמנה באובייקט ההזמנה
                mybl.updateOrder(newOrder); //עדכון ההזמנה במקור הנתונים לפי המספר זיהוי
                mybl.deleteAllOrderedDishesByOrderId(updateOrderID);//מחיקת כל המנות המוזמנות(הישנות) של ההזמנה לפי מספר זיהוי ההזמנה 
                this.Close();

            }

            foreach (BE.OrderedDish item in tempList) //הוספת כל המנות מהרשימה למקור הנתונים
            {
                item.orderId = orderId;//עדכון מספר ההזמנה בכל אובייקט
                mybl.addOrderedDish(item);
            }

            tempList.Clear(); //איפוס רשימת המנות המוזמנות
            refresh_addedDishesTable();
            //איפוס הלקוח והמסעדה
                listOfClientCombobox.SelectedItem = null;
                listOfBranchCombobox.SelectedItem = null;
            
            refresh(this, EventArgs.Empty); //הפעלת האירוע שנרשם אליו חלון ניול הזמנות, 
        }
        /// <summary>
        /// הפונקציה תבנה את האובייקט "הזמנה" לפי הנתונים שהמשתמש הזין בטופס
        /// </summary>
        /// <param name="newOrder">האובייקט "הזמנה" אותו נעדכן לפי הטופס</param>
        private void buildOrder( BE.Order newOrder)
        {
            newOrder.clientID = (mybl.getClientIdByName(listOfClientCombobox.SelectedItem.ToString())).idOfClient;
            newOrder.dateOfOrder = DateTime.Now;
            newOrder.price = int.Parse(totalPrice.Text);
            newOrder.branchId = (mybl.getBranchByName(listOfBranchCombobox.SelectedItem.ToString())).branchId;
            newOrder.CourierNeeded = false;//האם צריך משלוח-לתקן!
            
        }
        /// <summary>
        /// בעת לחיצה על מחיקה, הפונקציה תמחק את כל המנות שהמשתמש בחר
        /// בנוסף,תדאג לעדכן את שדה המחיר
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderDishDelete_Click(object sender, RoutedEventArgs e)
        {
            var cellInfos = addedDishes.SelectedItems;
            int count = addedDishes.SelectedItems.Count;

            for (int i = 0; i < count; i++)
            {
                BE.OrderedDish d = (BE.OrderedDish)addedDishes.SelectedItems[i];
                tempList.RemoveAll(orderDish =>
                                    orderDish.dishID == d.dishID &&
                                    orderDish.dishSize == d.dishSize &&
                                    orderDish.dishLevelOfKosher == d.dishLevelOfKosher &&
                                    orderDish.numberOfSameDish==d.numberOfSameDish);
                updatePriceTextBox(mybl.calculatePrice(mybl.getDishById(d.dishID).priceOfSingleDish, d.numberOfSameDish), -1);//שליחה לפונקציה שתעדכן את המחיר בשדה המחיר
            }
            refresh_addedDishesTable();//עדכון טבלת המנות המוזמנות הזמנית
        }

        #region tooltip
        private void dishToolTip(object sender, ToolTipEventArgs e)
        {
            ComboBox b = sender as ComboBox;
            b.ToolTip = "בחר מנה";
            
        }

        private void sizeToolTip(object sender, ToolTipEventArgs e)
        {
            ComboBox b = sender as ComboBox;
            b.ToolTip = "בחר גודל מנה";
        }

        private void kosherToolTip(object sender, ToolTipEventArgs e)
        {
            ComboBox b = sender as ComboBox;
            b.ToolTip = "בחר כשרות מנה";
        }



        #endregion




    }
}
