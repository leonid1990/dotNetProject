using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// מחלקת מנה, מממשת את 
    /// ICloneable
    /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
    /// המחלקה מממשת גם את
    /// IComparable
    /// על מנת שנוכל למצוא את המנה הכי יקרה, על ידי
    /// Extension Method Max()
    /// הדורש את המימוש הנ"ל
    /// </summary>
    public class Dish : ICloneable , IComparable
    {
        public static uint dishMaxId = 1;
        public uint MAX_PRICE_FOR_DISH = 400 ;
        public uint dishID { get; set; }
        public string dishName { get; set; }
        public SizeOfDish dishSize { get; set; }
        public uint priceOfSingleDish { get; set; }
        public Kosher dishLevelOfKosher { get; set; }

        
        public override string ToString()
        {
            return base.ToString();
        }
        /// <summary>
        /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
        /// </summary>
        /// <returns>אובייקט מועתק</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }


        /// <summary>
        /// מימוש את מחלקת השוואה, 
        /// </summary>
        /// <param name="obj">האובייקט המושווה</param>
        /// <returns>תחזיר מספר המציג מי יותר גדול</returns>
        public int CompareTo(object obj)
        {
            return this.priceOfSingleDish.CompareTo(((BE.Dish)obj).priceOfSingleDish );
        }
    }
}
