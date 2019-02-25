using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class Inventory
    {
        private int IsekaiInStock { get; set; } //A hidden number that limits Isekai orders
        private int ShonenAdventureInStock { get; set; } //A hidden number that limits Shonen orders

        private int DeluxeInStock { get; set; } //A hidden number that limits Delux orders

        public void RemoveFromStock(int number, string item, bool delux)
        {
            //Runs when order is formed, includes item and possible delux objects
            //Checks if delux is true, if so, remove from DeluxInStock as well
            //If anything being removed doesn't have any more stock available, throw error message
        }

        public void AddToStock(int number, string item, bool delux)
        {
            //Runs when order is cancelled or stock is refilled
        }

        public void RestockAll()
        {
            //Sets all stocks back to full
        }
    }
}
