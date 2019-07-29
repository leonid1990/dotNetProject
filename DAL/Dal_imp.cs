using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dal_imp : IDAL
    {

        #region singeltone
        private Dal_imp() { }
        private static Dal_imp instance;

        public static Dal_imp Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dal_imp();
                }
                return instance;
            }
        }

        #endregion
        #region get.. by ....

        public BE.Dish getDishByNmae(string nameOfDish)
        {

            int index = DS.DataSource.dsDish.FindIndex(dish => dish.dishName == nameOfDish);
            if (index == -1)
                throw new Exception("לא קיימת מנה כזאת");
            else
                return DS.DataSource.dsDish[index];


        }

        public double getPriceOfDish(uint dishId)
        {
            int index = DS.DataSource.dsDish.FindIndex(item => item.dishID == dishId);
            if (index == -1)
                throw new Exception("אין מנה כזאת");
            else
                return DS.DataSource.dsDish[index].priceOfSingleDish;
        }

        public BE.Kosher getKosherOfDish(int dishId)
        {
            int index = DS.DataSource.dsDish.FindIndex(item => item.dishID == dishId);
            if (index == -1)
                throw new Exception("אין מנה כזאת");
            else
                return DS.DataSource.dsDish[index].dishLevelOfKosher;
        }

        public string getNameOfBranchById(uint id)
        {
            string nameOfBranch = DS.DataSource.dsBranch.Where(b => b.branchId == id).FirstOrDefault().nameOfBranch;
            if (nameOfBranch != null)
                return nameOfBranch;
            else
                throw new Exception("לא קיימת מסעדה כזאת");
        }
        public string getNameOfClientById(uint id)
        {
            string nameOfClient = DS.DataSource.dsClient.Where(c => c.idOfClient == id).FirstOrDefault().nameOfClient;
            if (nameOfClient != null)
                return nameOfClient;
            else
                throw new Exception("לא קיים לקוח כזה");
        }
        public BE.Branch getBranchByName(string nameOfbranch)
        {

            int index = DS.DataSource.dsBranch.FindIndex(b => b.nameOfBranch == nameOfbranch);
            if (index != -1) return DS.DataSource.dsBranch[index];
            else
                throw new Exception("לא קיימת מסעדה כזאת");
        }
        public BE.Client getClientIdByName(string nameOfclient)
        {

            int index = DS.DataSource.dsClient.FindIndex(c => c.nameOfClient == nameOfclient);
            if (index != -1) return DS.DataSource.dsClient[index];
            else
                throw new Exception("לא קיים לקוח כזה");
        }
        #endregion

        #region add/update/remove dish

        public void addDish(BE.Dish new_dish)
        {
            new_dish.dishID = BE.Dish.dishMaxId++;
            // הוספת מנה עם מס' זיהוי כבר תקין
            DS.DataSource.dsDish.Add(new_dish);
        }
        public void deleteDish(BE.Dish dish)
        {
            if (!DS.DataSource.dsDish.Remove(dish))
                throw new Exception("אין מנה כזאת");
        }
        public void updateDish(BE.Dish dish)
        {
            int index = DS.DataSource.dsDish.FindIndex(z => z.dishID == dish.dishID);
            if (index == -1)
                throw new Exception("אין מנה כזאת");
            DS.DataSource.dsDish[index] = dish;
        }
        #endregion


        #region add/update/remove branch

        public void addBranch(BE.Branch new_branch)
        {
            new_branch.branchId = BE.Branch.branchMaxID++;
            // הוספת סניף עם מס' זיהוי כבר תקין
            DS.DataSource.dsBranch.Add(new_branch);
        }
        public void deleteBranch(BE.Branch branch)
        {
            if (!DS.DataSource.dsBranch.Remove(branch))
                throw new Exception("אין סניף כזה");
        }

        public void updateBranch(BE.Branch branch)
        {
            int index = DS.DataSource.dsBranch.FindIndex(z => z.branchId == branch.branchId);
            if (index == -1)
                throw new Exception("אין סניף כזה");
            DS.DataSource.dsBranch[index] = branch;
        }

        #endregion

        #region add/update/remove order

        public uint addOrder(BE.Order new_order)
        {

            new_order.orderId = BE.Order.orderDefaultId++;//עדכון מספר זיהוי ההזמנה

            new_order.dateOfOrder = DateTime.Now;//עדכון תאריך ההזמנה

            DS.DataSource.dsOrder.Add(new_order);
            return new_order.orderId;//החזרת מספר ההזמנה 
        }
        public void deleteOrder(uint orderId)
        {
            if (DS.DataSource.dsOrder.RemoveAll(or => or.orderId == orderId) == -1)
                throw new Exception("אין כזו הזמנה");

            // מחיקת כל המנות המוזמנות, יכנס לכאן רק אם ההזמנה קיימת
            DS.DataSource.dsOrderedDish.RemoveAll(item => item.orderId == orderId);

        }
        public void updateOrder(BE.Order order)
        {
            int index = DS.DataSource.dsOrder.FindIndex(z => z.orderId == order.orderId);
            if (index == -1)
                throw new Exception("אין הזמנה כזאת");

            DS.DataSource.dsOrder[index] = order;//עדכון ההזמנה
        }

        #endregion


        #region add/update/remove OrderedDish

        public void addOrderedDish(BE.OrderedDish new_orderedDish)
        {
            DS.DataSource.dsOrderedDish.Add(new_orderedDish);
        }

        /// <summary>
        /// בהנתן הזמנה קיימת - מעדכן כמות של מנה מוזמנת אחת. למחיקה/הוספה של מנות יש להשתמש בפונקציות מיוחדות
        /// </summary>
        /// <param name="orderedDish"></param>
        public void deleteAllOrderedDishesByOrderId(uint orderId)
        {
            DS.DataSource.dsOrderedDish.RemoveAll(dishOrder => dishOrder.orderId == orderId);
        }
        #endregion

        #region update-remove-add client

        public void addClient(BE.Client new_client)
        {
            new_client.idOfClient = BE.Client.clientmaxID++;
            DS.DataSource.dsClient.Add(new_client);

        }
        public void deleteClient(uint clientId)
        {
            if (DS.DataSource.dsClient.RemoveAll(cl => cl.idOfClient == clientId) == -1)
                throw new Exception("אין לקוח כזה");

            // מחיקת כל ההזמנות של הקליינט, יכנס לכאן רק אם הקליינט היה קיים
            List<BE.Order> listOfOrderFromClient = new List<BE.Order>();
            listOfOrderFromClient = DS.DataSource.dsOrder.Where(order => order.clientID == clientId).ToList();//רשימת כל ההזמנות של המשתמש

            foreach (BE.Order order in listOfOrderFromClient) //מחיקת כל ההזמנות של המשתמש
                this.deleteOrder(order.orderId);   

        }
        public void updateClient(BE.Client client)
        {
            int index = DS.DataSource.dsClient.FindIndex(c => c.idOfClient == client.idOfClient);
            if (index == -1)
                throw new Exception("אין לקוח כזה");
            DS.DataSource.dsClient[index] = client;
        }

        #endregion

  




        #region get dish/order/client/kosher by id



        public List<BE.OrderedDish> getListOfOrderDishByOrderId(uint orderId)
        {
            return (from orderDish in DS.DataSource.dsOrderedDish
                    where orderDish.orderId == orderId
                    select orderDish).ToList();
        }
        public BE.Order getOrderById(uint orderId)
        {
            return DS.DataSource.dsOrder.FirstOrDefault(order => order.orderId == orderId);
        }
        public BE.Dish getDishById(uint dish_id)
        {
            return (BE.Dish) DS.DataSource.dsDish.FirstOrDefault(dish => dish.dishID == dish_id).Clone();
        }

        public BE.Client getClientByOrderId(uint orderId)
        {
            return DS.DataSource.dsClient.FirstOrDefault(client => client.idOfClient == getOrderById(orderId).clientID);
        }

        public BE.Branch getBranchByBranchId(uint branchId)
        {
            return (BE.Branch) DS.DataSource.dsBranch.FirstOrDefault(b => b.branchId == branchId).Clone();
        }
        public BE.Branch getBranchByOrderId(uint orderId)
        {
            return DS.DataSource.dsBranch.FirstOrDefault(b => b.branchId == getOrderById(orderId).branchId);
        }
        public BE.Client getClientByClientId(uint clientId)
        {
            int index = DS.DataSource.dsClient.FindIndex(c => c.idOfClient == clientId);
            if (index == -1)
                throw new Exception("לא נמצא לקוח כזה");
            else
                return (BE.Client) DS.DataSource.dsClient[index].Clone();
        }

        public BE.Kosher getKosherOfDish(uint dishId)
        {
            return DS.DataSource.dsDish.Find(dish => dish.dishID == dishId).dishLevelOfKosher;
        }

        #endregion

        #region get list from ds
        public List<BE.OrderedDish> getListOfAllDishOrders() { return DS.DataSource.dsOrderedDish; }
        public List<BE.Order> getListOfAllOrders() { return DS.DataSource.dsOrder; }
        public List<BE.Dish> getListOfAllDishes() { return DS.DataSource.dsDish; }
        public List<BE.Branch> getListOfAllBranches() { return DS.DataSource.dsBranch; }
        public List<BE.Client> getListOfAllClients() { return DS.DataSource.dsClient; }
        #endregion
    }
}
