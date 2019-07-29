using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DS_XML
    {
        #region Roots
        public static XElement branchesRoot;
        public static XElement clientsRoot;
        public static XElement dishesRoot;
        public static XElement ordersRoot;
        public static XElement orderedDishesRoot;
        public static XElement configRoot;
        #endregion

        #region Paths
        public static string branchesPath = @"BranchXml.xml";
        public static string clientsPath = @"ClientXml.xml";
        public static string dishesPath = @"DishXml.xml";
        public static string ordersPath = @"OrderXml.xml";
        public static string orderedDishesPath = @"OrderedDishXml.xml";
        public static string configPath = @"Config.xml";
        #endregion

    }
}
