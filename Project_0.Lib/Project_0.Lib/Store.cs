using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class Store
    {
        private string Location { get; set; } //the location of store???
        public double Distance { get; set; } //player enters distance in miles to help calc shipping costs
        private double ShippingCosts { get; set; } //distance is multiplied by some num for costs
        private int IDNumber { get; set; } //ID numbers start at 1 and increment for every order
        public Date OrderDate { get; set; } //Player inputs date, can be used to help find Orders
        public string Customer { get; set; } //Player inputs customers name, can be used to find Orders

        public bool LastBoughtGame { get; set; } //true indicates last bought game was Isekai, false indicates Shonen

        public double TotalCost { get; } //the total cost of all items bought by customer

        public double GetGameCost(Games game, string edition)
        {
            //depending on game edition, return the correct price
            return 0;
        }

        public Order OrderItem(string item, Games game, string edition, int quantity, double shippingCost)
        {
            //reference to Order class
            //add Order to OrderRepository

            return new Order(item, IDNumber, OrderDate, Customer, GetGameCost(game, edition), quantity, shippingCost)

            //While multiple of the same game can be ordered, they must be theh same game and the same types, no "1 standard and 1 advanced" or "1 shonen standard and 1 isekai standard"
        }

        public bool RecommendedGame()
        {
            //if the last bought game was Isekai, return false (Shonen)
            //if the last bought game was Shonen, return true (Isekai)
            //will bring up advanced edition as default
            return false;
        }

    }
}
