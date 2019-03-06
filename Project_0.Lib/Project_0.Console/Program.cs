using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project_0.Context.Repo;
using Project_0.Lib;
using Project_0.Lib.InterfaceRepos;
using Project0.Context;
using Project0.Context.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging.Console;

namespace Project_0.Lib
{
    class Program
    {
        public static readonly LoggerFactory AppLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        static void Main(string[] args)
        {
            Console.WriteLine("Order Games");
            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            var dbContext = new Project0Context(options);
            IOrdersRepository OrdersRepository = new OrderRepository(dbContext);
            IStoreRepository StoreRepository = new StoreRepository(dbContext);
            ICustomerRepository CustomerRepository = new CustomerRepository(dbContext);
            IGamesRepository GamesRepository = new GamesRepository(dbContext);

            //Display customer screen and select customer, then pass that customer onto the selected store
            //CustomerScreen(CustomerRepository);
            int customerSelection = 0;
            int customerOptionSelect = 0;
            string name = "";
            bool moreGamesToBuy = true;
            //Display select store screen, or go back to customer selection
            //StoreScreen(StoreRepository);
            int storeSelection; 

            CustomerSelect:
            storeSelection = StoreRepository.GetStores().ToList().Count + 1;

            Console.WriteLine();
            Console.WriteLine("Press 1 to view all customers.  \nPress 2 to search for a customer by name.");
            Console.WriteLine($"Press 3 to view statistics.");

            customerOptionSelect = Convert.ToInt32(Console.ReadLine());

            if (customerOptionSelect == 1)
            {
                CustomerScreen(CustomerRepository);
                customerSelection = Convert.ToInt32(Console.ReadLine());
                if (!CustomerRepository.IsValidId(customerSelection))
                {
                    Console.WriteLine("Please input a valid customer ID");
                    goto CustomerSelect;
                }
            }
            else if (customerOptionSelect == 2)
            {
                Console.WriteLine();
                Console.WriteLine("Enter a full name: ");
                name = Console.ReadLine();
                //storeSelection = StoreRepository.GetStores().ToList().Count + 2;
            }
            else if (customerOptionSelect == 3)
            {
                int popId = OrdersRepository.GetMostPopularGame();
                Console.WriteLine();
                Console.WriteLine($"The most popular game is {GamesRepository.GetGameById(popId)}");
                goto CustomerSelect;
            }
            else
            {
                Console.WriteLine("Please enter a valid input.");
                goto CustomerSelect;
            }

            StoreScreen(StoreRepository);
            storeSelection = Convert.ToInt32(Console.ReadLine());

            CustomerImp selectedCustomer;
            StoreImp selectedStore;// = StoreRepository.GetStoreByLocation(storeSelection);
            List<CustomerImp> ListOfCustomers = CustomerRepository.GetCustomers().ToList();

            if (customerOptionSelect == 1)
                selectedCustomer = CustomerRepository.GetCustomerById(customerSelection); //Get the chosen customer by ID
            else if (customerOptionSelect == 2)
                selectedCustomer = CustomerRepository.GetCustomerByName(name); //Get the chosen customer by name
            else
                goto CustomerSelect;

            if (storeSelection == StoreRepository.GetStores().ToList().Count + 2) //if user chooses to view all order by selected cust
            {
                if (selectedCustomer.OrdersByCustomer.Count < 1) //if there are no orders on file for customer, go to cust select
                {
                    Console.WriteLine("Customer has no orders on file.");
                    goto CustomerSelect;
                }
                List<OrderImp> OrdersByCustomer = OrdersRepository.GetAllOrdersByCustomer(selectedCustomer.Id).ToList();
                for (int i = 0; i < selectedCustomer.OrdersByCustomer.Count; i++) //views all orders by customer
                {
                    ViewOrderDetails(OrdersByCustomer[i], StoreRepository);
                }
                Console.WriteLine("Press 1 to return to customer select.\nPress 2 to view list of stores.");
                int temp = Convert.ToInt32(Console.ReadLine());
                if (temp == 1)
                {
                    goto CustomerSelect;
                }
                else if (temp == 2)
                {
                    StoreScreen(StoreRepository);
                    storeSelection = Convert.ToInt32(Console.ReadLine());
                    goto StoreSelect;
                }
                else
                {
                    Console.WriteLine("Invalid input, redirecting to customer select.");
                    goto CustomerSelect;
                }
            }
            else if (storeSelection == StoreRepository.GetStores().ToList().Count + 1)
            {
                goto CustomerSelect;
            }

            StoreSelect:
            selectedStore = StoreRepository.GetStoreByLocation(storeSelection); //Get the chosen store

            OptionSelect:
            OptionsScreen(); //View options to handle or add orders
            int optionSelect = Convert.ToInt32(Console.ReadLine());
            int gameSelect = 0;
            int viewSelect = 0;
            if (optionSelect == 1) //view all orders at this store
            {
                ViewSelect:
                ViewOrdersScreen();
                viewSelect = Convert.ToInt32(Console.ReadLine());
                if (viewSelect == 1)
                {
                    List<OrderImp> OrderList = OrdersRepository.GetOrderByDate(selectedStore).ToList();
                    if (OrderList.Count < 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("There are no orders at this store.");
                        goto OptionSelect;
                    }
                    ViewAllOrdersInList(OrderList, StoreRepository);
                }
                else if (viewSelect == 2)
                {
                    List<OrderImp> OrderList = OrdersRepository.GetOrderByDateReverse(selectedStore).ToList();
                    if (OrderList.Count < 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("There are no orders at this store.");
                        goto OptionSelect;
                    }
                    ViewAllOrdersInList(OrderList, StoreRepository);
                }
                else if (viewSelect == 3)
                {
                    List<OrderImp> OrderList = OrdersRepository.GetOrderByCost(selectedStore).ToList();
                    if (OrderList.Count < 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("There are no orders at this store.");
                        goto OptionSelect;
                    }
                    ViewAllOrdersInList(OrderList, StoreRepository);
                }
                else if (viewSelect == 4)
                {
                    List<OrderImp> OrderList = OrdersRepository.GetOrderByCostReverse(selectedStore).ToList();
                    if (OrderList.Count < 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("There are no orders at this store.");
                        goto OptionSelect;
                    }
                    ViewAllOrdersInList(OrderList, StoreRepository);
                }
                else if (viewSelect == 5)
                {
                    goto OptionSelect;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input.");
                    goto ViewSelect;
                }
            }
            else if (optionSelect == 2) //Place an order at selected store
            {
                if (!selectedStore.CheckIfOrderIsReady(selectedCustomer))
                {
                    Console.WriteLine();
                    Console.WriteLine($"You have ordered from this story within the last 2 hours, " +
                        $"please pick a different option");
                    goto OptionSelect;
                }
                OrderImp newOrder = new OrderImp();
                OrderGamesImp newGames;
                while (moreGamesToBuy)
                {
                    newGames = new OrderGamesImp();
                    GameSelection:
                    PlaceOrderScreen(GamesRepository, selectedCustomer);
                    gameSelect = Convert.ToInt32(Console.ReadLine());

                    if (GamesRepository.GetGameById(gameSelect) == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter valid ID number");
                        goto GameSelection;
                    }

                    GamesImp selectedGame = GamesRepository.GetGameById(gameSelect);
                    selectedCustomer.LastGameBoughtId = selectedGame.Id;

                    EditionSelect:
                    Console.WriteLine();
                    Console.WriteLine("Enter the game edition: ");
                    Console.WriteLine($"1. Standard edition: {selectedGame.Cost}");
                    Console.WriteLine($"2. Advanced edition: {selectedGame.AdvancedCost}");
                    Console.WriteLine($"3. Deluxe edition: {selectedGame.AdvancedCost + 10}");
                    int selectedEdition = Convert.ToInt32(Console.ReadLine());

                    if (selectedEdition != 1 && selectedEdition != 2 && selectedEdition != 3)
                    {
                        Console.WriteLine("Please enter a valid edition number.");
                        goto GameSelection;
                    }
                    string editionName = "";
                    if (selectedEdition == 1)
                    {
                        editionName = "Standard";
                    }
                    else if (selectedEdition == 2)
                    {
                        editionName = "Advanced";
                    }
                    else
                    {
                        editionName = "Deluxe";
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Enter the number of {selectedGame.Name}, {editionName} Edition");
                    int quantityOfGame = Convert.ToInt32(Console.ReadLine());

                    StoreRepository.RemoveFromStock(quantityOfGame, selectedGame, selectedStore);
                    if (editionName == "Deluxe")
                    {
                        if (selectedStore.DeluxeInStock < quantityOfGame)
                        {
                            Console.WriteLine("Not enough deluxe in stock, choose another edition.");
                            goto EditionSelect;
                        }
                        else
                            StoreRepository.RemoveDeluxeFromStock(quantityOfGame, selectedStore.IDNumber);
                    }

                    //Assign the game purchases' statistics
                    newGames.Game = selectedGame;
                    newGames.GameId = selectedGame.Id;
                    newGames.Edition = selectedEdition;
                    newGames.GameQuantity = quantityOfGame;
                    newGames.OrderId = 100;

                    newOrder.GamesInOrder.Add(newGames);

                    AddGameChoice:
                    Console.WriteLine("Do you want to add another game?\nPress 1 to add another game\nPress 2 to finish order.");
                    int nextAction = Convert.ToInt32(Console.ReadLine());
                    if (nextAction == 1) { }
                    else if (nextAction == 2)
                        moreGamesToBuy = false;
                    else
                    {
                        Console.WriteLine("Enter a valid input.");
                        goto AddGameChoice;
                    }
                        

                    //OrderImp newOrder = new OrderImp(DateTime.Now, selectedCustomer.Id, selectedStore.ShippingCosts, selectedStore);
                }
                newOrder.OrderDate = DateTime.Now;
                newOrder.OrderCustomer = selectedCustomer.Id;
                newOrder.StoreId = selectedStore.IDNumber;
                OrdersRepository.AddOrder(newOrder); //Adds the new order to database
                Console.WriteLine("newOrder's OrderId = " + newOrder.OrderID);
                for (int i = 0; i < newOrder.GamesInOrder.Count; i++) //adds all new OrderGames to database and assigns their OrderId
                {
                    newOrder.GamesInOrder[i].OrderId = OrdersRepository.GetExactOrderByDate(newOrder.OrderDate).OrderID;
                    OrdersRepository.AddOrderItem(newOrder.GamesInOrder[i]);
                }
            }
            else if (optionSelect == 3) //go back to store selection screen
            {
                StoreScreen(StoreRepository);
                storeSelection = Convert.ToInt32(Console.ReadLine());
                goto StoreSelect;
            }
            else //invalid input
            {
                Console.WriteLine("Please enter a valid input.");
                goto OptionSelect;
            }

            FinalMenu();
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    StoreScreen(StoreRepository);
                    storeSelection = Convert.ToInt32(Console.ReadLine());
                    goto StoreSelect;
                case 2:
                    goto CustomerSelect;
                default:
                    break;
            }





            Console.ReadLine();
        }

