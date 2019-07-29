using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        /// <summary>
        /// הוספת מנה
        /// </summary>
        /// <param name="new_dish">אובייקט מנה</param>
        void addDish(BE.Dish new_dish); 
        /// <summary>
        /// מחיקת מנה
        /// </summary>
        /// <param name="dish">אובייקט מנה</param>
        void deleteDish(BE.Dish dish);  
        /// <summary>
        /// עדכון מנה
        /// </summary>
        /// <param name="dish"> אובייקט מנה מעודכנת</param>
        void updateDish(BE.Dish dish);  
        /// <summary>
        /// הוספת סניף
        /// </summary>
        /// <param name="new_branch">אובייקט סניף מעודכן</param>
        void addBranch(BE.Branch new_branch);   
        /// <summary>
        /// מחיקת סניף
        /// </summary>
        /// <param name="branch">אובייקט סניף</param>
        void deleteBranch(BE.Branch branch);
        /// <summary>
        /// עדכון סניף
        /// </summary>
        /// <param name="branch">אובייקט סניף</param>
        void updateBranch(BE.Branch branch);  
        /// <summary>
        /// הוספת הזמנה
        /// </summary>
        /// <param name="new_order"></param>
        /// <returns>אובייקט הזמנה מעודכנת</returns>

        uint addOrder(BE.Order new_order);  
        /// <summary>
        /// מחיקת הזמנה
        /// </summary>
        /// <param name="orderId">אובייקט הזמנה</param>
        void deleteOrder(uint orderId); 
        /// <summary>
        /// עדכון הזמנה
        /// </summary>
        /// <param name="order">מס' הזמנה</param>
        void updateOrder(BE.Order order);   
        /// <summary>
        /// הוספת הזמנה מנה מוזמנת
        /// </summary>
        /// <param name="new_orderedDish">אובייקט מנה מוזמנת מעודכנת</param>

        void addOrderedDish(BE.OrderedDish new_orderedDish);   
        /// <summary>
        /// מחיקת כל המנות המוזמנות של הזמנה מסויימת. שימושי כאשר מוחקים הזמנה או לקוח
        /// </summary>
        /// <param name="orderId">מס' הזמנה</param>
        void deleteAllOrderedDishesByOrderId(uint orderId);    
        /// <summary>
        /// הוספת לקוח
        /// </summary>
        /// <param name="new_client">אובייקט לקוח</param>
        void addClient(BE.Client new_client);  
        /// <summary>
        /// מחיקת לקוח
        /// </summary>
        /// <param name="clientId">מס' לקוח</param>
        void deleteClient(uint clientId);   
        /// <summary>
        /// עדכון לקוח
        /// </summary>
        /// <param name="client">אובייקט לקוח מעודכן</param>
        void updateClient(BE.Client client);    
        /// <summary>
        /// שם סניף לפי מס' זיהוי
        /// </summary>
        /// <param name="id">מס' סניף</param>
        /// <returns>שם הסניף</returns>
        string getNameOfBranchById(uint id);    
        /// <summary>
        /// שם לקוח לפי מס' זיהוי
        /// </summary>
        /// <param name="id">מס' לקוח</param>
        /// <returns>שם לקוח</returns>
        string getNameOfClientById(uint id);    
        /// <summary>
        /// סניף לפי שם
        /// </summary>
        /// <param name="name">שם סניף</param>
        /// <returns>אובייקט סניף</returns>
        BE.Branch getBranchByName(string name); 
        /// <summary>
        /// לקוח לפי שם
        /// </summary>
        /// <param name="name">שם לקוח</param>
        /// <returns>אובייקט לקוח</returns>
        BE.Client getClientIdByName(string name);   
        /// <summary>
        /// מנה לפי מס' זיהוי
        /// </summary>
        /// <param name="dish_id">מס' מנה</param>
        /// <returns>אובייקט מנה</returns>
        BE.Dish getDishById(uint dish_id);  
        /// <summary>
        /// מנה לפי שם
        /// </summary>
        /// <param name="nameOfDish"></param>
        /// <returns>אובייקט מנה</returns>
        BE.Dish getDishByNmae(string nameOfDish); 
        /// <summary>
        /// רמת הכשרות של המנה
        /// </summary>
        /// <param name="id">הכשר המנה</param>
        /// <returns>מס' מנה</returns>
        BE.Kosher getKosherOfDish(uint id); 
        /// <summary>
        /// לקוח לפי מס' הזמנה
        /// </summary>
        /// <param name="orderId">מס' הזמנה</param>
        /// <returns></returns>

        BE.Client getClientByOrderId(uint orderId); 
        /// <summary>
        /// לקוח לפי מס' זיהוי
        /// </summary>
        /// <param name="clientId">מס' לקוח</param>
        /// <returns>אובייקט לקוח</returns>
        BE.Client getClientByClientId(uint clientId);   
        /// <summary>
        /// הזמנה לפי מס' זיהוי
        /// </summary>
        /// <param name="id">מס' הזמנה</param>
        /// <returns>אובייקט הזמנה</returns>

        BE.Order getOrderById(uint id); 
        /// <summary>
        /// סניף לפי מס' זיהוי
        /// </summary>
        /// <param name="orderId">מס' הזמנה</param>
        /// <returns>אובייקט סניף</returns>
        BE.Branch getBranchByOrderId(uint orderId); 
        /// <summary>
        /// מחיר מנה
        /// </summary>
        /// <param name="id">מס' מנה</param>
        /// <returns>מחיר מנה</returns>

        double getPriceOfDish(uint id); 
        /// <summary>
        /// רשימת כל המנות המוזמנות של הזמנה ספציפית
        /// </summary>
        /// <param name="id">מס' הזמנה</param>
        /// <returns>כל המנות המוזמנות של ההזמנה</returns>
        List<BE.OrderedDish> getListOfOrderDishByOrderId(uint id);  
        /// <summary>
        /// סניף לפי מס' זיהוי
        /// </summary>
        /// <param name="id">מס' סניף</param>
        /// <returns>אובייקט סניף</returns>
        BE.Branch getBranchByBranchId(uint id); 
        /// <summary>
        /// רשימת כל המנות-המוזמנות
        /// </summary>
        /// <returns>רשימת כל המנות-המוזמנות</returns>
        List<BE.OrderedDish> getListOfAllDishOrders(); 
        /// <summary>
        /// רשימת כל ההזמנות
        /// </summary>
        /// <returns>רשימת כל ההזמנות</returns>
        List<BE.Order> getListOfAllOrders();  
        /// <summary>
        /// רשימת כל המנות
        /// </summary>
        /// <returns>רשימת כל המנות</returns>
        List<BE.Dish> getListOfAllDishes(); 
        /// <summary>
        /// רשימת כל הסניפים
        /// </summary>
        /// <returns>רשימת כל הסניפים</returns>
        List<BE.Branch> getListOfAllBranches(); 
        /// <summary>
        /// רשימת כל הלקוחות
        /// </summary>
        /// <returns>רשימת כל הלקוחות</returns>
        List<BE.Client> getListOfAllClients();  



    }
}
