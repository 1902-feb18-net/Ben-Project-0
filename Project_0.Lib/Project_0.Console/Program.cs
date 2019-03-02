using System;
using System.Collections.Generic;
using Project_0.Lib;

namespace Project_0.Lib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Games");


        }

        public static void StartScreen(StoreRepository storeRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Select a store: ");
            storeRepository.PrintStoreLocations();
        }


    }
}
