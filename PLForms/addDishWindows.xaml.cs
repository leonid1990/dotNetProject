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
using System.Globalization;
using System.Text.RegularExpressions;

namespace PLForms
{




    /// <summary>
    /// Interaction logic for addDishWindows.xaml
    /// </summary>
    public partial class addDishWindows : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();
        public event EventHandler refresh;
        public uint updateDishId=0; //אם ישאר 0 אנחנו בעמוד עריכה, אם ישתנה אנחנו בעמוד הוספה
        BE.Dish newDish;

        #region constructor of add / edit page + sub method

        #region constructors
        /// <summary>
        /// בנאי שיופעל בעמוד הוספת מנה
        /// </summary>
        public addDishWindows()
        {
            InitializeComponent();
            fillSizeKosherCombobox();//עידכון שדה הבחירה של גודל וכשרויות
            newDish= new BE.Dish ();
            orderGrid.DataContext = newDish;//חיבור טופס המנה לאובייקט מנה
            
        }
        /// <summary>
        /// בנאי שיופעל בעמוד עריכה
        /// </summary>
        /// <param name="dishId">מספר המנה אותה נערוך</param>
        public addDishWindows(uint dishId)
        {
            InitializeComponent();
            this.updateDishId = dishId;//קבלת מספר המנה שרוצים לעדכן
            fillSizeKosherCombobox();//עידכון שדה הבחירה של גודל וכשרויות

            newDish = new BE.Dish( );
            newDish = mybl.getDishById(dishId); //חיבור טופס המנה לאובייקט מנה



            orderGrid.DataContext = newDish;

            editValueTitleButtom();//עריכת כותרת העמוד+כפתור השליחה שיתאימו לעמוד עריכה

        }


        #endregion

        /// <summary>
        /// עידכון שדה הבחירה של גודל וכשרויות
        /// </summary>
        private void fillSizeKosherCombobox()
        {
            this.dishSize.ItemsSource = Enum.GetValues(typeof(BE.SizeOfDish));
            this.dishKosherLevel.ItemsSource = Enum.GetValues(typeof(BE.Kosher));
        }


        /// <summary>
        /// עריכת כותרת העמוד+כפתור השליחה שיתאימו לעמוד עריכה
        /// </summary>
        private void editValueTitleButtom()
        {
            this.buttonSend.Content = "ערוך מנה";
            this.title.Text = "עריכת מנה";
        }

        #endregion
        /// <summary>
        /// כאשר רוצים לערוך/להוסיף מנה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDishEvent(object sender, RoutedEventArgs e)
        {

            try
            {
                checkIfNull();//בדיקה שכל השדות מולאו
                if (newDish.dishID != 0)//אם אנחנו בעמוד עריכה
                {
                    string oldName = mybl.getDishById(newDish.dishID).dishName;//קבלת שם המנה הישן 
                    mybl.updateDish(newDish, oldName);

                }
                else
                    mybl.addDish(newDish);

                refresh(this, EventArgs.Empty);//הפעלת ריענון טבלה(למי שנרשם לאירוע הזה)
                MessageBox.Show("בוצע י' גבר");


            }
            catch (Exception error)
            {
                
                MessageBox.Show(error.Message);
            }


        }
        /// <summary>
        /// בדיקה האם השדות מלאים ותקינים
        /// </summary>
        private void checkIfNull()
        {
            if(name.Text.Length<4 )
                throw new Exception("שם המנה מינמום 4 תווים");
            if (int.Parse(price.Text) < 0)
                throw new Exception("מחיר לא יכול להיות שלילי");
            
            if(  dishSize.SelectedItem==null  || dishKosherLevel.SelectedItem==null ||price.Text.Length==0 )
                throw new Exception ("אנא מלא את כל השדות");
        }
        /// <summary>
        /// על מנת שיוכל להכניס רק מספרים
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
