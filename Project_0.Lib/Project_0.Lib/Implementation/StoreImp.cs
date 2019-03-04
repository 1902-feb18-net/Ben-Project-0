using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class StoreImp
    {
        public StoreImp()
        {

        }

        public StoreImp(string Local)
        {
            Items = new InventoryImp();
            Location = Local;
        }

        public string Location //the location of store???
        {
            get => Location;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Location name must not be empty", nameof(value));
                }
                Location = value;
            }
        }

        public double Distance //player enters distance in miles to help calc shipping costs

        {
            get => Distance;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Distance cannot be negative", nameof(value));
                }
                Distance = value;
            }
        }

        public decimal ShippingCosts //distance is multiplied by some num for costs
        {
            get => ShippingCosts;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Distance cannot be negative", nameof(value));
                }
                ShippingCosts = value;
            }
        } 

        public int IDNumber { get; set; } //ID numbers start at 1 and increment for every order
        public DateTime OrderDate { get; set; } //Player inputs date, can be used to help find Orders
        public CustomerImp Cust { get; set; } //Player inputs customers name, can be used to find Orders

        public InventoryImp Items { get; set; } //the store's inventory class
        public int DeluxeInStock { get; set; } //A hidden number that limits Delux orders

        public bool LastBoughtGameIsIsekai { get; set; } //true indicates last bought game was Isekai, false indicates Shonen

        public double TotalCost { get; } //the total cost of all items bought by customer

        //public decimal GetGameCost(GamesImp game, string edition)
        //{
        //    //depending on game edition, return the correct price
        //    if (edition == "Base")
        //        return game.Cost;
        //    else if (edition == "Advanced")
        //        return game.AdvancedCost;
        //    else if (edition == "Deluxe") 
        //        return game.DeluxeCost;
        //    else
        //        return 0;
        //}

        public OrderImp OrderItem(string item, GamesImp game, string edition, int quantity, double shippingCost)
        {
            //reference to Order class
            //add Order to OrderRepository
            bool delux = false;
            if (edition == "Deluxe")
                delux = true;

            if (Items.CheckStock(quantity, game, delux))
            {
                Items.RemoveFromStock(quantity, game, delux);
                return new OrderImp(IDNumber, OrderDate, Cust.Id, shippingCost, shippingCost, this);
            }
            else
            {
                throw new ArgumentException("There isn't enough inventory left for that item");
            }

            //While multiple of the same game can be ordered, they must be the same game and the same types, no "1 standard and 1 advanced" or "1 shonen standard and 1 isekai standard"
        }

        public bool RecommendedGame()
        {
            //if the last bought game was Isekai, return false (Shonen)
            //if the last bought game was Shonen, return true (Isekai)
            //will bring up advanced edition as default
            if (LastBoughtGameIsIsekai)
                return true;
            else
                return false;
        }

    }
}
