using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_0.Lib
{
    public interface IOrdersRepository
    {
        /// <summary>
        /// Search all orders for ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        OrderImp GetOrderByID(int ID);

        /// <summary>
        /// Search orders by date, from oldest to newest
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderImp> GetOrderByDate();

        /// <summary>
        /// Search orders by date, from newest to oldest
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderImp> GetOrderByDateReverse();

        /// <summary>
        /// Searches order by cost, from most expensive to least
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderImp> GetOrderByCost();

        /// <summary>
        /// Searches orders by cost, from least expensive to most
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderImp> GetOrderByCostReverse();

        /// <summary>
        /// Deactivates Order
        /// </summary>
        /// <param name="order"></param>
        void DeactiveOrder(OrderImp order);

        /// <summary>
        /// Adds an order to the repo list
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        void AddOrder(OrderImp order);

        /// <summary>
        /// Adds an entry to OrderGames, which records all games and their quantities bought through Orders
        /// Should be called a number of times per order equal to number of game types bought per order
        /// So if you buy both kinds of games in one order, should be called twice
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="Order"></param>
        /// <param name="quantity"></param>
        void AddOrderItem(GamesImp Game, OrderImp Order, int quantity);

        decimal GetTotalOrderCost(OrderImp Order, OrderGamesImp OrderGame);

    }
}
