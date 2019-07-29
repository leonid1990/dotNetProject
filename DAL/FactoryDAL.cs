using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public  class FactoryDAL
    {        


        public static IDAL getDAL()
        {
             //return Dal_XML_imp.Instance;
             return Dal_imp.Instance;
        }
    }
}
