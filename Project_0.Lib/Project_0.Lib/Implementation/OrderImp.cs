using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class OrderImp
    {

        public OrderImp()
        {
            GamesInOrder = new List<OrderGamesImp>();
        }

        public OrderImp(DateTime Dt, int customerId, decimal ShipCosts, StoreImp location)
        {
            //OrderGame = item;
            //OrderID = ID;
            OrderDate = Dt;
            OrderCustomer = customerId;
            //OrderCost = (decimal)GameCost;
            //OrderQuantity = Quantity;
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

        private decimal ShippingCost //How much shipping cost total
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

        public List<OrderGamesImp> GamesInOrder;

        public decimal TotalOrderCost()
        {
            OrderCost = 000.00m;
            for (int i = 0; i < GamesInOrder.Count; i++)
            {
                OrderCost += GamesInOrder[i].GetCostOfPurchase();
            }
            return OrderCost;
        }

        public void DisplayData()
        {
            //displays order ID, Date, Game Name, Customer, cost, and whether its still active
        }

    }
}
