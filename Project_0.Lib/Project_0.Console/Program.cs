using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project_0.Context.Repo;
using Project_0.Lib;
using Project_0.Lib.InterfaceRepos;
using Project0.Context;
using Project0.Context.Repositories;

namespace Project_0.Lib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Games");
            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            var options = optionsBuilder.Options;

            var dbContext = new Project0Context(options);
            IOrdersRepository OrdersRepository = new OrderRepository(dbContext);
            IStoreRepository StoreRepository = new StoreRepository(dbContext);
            ICustomerRepository CustomerRepoistory = new CustomerRepository(dbContext);
            //StartScreen(StoreRepository);
            //CustomerScreen(CustomerRepoistory);

            StoreScreen(StoreRepository);



        }

        public static void CustomerScreen(ICustomerRepository customerRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Select a customer: ");

            List<CustomerImp> ListOfCustomers = customerRepository.GetCustomers().ToList();
            for (int i = 0; i < ListOfCustomers.Count; i++)
            {
                Console.WriteLine(ListOfCustomers.Count);
                Console.WriteLine($"{ListOfCustomers[i].Id}: {ListOfCustomers[i].FirstName} {ListOfCustomers[i].LastName}");
            }
        }

        public static void StoreScreen(IStoreRepository storeRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Select a store: ");

            //int count = storeRepository.GetStores().ToList().Count;
            //Console.WriteLine(count);
            List<StoreImp> ListOfStores = storeRepository.GetStores().ToList();
            for (int i = 0; i < ListOfStores.Count; i++)
            {
                Console.WriteLine(ListOfStores.Count);
                Console.WriteLine($"{ListOfStores[i].IDNumber}: {ListOfStores[i].Location}");
            }

            var currentCustomer = new CustomerImp();
            //Console.WriteLine($"Default {ListOfStores.Count + 1 }: {storeRepository.GetStoreByLocation(currentCustomer.DefaultStoreId).Location}");
            //storeRepository.PrintStoreLocations();
        }


    }
}
