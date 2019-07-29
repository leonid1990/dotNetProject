using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        #region grouping
        /// <summary>
        /// יחזיר רשימת מנות לפי תנאי שישלח בהתצוגה
        /// </summary>
        /// <param name="cond">התנאי</param>
        /// <returns></returns>
        IEnumerable<BE.groupingClass> getOrderByCond(Predicate<BE.Order> cond);

        /// <summary>
        /// יחזיר רווחים לפי כתובות
        /// </summary>
        /// <returns></returns>
        IEnumerable<BE.groupingClass> getProfitByCustomerAdress();

        /// <summary>
        /// יחזיר רווחים לפי תאריכים
        /// </summary>
        /// <returns></returns>
        IEnumerable<BE.groupingClass> getProfitByDate();
        /// <summary>
        /// יחזיר רווחים לפי סוגי מנות
        /// </summary>
        /// <returns></returns>
        IEnumerable<BE.groupingClass> getProfitByKindOfDish();

        #endregion
        #region summary details

        /// <summary>
        /// יחזיר את שם המנה הכי פופולארית
        /// </summary>
        /// <returns></returns>
         string mostPopularDish();

        /// <summary>
        /// יחזיר את שם המסעדה הכי פופולארית
        /// </summary>
        /// <returns></returns>
         string mostPopularBranch();

        /// <summary>
        /// יחזיר את שם הלקוח שקנה הכי הרבה
        /// </summary>
        /// <returns></returns>
         string mostPopularClient();

        /// <summary>
        /// יחזיר את שם המנה הכי יקרה
        /// </summary>
        /// <returns></returns>
         string mostExpensiveDish();

        /// <summary>
        /// יחזיר את הרווח הכולל מכל המכירות
        /// </summary>
        /// <returns></returns>
         double sumProfit();

        /// <summary>
        /// יחזיר את מספר המנות הכולל
        /// </summary>
        /// <returns></returns>
         int numberOfDishes();

        /// <summary>
        /// יחזיר את מספר הסניפים הכולל
        /// </summary>
        /// <returns></returns>
         int numberOfBranches();

        /// <summary>
        /// יחזיר את מספר הלקוחות הכולל
        /// </summary>
        /// <returns></returns>
         int numberOfClients();

      
        #endregion

        /// <summary>
        /// בודק אם ההזמנה חורגת מהמחיר המקסימאלי
        /// </summary>
        /// <param name="orderPrice">מחיר ההזמנה </param>
        void maxOrderPayment(double orderPrice);

        /// <summary>
        /// הוספת קליינט 
        /// </summary>
        /// <param name="client"></param>
        void addClient(BE.Client client);

        /// <summary>
        /// מחיקת קליינט(כולל מחיקת כל ההזמנות שלו בשכבת הנתונים
        /// </summary>
        /// <param name="clientId">מספר הקליינט</param>
        void deleteClient(uint clientId);

        /// <summary>
        /// עדכון הלקוח לפי מספר הזיהוי שלו ובדיקה האם השם החדש לא מופיע כבר
        /// </summary>
        /// <param name="client">האובייקט החדש</param>
        /// <param name="oldName">שם ישן</param>
        void updateClient(BE.Client client , string oldName);

        /// <summary>
        /// הוספת מנה 
        /// </summary>
        /// <param name="dish">האובייקט מנה</param>
        void addDish(BE.Dish dish);
        /// <summary>
        /// מחיקת מנה, וזריקת חריגה במידה ומוזמנת
        /// </summary>
        /// <param name="dish">המנה</param>
        void deleteDish(BE.Dish dish);

        /// <summary>
        /// עדכון מנה לפי מספר הזיהוי. ובדיקה האם השם החדש מופיע כבר
        /// </summary>
        /// <param name="dish">האובייקט החדש </param>
        /// <param name="oldName">שם המנה הקודם , על מנת לבדוק אם שינה ,לבדוק אם השם כבר מופיע</param>
        void updateDish(BE.Dish dish, string oldName);

        /// <summary>
        /// הוספת מסעדה
        /// </summary>
        /// <param name="branch">האובייקט מסעדה</param>
        void addBranch(BE.Branch branch);

        /// <summary>
        /// מחיקת מסעדה
        /// </summary>
        /// <param name="branchToDelete">אובייקט המסעדה למחיקה</param>
        void deleteBranch(BE.Branch branchToDelete);

        /// <summary>
        /// עדכון מסעדה
        /// </summary>
        /// <param name="branch">האובייקט החדש</param>
        /// <param name="oldName">שם המסעדה הישן,במידה ושינה נבדוק שהשם לא מופיע כבר</param>
        void updateBranch(BE.Branch branch ,string oldName);

        /// <summary>
        /// הוספת הזמנה
        /// </summary>
        /// <param name="order">האובייקט הזמנה</param>
        /// <returns>יחזיר את מספר ההזמנה שנוספה</returns>
        uint addOrder(BE.Order order);

        /// <summary>
        /// מחיקת הזמנה כולל מחיקת כל המנות המוזמנות שבתוכה
        /// </summary>
        /// <param name="orderId">מספר זיהוי ההזמנה לפיו נמחק </param>
        void deleteOrder(uint orderId);

        /// <summary>
        /// עדכון הזמנה לפי מספר הזיהוי
        /// </summary>
        /// <param name="order">האובייקט המעודכן</param>
        void updateOrder(BE.Order order);

        /// <summary>
        /// הוספת מנה מוזמנת להזמנה קיימת
        /// </summary>
        /// <param name="newOrderDish">האובייקט מנה מוזמנת</param>
        void addOrderedDish(BE.OrderedDish newOrderDish);

        /// <summary>
        /// מחיקת מנה מוזמנת
        /// </summary>
        /// <param name="orderId">מספר זיהוי המנה המוזמנת</param>
        void deleteAllOrderedDishesByOrderId(uint orderId);

        /// <summary>
        /// חישוב מחיר מנה מוזמנת ספציפית
        /// </summary>
        /// <param name="priceOfSingleDish">מחיר מנה בודדת</param>
        /// <param name="numberOfSameDish">כמות מאותה מנה </param>
        /// <returns>מחיר למנה מוזמנת</returns>
        double calculatePrice(float priceOfSingleDish, uint numberOfSameDish);

        /// <summary>
        /// בודק האם רמת הכשרות +הגודל הדרושים מתאימים למנה הקיימת
        /// </summary>
        /// <param name="newOrderDish">המנה המוזמנת הדרושה</param>
        /// <param name="currentDishSize">הגודל הנתון למנה הדרושה</param>
        /// <param name="currentDishKosher">הכשרות הנתונה למנה הדרושה</param>
        void checkKosherSizeLevel(BE.OrderedDish newOrderDish, BE.SizeOfDish currentDishSize, BE.Kosher currentDishKosher);

        /// <summary>
        /// החזרת רשימת מנות מוזמנות לפי מספר הזמנה
        /// </summary>
        /// <param name="orderId">מספר זיהוי המשלוח</param>
        /// <returns>רשימת המנות המוזמנות</returns>
        List<BE.OrderedDish> getListOfOrderDishByOrderId(uint orderId);

        /// <summary>
        /// החזרת שם המסעדה לפי מספר זיהוי המסעדה
        /// </summary>
        /// <param name="id">מספר זיהוי המסעדה</param>
        /// <returns>שם המסעדה</returns>
        string getNameOfBranchById(uint id);

        /// <summary>
        /// החזרת שם הלקוח לפי מספר זיהוי הלקוח
        /// </summary>
        /// <param name="id">מספר זיהוי הלקוח</param>
        /// <returns>שם הלקוח</returns>
        string getNameOfClientById(uint id);

        /// <summary>
        /// מחזיר אובייקט מנה לפי מספר זיהוי מנה
        /// </summary>
        /// <param name="dish_id">מספר זיהוי מנה</param>
        /// <returns>אובייקט מנה</returns>
        BE.Dish getDishById(uint dish_id);

        /// <summary>
        /// החזרת אובייקט מסעדה לפי מספר זיהוי מסעדה
        /// </summary>
        /// <param name="branchId">מספר זיהוי מסעדה</param>
        /// <returns>אובייקט מסעדה</returns>
        BE.Branch getBranchByBranchId(uint branchId);
        /// <summary>
        /// החזרת אובייקט מסעדה לפי שם מסעדה
        /// </summary>
        /// <param name="nameOfbranch">שם מסעדה</param>
        /// <returns>החזרת אובייקט מסעדה</returns>
        BE.Branch getBranchByName(string nameOfbranch);

        /// <summary>
        /// החזרת מספר זיהוי לקוח לפי שם לקח
        /// </summary>
        /// <param name="nameOfclient">שם לקח</param>
        /// <returns>מספר זיהוי לקוח</returns>
        BE.Client getClientIdByName(string nameOfclient);

        /// <summary>
        /// החזרת אובייקט לקוח לפי מספר זיהוי לקוח
        /// </summary>
        /// <param name="clientId">מספר זיהוי לקוח</param>
        /// <returns>אובייקט לקוח</returns>
        BE.Client getClientByClientId(uint clientId);

        /// <summary>
        /// החזרת אובייקט מנסה לפי שם מנה
        /// </summary>
        /// <param name="nameOfDish">שם מנה</param>
        /// <returns>אובייקט מנה</returns>
        BE.Dish getDishByName(string nameOfDish);

        /// <summary>
        /// החזת אובייקט הזמנה לפי מספר זיהוי הזמנה
        /// </summary>
        /// <param name="orderId">מספר זיהוי הזמנה</param>
        /// <returns>אובייקט הזמנה</returns>
        BE.Order getOrderById(uint orderId);

        /// <summary>
        /// החזרת רשימה כל המנות המוזמנות
        /// </summary>
        /// <returns>רשימת המנות המוזמנות</returns>
        List<BE.OrderedDish> getListOfDishOrders();

        /// <summary>
        /// החזרת רשימת כל ההזמנות
        /// </summary>
        /// <returns>רשימת כל ההזמנות</returns>
        List<BE.Order> getListOfAllOrders();

        /// <summary>
        /// החזרת רשימת כל המנות
        /// </summary>
        /// <returns>רשימת כל המנות</returns>
        List<BE.Dish> getListOfAllDishes();

        /// <summary>
        /// החזרת שימת כל המסעדות
        /// </summary>
        /// <returns>רשימת כל המסעדות</returns>
        List<BE.Branch> getListOfAllBranches();

        /// <summary>
        /// החזרת רשימת כל הלקוחות
        /// </summary>
        /// <returns>רשימת כל הלקוחות</returns>
        List<BE.Client> getListOfAllClient();

    } 
}

