using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// מחלקת לקוח, מממשת את 
    /// ICloneable
    /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
    /// </summary>
    public class Client :ICloneable
    {
        public static uint clientmaxID = 1;
        public uint idOfClient { get; set; }
        public string nameOfClient { get; set; }
        public string addressOfClient { get; set; }
        public string LocationOfClient { get; set; }
        public ulong numberOfCreditCard { get; set; }
        public byte age { get; set; }
        /// <summary>
        /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
        /// </summary>
        /// <returns>אובייקט מועתק</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
