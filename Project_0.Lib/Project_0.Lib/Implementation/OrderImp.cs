﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class OrderImp
    {

        public OrderImp()
        {

        }

        public OrderImp(string item, int ID, DateTime Dt, int customerId, double GameCost, int Quantity, double ShipCosts, StoreImp location)
        {
            OrderGame = item;
            OrderID = ID;
            OrderDate = Dt;
            OrderCustomer = customerId;
            OrderCost = (decimal)GameCost;
            OrderQuantity = Quantity;
            ShippingCost = ShipCosts;
            StoreId = location.IDNumber;
            Valid = true;
        }

        private string OrderGame { get; set; } //Game name
        public int OrderID { get; set; } //Item ID
        public DateTime OrderDate { get; set; } //Date ordered
        public int OrderCustomer { get; set; } //Customer name
        public decimal OrderCost { get; set; } //How expensive the game was, excluding shipping
        private int OrderQuantity { get; set; }//How many items were ordered

        private double ShippingCost //How much shipping cost total
        {
            get => ShippingCost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Shipping costs must be above 0.", nameof(value));
                }
                ShippingCost = value;
            }
        }

        public int StoreId { get; set; }

        public bool Valid { get; set; } //is the Order cancelled or not?

        private double TotalOrderCost()
        {
            //OrderCost = OrderQuantity * OrderCost + ShippingCost
            return OrderQuantity * (double)OrderCost + ShippingCost;
        }

        public void DisplayData()
        {
            //displays order ID, Date, Game Name, Customer, cost, and whether its still active
        }

    }
}