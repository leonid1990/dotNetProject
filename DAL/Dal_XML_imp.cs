using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {

        #region singeltone
        private static  Dal_XML_imp instance;

       public static Dal_XML_imp Instance
       {
           get
           {
               if (instance == null)
               {
                   instance = new Dal_XML_imp();
               }
               return instance;
           }
       }
       #endregion

       #region CTOR

       /// <summary>
        /// אם הקובץ לא קיים הבנאי יוצר אותו. אם קיים, הבנאי טוען אותו ואת קובץ הקונפיגורציות, שם שמור מס' הזיהוי הרץ
        /// </summary>
        private Dal_XML_imp()
        {
            if (!File.Exists(DS_XML.branchesPath))    
                CreateBranchesFile();
            else
            {
                loadBranchesFile(); 
                loadConfigFile();
                Branch.branchMaxID = uint.Parse(DS_XML.configRoot.Element("BranchMaxID").Value);
            }

            if (!File.Exists(DS_XML.clientsPath))
                CreateClientsFile();
            else
            {
                loadClientsFile();
                loadConfigFile();
                Client.clientmaxID = uint.Parse(DS_XML.configRoot.Element("ClientMaxID").Value);
            }

            if (!File.Exists(DS_XML.dishesPath))
                CreateDishesFile();
            else
            {
                loadDishesFile();
                loadConfigFile();
                Dish.dishMaxId = uint.Parse(DS_XML.configRoot.Element("DishMaxID").Value);
            }

            if (!File.Exists(DS_XML.ordersPath))
                CreateOrdersFile();
            else
            {
                loadOrdersFile();
                loadConfigFile();
                Order.orderDefaultId = uint.Parse(DS_XML.configRoot.Element("OrderMaxID").Value);
            }

            if (!File.Exists(DS_XML.orderedDishesPath))
                CreateOrderedDishesFile();
            else
                loadOrderedDishesFile();

            if (!File.Exists(DS_XML.configPath))
                CreateConfigFile();
            else
                loadConfigFile();
        }
        #endregion

        #region createXmlFiles
        //  פונקציות יצירת הקבצים - אתחול השורש, אתחול מס' הזיהוי למס' הראשון ושמירה
        private void CreateBranchesFile()
        {
            CreateConfigFile();
            DS_XML.branchesRoot = new XElement("Branches");
            XElement static_id = new XElement("BranchMaxID", 1);
            DS_XML.configRoot.Add(static_id);
            DS_XML.branchesRoot.Save(DS_XML.branchesPath);
            DS_XML.configRoot.Save( DS_XML.configPath);
        }
        private void CreateClientsFile()
        {
            DS_XML.clientsRoot = new XElement("Clients");
            XElement static_id = new XElement("ClientMaxID", 1);
            DS_XML.configRoot.Add(static_id);
            DS_XML.clientsRoot.Save(DS_XML.clientsPath);
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        private void CreateDishesFile()
        {
            DS_XML.dishesRoot = new XElement("Dishes");
            XElement static_id = new XElement("DishMaxID", 1);
            DS_XML.configRoot.Add(static_id);
            DS_XML.dishesRoot.Save(DS_XML.dishesPath);
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        private void CreateOrdersFile()
        {
            DS_XML.ordersRoot = new XElement("Orders");
            XElement static_id = new XElement("OrderMaxID", 1);
            DS_XML.configRoot.Add(static_id);
            DS_XML.ordersRoot.Save(DS_XML.ordersPath);
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        private void CreateOrderedDishesFile()
        {
            DS_XML.orderedDishesRoot = new XElement("OrderedDishes");
            XElement static_id = new XElement("OrderedDishMaxID", 1);
            DS_XML.configRoot.Add(static_id);
            DS_XML.orderedDishesRoot.Save(DS_XML.orderedDishesPath);
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        private void CreateConfigFile()
        {
            DS_XML.configRoot = new XElement("Config");
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        #endregion

        #region loadXmlFiles
        //  פונקציות טעינת הקבצים
        void loadBranchesFile()
        {
            try
            {
                DS_XML.branchesRoot = XElement.Load(DS_XML.branchesPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }
        void loadClientsFile()
        {
            try
            {
                DS_XML.clientsRoot = XElement.Load(DS_XML.clientsPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }
        void loadDishesFile()
        {
            try
            {
                DS_XML.dishesRoot = XElement.Load(DS_XML.dishesPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }
        void loadOrdersFile()
        {
            try
            {
                DS_XML.ordersRoot = XElement.Load(DS_XML.ordersPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }
        void loadOrderedDishesFile()
        {
            try
            {
                DS_XML.orderedDishesRoot = XElement.Load(DS_XML.orderedDishesPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }
        void loadConfigFile()
        {
            try
            {
                DS_XML.configRoot = XElement.Load(DS_XML.configPath);
            }
            catch
            {
                throw new Exception("נמצאה שגיאה בהעלאת הקובץ");
            }
        }

        #endregion

        #region add/update/delete Branch
        public void addBranch(Branch brnch)
        {
            loadBranchesFile();
            loadConfigFile();
            // בדיקה האם הסניף קיים
            XElement branchCheck = (from b in DS_XML.branchesRoot.Elements()
                                    where uint.Parse(b.Element("ID").Value) == brnch.branchId
                                    select b).FirstOrDefault();
            if (branchCheck != null)
                throw new Exception("סניף זה כבר קיים במערכת");
            if (Branch.branchMaxID == 99999999)
                throw new Exception("שגיאת מערכת: לא ניתן להוסיף סניפים חדשים. מספר הסניף יחרוג משמונה תווים");
            //  יצירת סניף חדש
            XElement id = new XElement("ID", Branch.branchMaxID++);
            XElement name = new XElement("Name", brnch.nameOfBranch);
            XElement addrs = new XElement("Address", brnch.adressOfBranch);
            XElement phone = new XElement("Phone", brnch.phoneNumberOfBranch);
            XElement responsible = new XElement("Responsible", brnch.nameOfResponsible);
            XElement workers_num = new XElement("Workers", brnch.numberOfWorkers);
            XElement couriers = new XElement("Free-Couriers", brnch.numberOfFreeCouriers);
            XElement branch = new XElement("Branch", id, name, addrs, phone, responsible, workers_num, couriers);
            // הוספת הסניף לקובץ ושמירה
            DS_XML.branchesRoot.Add(branch);
            DS_XML.branchesRoot.Save(DS_XML.branchesPath);
            // שמירת מס' הזיהוי הרץ
            DS_XML.configRoot.Element("BranchMaxID").Value = Branch.branchMaxID.ToString();
            DS_XML.configRoot.Save(DS_XML.configPath);
            
        }

        public void updateBranch(Branch brnch)
        {
            loadBranchesFile();
            // בדיקה האם הסניף קיים
            XElement branch = (from b in DS_XML.branchesRoot.Elements()
                               where uint.Parse(b.Element("ID").Value) == brnch.branchId
                               select b).FirstOrDefault();
            if (branch == null)
                throw new Exception("סניף לא נמצא");
            //  עדכון הסניף
            branch.Element("Name").Value = brnch.nameOfBranch;
            branch.Element("Address").Value = brnch.adressOfBranch;
            branch.Element("Phone").Value = brnch.phoneNumberOfBranch.ToString();
            branch.Element("Responsible").Value = brnch.nameOfResponsible;
            branch.Element("Credit-Workers").Value = brnch.numberOfWorkers.ToString();
            branch.Element("Free-Couriers").Value = brnch.numberOfFreeCouriers.ToString();
            DS_XML.branchesRoot.Save(DS_XML.branchesPath);
        }

        public void deleteBranch(Branch brnch)
        {
            loadBranchesFile();
            // בדיקה האם הסניף קיים
            XElement branch = (from b in DS_XML.branchesRoot.Elements()
                               where uint.Parse(b.Element("ID").Value) == brnch.branchId
                               select b).FirstOrDefault();
            if (branch == null)
                throw new Exception("סניף לא נמצא");
            //  מחיקת הסניף
            branch.Remove();
            DS_XML.branchesRoot.Save(DS_XML.branchesPath);

        }
        #endregion

        #region add/update/delete Client
        public void addClient(Client clnt)
        {
            loadClientsFile();
            loadConfigFile();
            // בדיקה האם הלקוח קיים
            XElement clientCheck = (from c in DS_XML.clientsRoot.Elements()
                                    where uint.Parse(c.Element("ID").Value) == clnt.idOfClient
                                    select c).FirstOrDefault();
            if (clientCheck != null)
                throw new Exception("לקוח זה כבר קיים במערכת");
            if (Client.clientmaxID == 99999999)
                throw new Exception("שגיאת מערכת: לא ניתן להוסיף לקוחות חדשים. מספר הלקוח יחרוג משמונה תווים");
            //  יצירת לקוח חדש
            XElement id = new XElement("ID", Client.clientmaxID++);
            XElement name = new XElement("Name", clnt.nameOfClient);
            XElement addrs = new XElement("Address", clnt.addressOfClient);
            XElement age = new XElement("Age", clnt.age);
            XElement location = new XElement("Location", clnt.LocationOfClient);
            XElement creditCard = new XElement("Credit-Card", clnt.numberOfCreditCard);
            XElement client = new XElement("Client", id, name, addrs, age, location, creditCard);
            // הוספת הלקוח לקובץ ושמירה
            DS_XML.clientsRoot.Add(client);
            DS_XML.clientsRoot.Save(DS_XML.clientsPath);
            // שמירת מס' הזיהוי הרץ
            DS_XML.configRoot.Element("ClientMaxID").Value = Client.clientmaxID.ToString();
            DS_XML.configRoot.Save(DS_XML.configPath);
            
        }

        public void updateClient(Client clnt)
        {
            loadClientsFile();
            // בדיקה האם הלקוח קיים
            XElement client = (from c in DS_XML.clientsRoot.Elements()
                               where uint.Parse(c.Element("ID").Value) == clnt.idOfClient
                               select c).FirstOrDefault();
            if (client == null)
                throw new Exception("לקוח לא נמצא");
            //  עדכון הלקוח
            client.Element("Name").Value = clnt.nameOfClient;
            client.Element("Address").Value = clnt.addressOfClient;
            client.Element("Age").Value = clnt.age.ToString();
            client.Element("Location").Value = clnt.LocationOfClient;
            client.Element("Credit-Card").Value = clnt.numberOfCreditCard.ToString();
            DS_XML.clientsRoot.Save(DS_XML.clientsPath);
        }

        public void deleteClient(uint clientId)
        {
            loadClientsFile();
            // בדיקה האם הלקוח קיים
            XElement client = (from c in DS_XML.clientsRoot.Elements()
                               where uint.Parse(c.Element("ID").Value) == clientId
                               select c).FirstOrDefault();
            if (client == null)
                throw new Exception("לקוח לא נמצא");
            //  מחיקת הלקוח
            client.Remove();
            DS_XML.clientsRoot.Save(DS_XML.clientsPath);

            // מחיקת כל ההזמנות של הקליינט, יכנס לכאן רק אם הקליינט היה קיים
            List<BE.Order> listOfOrderFromClient = new List<BE.Order>();
            listOfOrderFromClient = getListOfAllOrders().Where(order => order.clientID == clientId).ToList();//רשימת כל ההזמנות של המשתמש

            foreach (BE.Order order in listOfOrderFromClient) //מחיקת כל ההזמנות של המשתמש
                this.deleteOrder(order.orderId);

        }
        #endregion

        #region add/update/delete Dish

        public void addDish(BE.Dish new_dish)
        {
            loadDishesFile();
            loadConfigFile();
            // בדיקה האם המנה קיימת
            XElement dishCheck = (from d in DS_XML.dishesRoot.Elements()
                                  where uint.Parse(d.Element("ID").Value) == new_dish.dishID
                                  select d).FirstOrDefault();
            if (dishCheck != null)
                throw new Exception("מנה זו כבר קיימת במערכת");
            if (Client.clientmaxID == 99999999)
                throw new Exception("שגיאת מערכת: לא ניתן להוסיף מנות חדשות. מספר המנה יחרוג משמונה תווים");
            //  הוספת מנה חדשה
            XElement id = new XElement("ID", Dish.dishMaxId++);
            XElement name = new XElement("Name", new_dish.dishName);
            XElement size = new XElement("Size", new_dish.dishSize);
            XElement price = new XElement("Price", new_dish.priceOfSingleDish);
            XElement kosher = new XElement("Kosher", new_dish.dishLevelOfKosher);
            XElement dish = new XElement("Dish", id, name, size, price, kosher);
            DS_XML.dishesRoot.Add(dish);
            DS_XML.dishesRoot.Save(DS_XML.dishesPath);
            // שמירת מס' הזיהוי הרץ
            DS_XML.configRoot.Element("DishMaxID").Value = Dish.dishMaxId.ToString();
            DS_XML.configRoot.Save(DS_XML.configPath);
        }
        public void updateDish(Dish dsh)
        {
            loadDishesFile();
            // בדיקה האם המנה קיימת
            XElement dish = (from d in DS_XML.dishesRoot.Elements()
                             where uint.Parse(d.Element("ID").Value) == dsh.dishID
                             select d).FirstOrDefault();
            if (dish == null)
                throw new Exception("מנה לא נמצאה");
            // עדכון המנה
            dish.Element("Name").Value = dsh.dishName;
            dish.Element("Size").Value = dsh.dishSize.ToString();
            dish.Element("Price").Value = dsh.priceOfSingleDish.ToString();
            dish.Element("Kosher").Value = dsh.dishLevelOfKosher.ToString();
            DS_XML.dishesRoot.Save(DS_XML.dishesPath);
        }
        public void deleteDish(Dish dsh)
        {
            loadDishesFile();
            // בדיקה האם המנה קיימת
            XElement dish = (from d in DS_XML.dishesRoot.Elements()
                             where uint.Parse(d.Element("ID").Value) == dsh.dishID
                             select d).FirstOrDefault();
            if (dish == null)
                throw new Exception("מנה לא נמצאה");
            // מחיקת המנה
            dish.Remove();
            DS_XML.dishesRoot.Save(DS_XML.dishesPath);
        }
        #endregion

        #region add/update/delete Order
        public uint addOrder(Order new_order)
        {
            loadOrdersFile();
            loadConfigFile();
            // בדיקה האם ההזמנה קיימת
            XElement orderCheck = (from o in DS_XML.ordersRoot.Elements()
                                   where uint.Parse(o.Element("OrderID").Value) == new_order.orderId
                                   select o).FirstOrDefault();
            if (orderCheck != null)
                throw new Exception("הזמנה זו כבר קיימת במערכת");
            if (Order.orderDefaultId == 99999999)
                throw new Exception("שגיאת מערכת: לא ניתן להוסיף הזמנות חדשות. מספר ההזמנה יחרוג משמונה תווים");
            XElement order_id = new XElement("OrderID", Order.orderDefaultId++);
            XElement date = new XElement("Date", new_order.dateOfOrder);
            XElement branch_id = new XElement("BranchID", new_order.branchId);
            XElement client_id = new XElement("ClientID", new_order.clientID);
            XElement courier = new XElement("CourierNeeded", new_order.CourierNeeded);
            XElement price = new XElement("Price", new_order.price);
            XElement order = new XElement("Order", order_id, date, branch_id, client_id, courier, price);
            DS_XML.ordersRoot.Add(order);
            DS_XML.ordersRoot.Save(DS_XML.ordersPath);
            // שמירת מס' הזיהוי הרץ
            DS_XML.configRoot.Element("OrderMaxID").Value = Order.orderDefaultId.ToString();
            DS_XML.configRoot.Save(DS_XML.configPath);
            

            return Order.orderDefaultId - 1;    //  החזרת מס' הזיהוי של המנה שזה עתה נוספה, המשתנה הזה שומר תמיד את מס' הזיהוי של המנה הבאה ולא של הנוכחית
        }
        public void updateOrder(Order ordr)
        {
            loadOrdersFile();
            // בדיקה האם ההזמנה קיימת
            XElement order = (from o in DS_XML.ordersRoot.Elements()
                              where uint.Parse(o.Element("OrderID").Value) == ordr.orderId
                              select o).FirstOrDefault();
            if (order == null)
                throw new Exception("הזמנה לא נמצאה");
            // עדכון ההזמנה            
            order.Element("BranchID").Value = ordr.branchId.ToString();
            order.Element("ClientID").Value = ordr.clientID.ToString();
            order.Element("CourierNeeded").Value = ordr.CourierNeeded.ToString();
            order.Element("Price").Value = ordr.price.ToString();
            order.Element("Date").Value = ordr.dateOfOrder.ToString();
            DS_XML.ordersRoot.Save(DS_XML.ordersPath);
        }
        public void deleteOrder(uint orderId)
        {
            loadOrdersFile();
            // בדיקה האם ההזמנה קיימת
            XElement order = (from o in DS_XML.ordersRoot.Elements()
                              where uint.Parse(o.Element("OrderID").Value) == orderId
                              select o).FirstOrDefault();
            if (order == null)
                throw new Exception("הזמנה לא נמצאה");
            // מחיקת ההזמנה
            order.Remove();
            DS_XML.ordersRoot.Save(DS_XML.ordersPath);

            // מחיקת כל המנות המוזמנות, יכנס לכאן רק אם ההזמנה קיימת
            deleteAllOrderedDishesByOrderId(orderId);
        }
        #endregion

        #region add/update/delete OrderedDish
        public void addOrderedDish(BE.OrderedDish new_orderedDish)
        {
            loadOrderedDishesFile();
            // יצירת מנה מוזמנת
            XElement order_id = new XElement("OrderID", new_orderedDish.orderId);
            XElement dish_id = new XElement("DishID", new_orderedDish.dishID);
            XElement amount = new XElement("SameDish", new_orderedDish.numberOfSameDish);
            XElement kosher = new XElement("Kosher", new_orderedDish.dishLevelOfKosher);
            XElement size = new XElement("Size", new_orderedDish.dishSize);
            XElement notes = new XElement("Notes", new_orderedDish.notes);
            XElement ordered_dish = new XElement("OrderedDish", order_id, dish_id, amount, kosher, size, notes);
            // הוספת המנה המוזמנת ושמירה
            DS_XML.orderedDishesRoot.Add(ordered_dish);
            DS_XML.orderedDishesRoot.Save(DS_XML.orderedDishesPath);
        }
        public void deleteAllOrderedDishesByOrderId(uint orderId)
        {
            loadOrdersFile();
            // בדיקה האם המנה-המוזמנת קיימת
            List<XElement> toDelete = (from od in DS_XML.orderedDishesRoot.Elements()
                                     where uint.Parse(od.Element("OrderID").Value) == orderId
                                     select od).ToList();
            if (toDelete == null)
                throw new Exception("הזמנה לא נמצאה");
            // מחיקת המנות-המוזמנות
            foreach (XElement item in toDelete)
            {
                item.Remove();
            }
            DS_XML.orderedDishesRoot.Save(DS_XML.orderedDishesPath);
        }
        #endregion

        #region Various

        public string getNameOfBranchById(uint id)
        {
            return getListOfAllBranches().Find(b => b.branchId == id).nameOfBranch;
        }

        public string getNameOfClientById(uint id)
        {
            return getListOfAllClients().Find(c => c.idOfClient == id).nameOfClient;
        }

        public BE.Branch getBranchByName(string name)
        {
            return getListOfAllBranches().Find(b => b.nameOfBranch == name);
        }

        public BE.Client getClientIdByName(string name)
        {
            return getListOfAllClients().Find(c => c.nameOfClient == name);
        }

        public BE.Dish getDishById(uint dish_id)
        {
            return getListOfAllDishes().Find(d => d.dishID == dish_id);
        }

        public BE.Dish getDishByNmae(string nameOfDish)
        {
            return getListOfAllDishes().Find(d => d.dishName == nameOfDish);
        }

        public BE.Kosher getKosherOfDish(uint id)
        {
            return getListOfAllDishes().Find(d => d.dishID == id).dishLevelOfKosher;
        }

        public BE.Client getClientByOrderId(uint orderId)
        {
            return getListOfAllClients().FirstOrDefault(client => client.idOfClient == getOrderById(orderId).clientID);
        }

        public BE.Client getClientByClientId(uint clientId)
        {
            return getListOfAllClients().Find(c => c.idOfClient == clientId);
        }

        public BE.Order getOrderById(uint id)
        {
            return getListOfAllOrders().Find(o => o.orderId == id);
        }

        public BE.Branch getBranchByOrderId(uint orderId)
        {
            return getListOfAllBranches().FirstOrDefault(b => b.branchId == getOrderById(orderId).branchId);
        }

        public double getPriceOfDish(uint id)
        {
            return getListOfAllDishes().Find(d => d.dishID == id).priceOfSingleDish;
        }

        public List<BE.OrderedDish> getListOfOrderDishByOrderId(uint id)
        {
            return getListOfAllDishOrders().Where(od => od.orderId == id).ToList();
        }

        public BE.Branch getBranchByBranchId(uint id)
        {
            return getListOfAllBranches().Find(b => b.branchId == id);
        }
        #endregion

        #region getListofAll...
        public List<BE.OrderedDish> getListOfAllDishOrders()
        {
            loadOrderedDishesFile();
            List<OrderedDish> orderedDishes = new List<OrderedDish>();
            try
            {
                orderedDishes = (from od in DS_XML.orderedDishesRoot.Elements()
                                 where od.HasElements
                                 select XElementToOrderedDish(od)).ToList();
            }
            catch
            {
                orderedDishes = null;
            }

            return orderedDishes;
        }

        public List<BE.Order> getListOfAllOrders()
        {
            loadOrdersFile();
            List<Order> orders = new List<Order>();
            try
            {
                orders = (from o in DS_XML.ordersRoot.Elements()
                          where o.HasElements
                          select XElementToOrder(o)).ToList();
            }
            catch
            {
                orders = null;
            }

            return orders;
        }

        public List<BE.Dish> getListOfAllDishes()
        {
            loadDishesFile();
            List<Dish> dishes = new List<Dish>();
            try
            {
                dishes = (from d in DS_XML.dishesRoot.Elements()
                          where d.HasElements
                          select XElementToDish(d)).ToList();
            }
            catch
            {
                dishes = null;
            }

            return dishes;
        }

        public List<BE.Branch> getListOfAllBranches()
        {
            loadBranchesFile();
            List<Branch> branches = new List<Branch>();
            try
            {
                branches = (from b in DS_XML.branchesRoot.Elements()
                            where b.HasElements
                            select XElementToBranch(b)).ToList();
            }
            catch
            {
                branches = null;
            }

            return branches;
        }

        public List<BE.Client> getListOfAllClients()
        {
            loadClientsFile();
            List<Client> clients = new List<Client>();
            try
            {
                clients = (from c in DS_XML.clientsRoot.Elements()
                           where c.HasElements
                           select XElementToClient(c)).ToList();
            }
            catch
            {
                clients = null;
            }

            return clients;
        }
        #endregion

        #region XElementTo...
        //  הפונקציות דלהלן ממירות רשומה שנטענה מקובץ לאובייקט המתאים
        Branch XElementToBranch(XElement XBranch)
        {
            Branch branch = new Branch
            {
                branchId = uint.Parse(XBranch.Element("ID").Value),
                nameOfBranch = XBranch.Element("Name").Value,
                adressOfBranch = XBranch.Element("Address").Value,
                phoneNumberOfBranch = XBranch.Element("Phone").Value,
                nameOfResponsible = XBranch.Element("Responsible").Value,
                numberOfWorkers = ushort.Parse(XBranch.Element("Workers").Value),
                numberOfFreeCouriers = ushort.Parse(XBranch.Element("Free-Couriers").Value)
            };

            return branch;
        }

        Client XElementToClient(XElement XClient)
        {
            Client client = new Client
            {
                idOfClient = uint.Parse(XClient.Element("ID").Value),
                nameOfClient = XClient.Element("Name").Value,
                addressOfClient = XClient.Element("Address").Value,
                LocationOfClient = XClient.Element("Location").Value,
                age = byte.Parse(XClient.Element("Age").Value),
                numberOfCreditCard = ulong.Parse(XClient.Element("Credit-Card").Value)
            };

            return client;
        }

        Dish XElementToDish(XElement XDish)
        {
            Dish dish = new Dish
            {
                dishID = uint.Parse(XDish.Element("ID").Value),
                dishName = XDish.Element("Name").Value,
                dishSize = (SizeOfDish)Enum.Parse(typeof(SizeOfDish), XDish.Element("Size").Value),
                priceOfSingleDish = uint.Parse(XDish.Element("Price").Value),
                dishLevelOfKosher = (Kosher)Enum.Parse(typeof(Kosher), XDish.Element("Kosher").Value),
            };

            return dish;
        }

        Order XElementToOrder(XElement XOrder)
        {
            Order order = new Order
            {
                orderId = uint.Parse(XOrder.Element("OrderID").Value),
                dateOfOrder = DateTime.Parse(XOrder.Element("Date").Value),
                branchId = uint.Parse(XOrder.Element("BranchID").Value),
                clientID = uint.Parse(XOrder.Element("ClientID").Value),
                CourierNeeded = bool.Parse(XOrder.Element("CourierNeeded").Value),
                price = double.Parse(XOrder.Element("Price").Value),
            };

            return order;
        }

        OrderedDish XElementToOrderedDish(XElement XOrderedDish)
        {
            OrderedDish orderedDish = new OrderedDish
            {
                orderId = uint.Parse(XOrderedDish.Element("OrderID").Value),
                dishID = uint.Parse(XOrderedDish.Element("DishID").Value),
                numberOfSameDish = uint.Parse(XOrderedDish.Element("SameDish").Value),
                dishLevelOfKosher = (Kosher)Enum.Parse(typeof(Kosher), XOrderedDish.Element("Kosher").Value),
                dishSize = (SizeOfDish)Enum.Parse(typeof(SizeOfDish), XOrderedDish.Element("Size").Value),
                notes = XOrderedDish.Element("Notes").Value,
            };

            return orderedDish;
        }


        #endregion
    }
}