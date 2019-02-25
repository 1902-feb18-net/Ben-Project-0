using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class OrdersRepository
    {

        private readonly ICollection<Order> _Data;

        public Order GetOrderByID(int ID)
        {
            //goes through _Data to find order with specified ID
            //returns null if ID doesn't exist
        }

        public List<Order> GetOrderByDate(string Date)
        {
            //goes through _Data to find order(s) at specified Date
            //returns all Orders at that date, organized by ID
            //returns null if Date doesn't exist
        }

        public List<Order> GetOrderByCustomer(string Customer)
        {
            //goes through _Data to find order(s) by specified Customer
            //returns all Orders by that Customer, organized by ID
            //returns null if customer doesn't exist
        }

        public List<Order> RemoveOrder(Order order)
        {
            //Removes the relevant order from the order list
            //Order toDeactivate = order;
            //toDeactive.Valid = false;
        }

        public List<Order> AddOrder(Order order)
        {
            //_Data.Add(order);
        }
    }
}
