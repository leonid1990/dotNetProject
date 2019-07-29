using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// מחלקת סניף, מממשת את 
    /// ICloneable
    /// על מנת שהאובייקט המוחזר ממקור הנתונים לא יוחזר כמשתנה ייחוס ואז התצוגה הגרפית תוכל חלילה לשנותו ישירות ללא מעבר על פי התקן
    /// </summary>
    public class Branch :ICloneable
    {
        public static uint branchMaxID = 1;
        public uint branchId { get; set; }
        public string nameOfBranch { get; set; }
        public string adressOfBranch { get; set; }
        public string phoneNumberOfBranch { get; set; }
        public string nameOfResponsible { get; set; }
        public ushort numberOfWorkers { get; set; }
        public ushort numberOfFreeCouriers { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }




    }
}
