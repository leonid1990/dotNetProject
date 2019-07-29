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
    public partial class addClientWindows : Window
    {
        BL.IBL mybl = BL.FactoryBL.getBL();
        public event EventHandler refresh;
        uint updateClientId = 0 ;//במידה ובחלון עריכה המשתנה ישתנה למספר זיהוי הלקוח אותו נערוך
        public BE.Client newClient ;

        #region construtors+sub methode

        /// <summary>
        /// בנאי שיופעל בחלון הוספת לקוח
        /// </summary>
        public addClientWindows()
        {
            InitializeComponent();
            newClient = new BE.Client();
            clientGrid.DataContext = newClient;
            ValueTitleButtom();//עריכת כותרת העמוד+כפתור השליחה שיתאימו לעמוד הוספה
        }

        /// <summary>
        /// בנאי שיופעל בחלון עריכת לקוח
        /// </summary>
        /// <param name="clientId">מספר זיהוי הלקוח אותו נערוך</param>
        public addClientWindows(uint clientId)
        {
            InitializeComponent();
            this.updateClientId = clientId;//קבלת מספר הלקוח שרוצים לעדכן

            newClient = new BE.Client();
            newClient = mybl.getClientByClientId(clientId);
            clientGrid.DataContext = newClient;




            ValueTitleButtom();//עריכת כותרת העמוד+כפתור השליחה שיתאימו לעמוד עריכה

        }

        /// <summary>
        /// עידכון כותרות העמוד+כפתור השליחה שיתאימו לטופס עריכה או הוספה
        /// </summary>
        private void ValueTitleButtom()
        {

            if (updateClientId != 0)//אם בעמוד עריכה
            {   this.title.Text = "עריכת לקוח";
                this.Title="עריכת לקוח";
                 this.buttonText.Content = "ערוך לקוח";
            }
            else
            {
                this.title.Text = "הוספת לקוח";
                this.Title = "הוספת לקוח";
                this.buttonText.Content = "הוסף לקוח";
            }
        }

        #endregion

        /// <summary>
        /// הוספת/עריכת לקוח
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addClientEvent(object sender, RoutedEventArgs e)
        {

            try
            {
                checkIfNull();//בדיקה שכל השדות מלאים כנדרש
                if(updateClientId ==0)//אם בחלון הוספת לקוח
                mybl.addClient(newClient);
                else
                {
                    string oldName = mybl.getClientByClientId(newClient.idOfClient).nameOfClient;//קבלת שם הלקוח הישן(לבדיקה אם לא ערך לשם לקוח שכבר קיים)
                    mybl.updateClient(newClient, oldName);

                }
                refresh(this, EventArgs.Empty);
                MessageBox.Show("בוצע י' גבר");

            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }


        }

        /// <summary>
        /// בדיקה שכל השדות מולאו
        /// </summary>
        private void checkIfNull()
        {
            if (name.Text.Length == 0 || adress.Text.Length == 0 || location.Text.Length == 0 || creditCard.Text.Length == 0 || age.Text.Length == 0)
                throw new Exception("אנא מלא את כל השדות");

            if (name.Text.Length < 4)
                throw new Exception("שם הלקוח מינמום 4 תווים");

        }

        /// <summary>
        /// פונקציה לקלט רק של ספרות
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
