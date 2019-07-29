using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// מחלקת מנה-מוזמנת, מממשת את 
    /// ICloneable
    /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
    /// </summary>
    public class OrderedDish
    {
        public uint orderId { get; set; }
        public uint dishID { get; set; }
        public uint numberOfSameDish { get; set; }
        public Kosher dishLevelOfKosher { get; set; }
        public SizeOfDish dishSize { get; set; }
        public string notes { get; set; }
        public override string ToString()

        {
            return base.ToString();
        }
    }
}
