using Project_0.Context.Repo;
using Project0.Context;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace Project_0.Lib
{
    public class OrderTest
    {
        readonly OrderImp orders = new OrderImp();
        private readonly Project0Context _db;

        [Fact]
        public void OrderIdIsAssignedCorrectly()
        {
            const int testId = 1;
            orders.OrderID = testId;
            Assert.Equal(orders.OrderID, testId);
        }

        [Fact]
        public void OrderDateIsAssignedCorrectly()
        {
            DateTime dateTime = DateTime.Now;
            orders.OrderDate = dateTime;

            Assert.Equal(dateTime, orders.OrderDate);
        }

        [Fact]
        public void OrderCustomerIdIsAssignedCorrectly()
        {
            const int testId = 1;
            orders.OrderCustomer = testId;
            Assert.Equal(orders.OrderCustomer, testId);
        }

        [Fact]
        public void OrdersReturnNullWhenThereAreNone()
        {
            var repo = new OrdersRepository(_db);
        }

        [Fact]
        public void test3()
        {

        }

        [Fact]
        public void test4()
        {

        }

        [Fact]
        public void Add_writes_to_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<Project0Context>().UseSqlite(connection).Options;

                // Create the schema in the database
                using (var context = new Project0Context(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new Project0Context(options))
                {
                    var orders = new OrdersRepository(context);

                    OrderImp _order = new OrderImp(1, DateTime.Now, 1, 19.99, 0.00, new StoreImp("San Francisco"));
                    OrderImp _order2 = new OrderImp(4, DateTime.Now, 1, 19.99, 0.00, new StoreImp("San Francisco"));
                    OrderGamesImp _orderGame = new OrderGamesImp(1, 2, 5);

                    orders.AddOrder(_order);
                    orders.AddOrder(_order2);
                    orders.AddOrderItem(_orderGame.OrderId, _orderGame.GameId, _orderGame.GameQuantity);

                    //Tests add order
                    Assert.Equal(_order.OrderID, context.Orders.Find(1).OrderId);
                    Assert.Equal(_order2.OrderID, context.Orders.Find(4).OrderId);

                    //Tests add orderItem
                    Assert.Equal(_orderGame.OrderId, context.OrderGames.Find(1, 2).OrderId);
                    Assert.Equal(_orderGame.GameId, context.OrderGames.Find(1, 2).GameId);
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new Project0Context(options))
                {
                    OrderImp _order = new OrderImp(1, DateTime.Now, 1, 19.99, 0.00, new StoreImp("San Francisco"));
                    OrderImp _order2 = new OrderImp(4, DateTime.Now, 1, 19.99, 0.00, new StoreImp("San Francisco"));

                    Assert.Equal(_order.OrderID, context.Orders.Find(1).OrderId);
                    Assert.Equal(_order2.OrderID, context.Orders.Find(4).OrderId);
                }
            }
            finally
            {
                connection.Close();
            }
        }


        [Fact]
        public void Test_Find_By_Functions()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            IEnumerable<OrderImp> returnIEnumerable(Project0Context contxt)
            {
                foreach (var item in contxt.Orders)
                {
                    yield return Mapper.Map(item);
                }
            }

            try
            {
                var options = new DbContextOptionsBuilder<Project0Context>().UseSqlite(connection).Options;

                // Create the schema in the database
                using (var context = new Project0Context(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new Project0Context(options))
                {
                    var orders = new OrdersRepository(context);

                    //Testing getting order by ID
                    OrderImp _order = new OrderImp(1, DateTime.Now, 1, 29.99, 0.00, new StoreImp("San Francisco"));
                    OrderImp _order2 = new OrderImp(4, DateTime.Now, 1, 19.99, 0.00, new StoreImp("San Francisco"));

                    orders.AddOrder(_order);
                    orders.AddOrder(_order2);

                    Assert.Equal(_order, orders.GetOrderByID(_order.OrderID));
                    Assert.Equal(_order2, orders.GetOrderByID(_order2.OrderID));

                    //Tests getting order by Date
                    List<OrderImp> testList1 = returnIEnumerable(context).ToList();
                    List<OrderImp> testList2 = orders.GetOrderByDate().ToList();

                    Assert.Equal(testList1.Count, testList2.Count);

                    for (int i = 0; i < testList1.Count; i++)
                    {
                        Assert.Equal(testList1[i].OrderID, testList2[i].OrderID);
                        Assert.Equal(testList1[i].OrderCost, testList2[i].OrderCost);
                        Assert.Equal(testList1[i].OrderDate, testList2[i].OrderDate);
                    }

                    //Tests getting order by Date reversed
                    testList2 = orders.GetOrderByDateReverse().ToList();
                    testList1.Reverse();

                    for (int i = 0; i < testList1.Count; i++)
                    {
                        Assert.Equal(testList1[i].OrderID, testList2[i].OrderID);
                        Assert.Equal(testList1[i].OrderCost, testList2[i].OrderCost);
                        Assert.Equal(testList1[i].OrderDate, testList2[i].OrderDate);
                    }

                    //Test getting order by Cost
                    testList2 = orders.GetOrderByCost().ToList();

                    for (int i = 0; i < testList1.Count; i++)
                    {
                        Assert.Equal(testList1[i].OrderID, testList2[i].OrderID);
                        Assert.Equal(testList1[i].OrderCost, testList2[i].OrderCost);
                        Assert.Equal(testList1[i].OrderDate, testList2[i].OrderDate);
                    }

                    //Test getting order by Cost reverse
                    testList2 = orders.GetOrderByCostReverse().ToList();
                    testList1.Reverse();


                    for (int i = 0; i < testList1.Count; i++)
                    {
                        Assert.Equal(testList1[i].OrderID, testList2[i].OrderID);
                        Assert.Equal(testList1[i].OrderCost, testList2[i].OrderCost);
                        Assert.Equal(testList1[i].OrderDate, testList2[i].OrderDate);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
