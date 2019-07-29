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
    /// Interaction logic for addBranchWindows.xaml
    /// </summary>
    public partial class addBranchWindows : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();
        public event EventHandler refresh;
        BE.Branch newBranch;

        #region constructors (edit+add) +method

        /// <summary>
        /// בנאי שיופעל לחלון הוספת מסעדה
        /// </summary>
        public addBranchWindows()
        {
            InitializeComponent();
            newBranch = new BE.Branch();
            branchGrid.DataContext = newBranch;
            ValueTitleButtom();//עריכת כותרות העמוד+כפתור השליחה
            
        }
        /// <summary>
        /// בנאי שיופעל לחלון עריכת מסעדה
        /// </summary>
        /// <param name="branchId"></param>
        public addBranchWindows (uint branchId)
        {
            InitializeComponent();
            newBranch = new BE.Branch();
            newBranch = mybl.getBranchByBranchId(branchId); //קבלת האובייקט מסעדה על פי מספר הזיהוי שקיבלתי
            branchGrid.DataContext = newBranch;
            ValueTitleButtom();//עריכת כותרות העמוד+כפתור השליחה
        }
        /// <summary>
        /// עדכון כותרות העמוד+כפתור השליחה בהתאם לעמוד עריכה/הוספת מסעדה
        /// </summary>
        private void ValueTitleButtom()
        {
            if (newBranch.branchId== 0)//אם בעמוד הוספה
            {
                this.Title = "עמוד הוספת מסעדה";
                this.buttonForm.Content = "הוסף מסעדה";
                this.title.Text = "הוספת מסעדה";
            }
            else//אם בעמוד עריכה
            {
                this.Title = "עמוד עריכת מסעדה";
                this.buttonForm.Content = "ערוך מסעדה";
                this.title.Text = "עריכת מסעדה";
            }
        }

        #endregion

        private void addBranchEvent(object sender, RoutedEventArgs e)
        {

            try
            {
                checkIfNull();
                if (newBranch.branchId == 0)//אם בעמוד הוספת מסעדה
                    mybl.addBranch(newBranch);
                else
                {
                    string oldName = mybl.getBranchByBranchId(newBranch.branchId).nameOfBranch;
                    mybl.updateBranch(newBranch, oldName);
                }
                refresh(this, EventArgs.Empty);
                MessageBox.Show("בוצע י' גבר");

            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }


        }

        private void checkIfNull()
        {
            if (name.Text.Length == 0 || 
                adress.Text.Length == 0 ||
               // branchKosherLevel.SelectedItem == null ||
                phone.Text.Length == 0 ||
                responsible.Text.Length == 0 ||
                free_couriers.Text.Length == 0 ||
                workers.Text.Length == 0)
              
                    throw new Exception("אנא מלא את כל השדות");

            if (name.Text.Length < 4)
                throw new Exception("שם הסניף מינמום 4 תווים");


        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
