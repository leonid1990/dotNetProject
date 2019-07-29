using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class bl:IBL
    {
        DAL.IDAL myDal = DAL.FactoryDAL.getDAL();

        public IEnumerable<BE.groupingClass> getOrderByCond(Predicate<BE.Order> cond)
       {
           var x = from item in myDal.getListOfAllOrders()
                   let nameOfClient = myDal.getClientByOrderId(item.orderId).nameOfClient
                   where cond(item)
                   select new BE.groupingClass { text = nameOfClient, profit = item.price };

           return x;
       }

       #region get profit by....
        public IEnumerable<BE.groupingClass> getProfitByKindOfDish()
       {
           var tempList = new List< IGrouping<uint, double>>();

           return  from item in myDal.getListOfAllDishOrders()
                   let priceOfDish = myDal.getPriceOfDish(item.dishID)
                   let profit=item.numberOfSameDish * priceOfDish
                   group profit by new {myDal.getDishById(item.dishID).dishName, item.dishID} into g
                   select new BE.groupingClass { text = g.Key.dishName, profit = g.Sum() };
 

           
        }

        public IEnumerable<BE.groupingClass> getProfitByDate()
        {

            var x = from order in myDal.getListOfAllOrders()
                                                            let tempOrderId = order.orderId
                                                            let date= order.dateOfOrder
                                                            let profit=order.price
                                                            group profit by date into g
                                                            select new BE.groupingClass { text = g.Key.Date.DayOfWeek.ToString(), profit = g.Sum() };
            return x;
        }

        public IEnumerable<BE.groupingClass> getProfitByCustomerAdress()
        {


            var x = from tempClient in myDal.getListOfAllClients()
                    let tempClientId = tempClient.idOfClient
                    let orderFromThisClient = myDal.getListOfAllOrders().Where(order => order.clientID == tempClientId)
                    let addressOfClient = tempClient.addressOfClient
                    from temp_order in orderFromThisClient
                    let profit = temp_order.price
                    group profit by new{ addressOfClient} into g
                    select new BE.groupingClass { text= g.Key.addressOfClient ,profit= g.Sum()};



            return x;
        }
       #endregion

        #region add/update/remove dish

        public void addDish(BE.Dish new_dish)
       {

           try
           {
               checkDoubleName(new_dish, new_dish.dishName);
               checkHighPrice(new_dish);
               myDal.addDish(new_dish);
           }
           catch (Exception)
           {
               throw;
           }
       }
        public void deleteDish(BE.Dish dish)
       {
           try
           {
               if (myDal.getListOfAllDishOrders().Exists(orderDish => orderDish.dishID == dish.dishID))
                   throw new Exception("המנה מוזמנת אצל לקוחות, אל תמחק !!!");
               myDal.deleteDish(dish);
           }
           catch (Exception)
           {               
               throw;
           }
       }
        public void updateDish(BE.Dish dish, string oldName)
        {


            try
            {
                if(dish.dishName!=oldName)//אם שונה שם המנה לבדוק שהשם לא מופיע כבר
                      checkDoubleName(dish,dish.dishName);
                checkHighPrice(dish);
                myDal.updateDish(dish);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region add/update/remove branch
        public void addBranch(BE.Branch new_branch)
        {
            
            try
            {              
                myDal.addBranch(new_branch);
            }
            catch (Exception)
            {               
                throw;
            }
        }
        public void deleteBranch(BE.Branch branchToDelete)
        {
            try
            {
                if (myDal.getListOfAllOrders().Exists(order => order.branchId == branchToDelete.branchId))
                    throw new Exception("הסניף משרת הזמנות קיימות, אל תמחק!!!");
                myDal.deleteBranch(branchToDelete);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateBranch(BE.Branch branch,string oldName)
        {
            try
            {
                if(oldName!=branch.nameOfBranch)//אם שם המנה שונה,לבדוק שהשם החד לא קיים כבר
                    checkDoubleName(branch, branch.nameOfBranch);
                myDal.updateBranch(branch);
            }
            catch (Exception)
            {               
                throw;
            }
        }
        #endregion

        #region add/update/remove order

        public uint addOrder(BE.Order order)
        {

            if (order.CourierNeeded && myDal.getBranchByBranchId(order.branchId).numberOfFreeCouriers==0 ) //אם צריך משלוח אבל אין מספיק שליחים בסניף שבחר
                throw new Exception("אין מספיק שליחים על מנת לבצע הזמנה");
            try
            {
                order.dateOfOrder = DateTime.Now;
                return myDal.addOrder(order);//הוספת ההזמנה
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void deleteOrder(uint orderId)
        {            
            try
            {
            // מחיקת ההזמנה
                myDal.deleteOrder(orderId);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateOrder(BE.Order order)
        {
            if (!myDal.getListOfAllBranches().Exists(branch => branch.branchId == order.branchId))
                throw new Exception("סניף לא קיים");
            if (order.CourierNeeded && myDal.getListOfAllBranches().Find(branch => branch.branchId == order.branchId).numberOfFreeCouriers == 0)
                throw new Exception("אין מספיק שליחים על מנת לבצע הזמנה");
            try
            {
                myDal.updateOrder(order);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region add/update/remove orderedDish

        /// <summary>
        /// הוספת מנה להזמנה קיימת או להזמנה חדשה
        /// </summary>
        /// <param name="l"></param>
        public void addOrderedDish(BE.OrderedDish new_ordered_dish)
        {

           // checkKosherLevel(new_ordered_dish);

            try
            {
                myDal.addOrderedDish(new_ordered_dish);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void deleteAllOrderedDishesByOrderId(uint orderId)
        {
            try
            {
                myDal.deleteAllOrderedDishesByOrderId(orderId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region add-update-remove client

        public void addClient(BE.Client client )
        {
            try
            {
                this.checkAge(client.age);//בדיקת תקינות גיל
                checkDoubleName(client, client.nameOfClient);//בדיקה אם לא קיים שם הלקוח כבר
                myDal.addClient(client); //הוספת הלקוח
            }
            catch (Exception)
            {                
                throw;
            }
        }
        public void deleteClient(uint clientId)
        {
            try
            {
                // מחיקת קליינט כולל כל ההזמנות אליו הוא נרשם
                myDal.deleteClient(clientId);
               
            }
            catch (Exception)
            {
                throw;
            }
        }
       public  void updateClient(BE.Client client,string oldName)
        {
            checkAge(client.age);
            try
            {
                if (client.nameOfClient != oldName)//אם שונה השם לבדוק שהשם לא קיים כבר במערכת
                    checkDoubleName(client, client.nameOfClient);
                myDal.updateClient(client);
            }
            catch (Exception)
            {               
                throw;
            }
        }

        #endregion

       
        /// <summary>
        /// חישוב המחיר של מנה מוזמנת אחת
        /// </summary>
        /// <param name="priceOfSingleDish">מחיר למנה אחת</param>
        /// <param name="numberOfSameDish">כמות</param>
        /// <returns></returns>
       public double calculatePrice(float priceOfSingleDish, uint numberOfSameDish)
       {
           double sum= priceOfSingleDish * numberOfSameDish;
           return sum;
       }
        

        #region Commomn_Exceptions

        /// <summary>
        /// בדיקת גיל מינימלי
        /// </summary>
        /// <param name="age"></param>
        /// 
       private void checkHighPrice(BE.Dish dish)
       {
           if (dish.priceOfSingleDish > dish.MAX_PRICE_FOR_DISH)
               throw new Exception("יקר מידי, אין מצב שיקנו");
       }

       #region check kosher & size level
       /// <summary>
        /// בודק האם המנה שרוצים להוסיף תקינה מבחינת כשרות וגודל
        /// </summary>
       /// <param name="newOrderDish">המנה שרוצים להוסיף</param>
       /// <param name="currentDishSize">גודל המנה שקיים במערכת</param>
       /// <param name="currentDishKosher">רמת הכשרות הקיימת במערכת</param>
        public void checkKosherSizeLevel(BE.OrderedDish newOrderDish, BE.SizeOfDish currentDishSize, BE.Kosher currentDishKosher)
       {
           try
           {
               checkKosherLevel(newOrderDish.dishLevelOfKosher, currentDishKosher);
               checkSizeLevel(newOrderDish.dishSize, currentDishSize);
           }
           catch (Exception)
           {
               
               throw;
           }
       }

        public void checkKosherLevel(BE.Kosher wantedKosher, BE.Kosher currentDishKosher)
        {
            if (wantedKosher > currentDishKosher)
                throw new Exception("רמת הכשרות לא מתאימה");
        }
        public void checkSizeLevel(BE.SizeOfDish wantedSize, BE.SizeOfDish currentDishSize)
        {
            if (wantedSize > currentDishSize)
                throw new Exception("אין לנו מנה בגודל כזה");
        }

       #endregion

        /// <summary>
        /// בדיקת כפילות שמות, יבדוק אם השם קיים כבר במקור הנתונים
        /// </summary>
        /// <typeparam name="T">יקבל אובייקט מסוג מסעדה/לקוח/מנה כדי לדעת על איזה רשימה לרוץ</typeparam>
        /// <param name="kindOfObject">סוג האובייקט שהתקבל גנרי</param>
        /// <param name="nameToSearch">שם לחיפוש האם כבר מופיע באגר הנתונים המתאים</param>
        private void checkDoubleName<T>(T kindOfObject ,string nameToSearch)
       {
           switch (typeof(T).Name)
           {
               case "Dish":
                  if ( myDal.getListOfAllDishes().Exists(dish => dish.dishName == nameToSearch))
                      throw new Exception("שם המנה קיים כבר");
                   break;
              case "Client":
                  if (myDal.getListOfAllClients().Exists(client => client.nameOfClient==nameToSearch))
                      throw new Exception("שם הלקוח קיים כבר");
                   break;

             case "Branch":
                  if ( myDal.getListOfAllBranches().Exists(branch => branch.nameOfBranch == nameToSearch))
                      throw new Exception("שם המסעדה קיים כבר");
                   break;


           }


       }

        public void checkAge(byte age)
        {
            if (age < 18)
                throw new Exception("לא ניתן להזמין מתחת לגיל 18");
        }
        /// <summary>
        /// בדיקת חריגה ממחיר מקסימלי של הזמנה
        /// </summary>
        /// <param name="price"></param>
        public void maxOrderPayment(double price)
        {
            if (price > BE.Order.MAX_PRICE_FOR_ORDER)
                throw new Exception("מחיר גבוה מדי להזמנה");
        }
        /// <summary>
        /// בדיקה האם דרישת הכשרות של הלקוח קיימת בסניף
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="branch"></param>

        #endregion

        #region get ...by ...

        public BE.Order getOrderById(uint orderId)
        {
            try
            {
                return myDal.getOrderById(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BE.Branch getBranchByBranchId(uint branchId)

        {
            try
            {
                return myDal.getBranchByBranchId(branchId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public BE.Client getClientByClientId(uint clientId)
        {
            try
            {
                return myDal.getClientByClientId(clientId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<BE.OrderedDish> getListOfOrderDishByOrderId(uint orderId)
        {
            try
            {
                return myDal.getListOfOrderDishByOrderId(orderId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public BE.Dish getDishByName(string nameOfDish)
        {
            try
            {
                return myDal.getDishByNmae(nameOfDish);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public BE.Dish getDishById(uint dish_id)
        {
            try
            {
                return myDal.getDishById(dish_id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string getNameOfBranchById(uint id)
        {
            try
            {
               return  myDal.getNameOfBranchById(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string getNameOfClientById(uint id)
        {
            try
            {
                return myDal.getNameOfClientById(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public BE.Branch getBranchByName(string nameOfbranch)
        {
            try
            {
                return myDal.getBranchByName(nameOfbranch);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public BE.Client getClientIdByName(string nameOfclient)
        {
            try
            {
                return myDal.getClientIdByName(nameOfclient);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
        #region summary ditails
        public  string mostExpensiveDish()
        {
            return myDal.getListOfAllDishes().Max().dishName;
        }
        public string mostPopularDish()
        {
            return (from item in myDal.getListOfAllDishes()
                    group item by item.dishName into g
                    orderby g.Count() descending
                    select g.Key).First();     
        }
        public string mostPopularBranch()
        {
          return   (from item in myDal.getListOfAllBranches()
                group item by item.nameOfBranch into g
                orderby g.Count() descending
                select g.Key).First();       
            
        }

        public string mostPopularClient()
        {
          return   (from item in myDal.getListOfAllClients()
                group item by item.nameOfClient into g
                orderby g.Count() descending
                select g.Key).First();  
        }

        public double sumProfit ()
        {
           return myDal.getListOfAllOrders().Sum(order=>order.price);
        }

        public int  numberOfDishes ()
        {
           return myDal.getListOfAllDishes().Count();
        }

         public int numberOfBranches ()
        {
           return myDal.getListOfAllBranches().Count();
        }

        public int numberOfClients ()
        {
            return myDal.getListOfAllClients().Count();
        }
      

        #endregion
        #region get list of all be

        public List<BE.OrderedDish> getListOfDishOrders(){ return myDal.getListOfAllDishOrders(); }
        public List<BE.Order> getListOfAllOrders() { return myDal.getListOfAllOrders(); }
        public List<BE.Dish> getListOfAllDishes() { return myDal.getListOfAllDishes(); }
        public List<BE.Branch> getListOfAllBranches() { return myDal.getListOfAllBranches(); }
        public List<BE.Client> getListOfAllClient() { return myDal.getListOfAllClients(); }

        #endregion
    }
}
  