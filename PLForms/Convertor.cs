using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using System.Globalization;


namespace PLForms
{
     /// <summary>
     /// המרה של בולינאי ל"כן" ו "לא"
     /// </summary>
        public class ConvertBoolForText  : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if ((bool)value)
                return "כן".ToString();
            else
                return "לא".ToString();
            
        }
        public object ConvertBack( object value,Type targetType,object parameter,CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// המרה ממספר מנה לשם מנה
    /// </summary>
    public class ConvertIDtoNameOfDish : IValueConverter
    {
        BL.IBL mybl = BL.FactoryBL.getBL();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return mybl.getDishById(uint.Parse(value.ToString())).dishName;
            
        }
        public object ConvertBack( object value,Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// המרה ממספר לקוח לשם לקוח
    /// </summary>
    public class ConvertIDtoNameOfClient: IValueConverter
    {
        BL.IBL mybl = BL.FactoryBL.getBL();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          
                return mybl.getNameOfClientById((uint.Parse(value.ToString())));
            
        }
        public object ConvertBack( object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    /// <summary>
    /// המרה ממספר מסעדה לשם מסעדה
    /// </summary>
    public class ConvertIDtoNameOfBranch: IValueConverter
    {
                BL.IBL mybl = BL.FactoryBL.getBL();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          
               string name =mybl.getNameOfBranchById((uint.Parse(value.ToString())));
               return name;
            
        }
        public object ConvertBack(object value , Type targetType   , object parameter,CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        



    }
}
