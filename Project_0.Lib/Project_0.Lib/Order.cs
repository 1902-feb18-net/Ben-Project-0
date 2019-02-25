using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class Order
    {
        Order(string item, int ID, Date Dt, string Customer, double GameCost, int Quantity, double ShipCosts)
        {
            OrderGame = item;
            OrderID = ID;
            OrderDate = Dt;
            OrderCustomer = Customer;
            OrderCost = GameCost;
            OrderQuantity = Quantity;
            ShippingCost = ShipCosts;
            Valid = true;
        }

        private string OrderGame { get; } //Game name
        private int OrderID { get; } //Item ID
        private Date OrderDate { get; } //Date ordered
        private string OrderCustomer { get; } //Customer name
        private double OrderCost { get; set; } //How expensive the game was, excluding shipping
        private int OrderQuantity { get; } //How many items were ordered
        private double ShippingCost { get; set; } //How much shipping cost total

        public bool Valid; //is the Order cancelled or not?

        private double TotalOrderCost()
        {
            //OrderCost = OrderQuantity * OrderCost + ShippingCost
            return OrderQuantity * OrderCost + ShippingCost;
        }

        public void DisplayData()
        {
            //displays order ID, Date, Game Name, Customer, cost, and whether its still active
        }

    }
}
