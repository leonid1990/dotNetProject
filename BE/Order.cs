using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// מחלקת הזמנה, מממשת את 
    /// ICloneable
    /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
    /// </summary>
    public class Order
    {
        public static uint orderDefaultId = 1;
        public static uint MAX_PRICE_FOR_ORDER = 1000;
        public uint orderId { get; set; }
        public DateTime dateOfOrder { get; set; }
        public uint branchId { get; set; }
        public uint clientID { get; set; }
        public bool CourierNeeded { get; set; }
        public double price { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