        public static void CustomerScreen(ICustomerRepository customerRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Select a customer: ");

            List<CustomerImp> ListOfCustomers = customerRepository.GetCustomers().ToList();
            for (int i = 0; i < ListOfCustomers.Count; i++)
            {
                Console.WriteLine($"{ListOfCustomers[i].Id}: {ListOfCustomers[i].FirstName} {ListOfCustomers[i].LastName}");
            }
        }

        public static void StoreScreen(IStoreRepository storeRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Select a store: ");

            List<StoreImp> ListOfStores = storeRepository.GetStores().ToList();
            for (int i = 0; i < ListOfStores.Count; i++)
            {
                Console.WriteLine($"{ListOfStores[i].IDNumber}: {ListOfStores[i].Location}");
            }
            Console.WriteLine($"Press {ListOfStores.Count + 1} to return to customer selection");
            Console.WriteLine($"Press {ListOfStores.Count + 2} to view all orders by current customer.");

        }

        public static void OptionsScreen()
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to view orders\nPress 2 to place an order\nPress 3 to choose another store.");
        }

        public static void ViewOrdersScreen()
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to view all orders from least to most recent." +
                "\nPress 2 to view all orders from most to least recent." +
                "\nPress 3 to view all order from most to least expensive." +
                "\nPress 4 to view all orders from least to most expensive." +
                //"\nPress 4 to search for an order by a specific ID." +
                "\nPress 5 to return to options screen.");
        }

        public static void ViewOrderDetails(OrderImp order, IStoreRepository storeRepository)
        {
            Console.WriteLine();
            Console.WriteLine($"Order ID: {order.OrderID},\n Customer ID: {order.OrderCustomer},\n " +
                $"Order Cost: {order.OrderCost},\n Order Date: {order.OrderDate}" +
                $"\n Order Location: {storeRepository.GetStoreByLocation(order.StoreId).Location}");
            //Console.WriteLine("List of games in purchase: ");
            //for (int i = 0; i < order.GamesInOrder.Count; i++)
            //{
            //    Console.WriteLine(order.GamesInOrder[i].Game.Name);
            //}
        }

        public static void ViewAllOrdersInList(List <OrderImp> orderList, IStoreRepository storeRepository)
        {
            for (int i = 0; i < orderList.Count; i++)
            {
                ViewOrderDetails(orderList[i], storeRepository);
            }
        }

        public static void PlaceOrderScreen(IGamesRepository gamesRepository, CustomerImp customer)
        {
            Console.WriteLine();
            Console.WriteLine("Select a game to add to purchase.");
            List<GamesImp> ListOfGames = gamesRepository.GetAllGames().ToList();
            if (customer.LastGameBoughtId + 1 > ListOfGames.Count)
                customer.LastGameBoughtId = 0;
            for (int i = 0; i < ListOfGames.Count; i++)
            {
                if (ListOfGames[i].Id == customer.LastGameBoughtId + 1)
                    Console.WriteLine($"{ListOfGames[i].Id}: {ListOfGames[i].Name} (recommended)");
                else
                    Console.WriteLine($"{ListOfGames[i].Id}: {ListOfGames[i].Name}");
            }
            customer.LastGameBoughtId++;
        }

        public static void FinalMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to go back to the store select menu.\n" +
                "Press 2 to go back to the customer select menu.\n" +
                "Press 3 to quit.");

        }
    }
}
