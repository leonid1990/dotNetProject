using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Console
{
    enum Menu
    { ADD_DISH = 1, ADD_BRANCH, ADD_ORDER,
      DELETE_DISH, DELETE_BRANCH, DELETE_ORDER,
      UPDATE_DISH, UPDTATE_BRANCH, UPDATE_ORDER,
        EXIT = 0
    };
    class Program
    {
        static void Main(string[] args)
        {
            BL.IBL myBl = BL.FactoryBL.getBL();

            BE.Branch branch = new BE.Branch();
            BE.Client client = new BE.Client();
            BE.Dish dish = new BE.Dish();
            BE.Order order = new BE.Order();
            BE.OrderedDish orderedDish = new BE.OrderedDish();

            Console.WriteLine("Hello, this is a check");
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            int choice;
            while (true)
            {
                Console.WriteLine("enter your choice:");
                input = Console.ReadKey();
                choice = int.Parse(input.KeyChar.ToString());

                switch (choice)
                {
                    case 0:
                        break;
                    case 3:
                        try
                        {
                            branch = init_branch(10,"g. mordechai", "vaad leumi", 123, "moshe", 12, 20, BE.Kosher.GLAT);
                            order = init_order(1, DateTime.Now, 2, 3, false);
                            myBl.addOrder(order);
                            orderedDish = init_order_dish(1, 2, 3, BE.Kosher.GLAT, BE.SizeOfDish.LARGE);
                            myBl.addOrderedDish(orderedDish, order, branch);
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        break;
                    default:
                        Console.WriteLine("error");
                        break;
                }

            }

            
        }
       
        static BE.Order init_order(uint order_id, DateTime date, uint branch_id, uint client_id, bool courier_needed)
        {
            BE.Order order = new BE.Order();
            order.orderId = order_id;
            order.dateOfOrder = date;
            order.branchId = branch_id;
            order.clientID = client_id;
            order.CourierNeeded = courier_needed;

            return order;
        }
        static BE.OrderedDish init_order_dish(uint order_id, uint dish_id, uint number_of_same_dish, BE.Kosher level, BE.SizeOfDish size)
        {
            BE.OrderedDish ordered_dish= new BE.OrderedDish();
            ordered_dish.orderId = order_id;
            ordered_dish.dishID = dish_id;
            ordered_dish.numberOfSameDish = number_of_same_dish;
            ordered_dish.dishLevelOfKosher = level;
            ordered_dish.dishSize = size;

            return ordered_dish;
        }
        static BE.Branch init_branch(uint id, string name, string adress, string phone_number, string responsible, ushort workers, ushort free_couriers, BE.Kosher level)
        {
            BE.Branch branch = new BE.Branch();
            branch.branchId = id;
            branch.nameOfBranch = name;
            branch.phoneNumberOfBranch = phone_number;
            branch.nameOfResponsible = responsible;
            branch.numberOfWorkers = workers;
            branch.numberOfFreeCouriers = free_couriers;
            branch.branchLevelOfKosher = level;

            return branch;
        }
    }
}
