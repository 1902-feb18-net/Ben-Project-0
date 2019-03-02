using Project_0.Lib;
using Project0.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_0.Context.Repo
{
    public class OrdersRepository : IOrdersRepository
    {

        private readonly Project0Context _db;

        public OrdersRepository(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public OrderImp GetOrderByID(int ID)
        {
            //goes through _Data to find order with specified ID
            //returns null if ID doesn't exist
            return Mapper.Map(_db.Orders.First(r => r.OrderId == ID)) ?? throw new ArgumentNullException("ID needs to be valid");
        }

        public IEnumerable<OrderImp> GetOrderByDate()
        {
            //foreach (OrderImp r in _Data)
            //{
            //    yield return r;
            //}
            return Mapper.Map(_db.Orders.OrderBy(r => r.OrderDate));
        }

        public IEnumerable<OrderImp> GetOrderByDateReverse()
        {
            //foreach (OrderImp r in _Data)
            //{
            //    yield return r;
            //}
            return Mapper.Map(_db.Orders.OrderByDescending(r => r.OrderDate));

        }

        public IEnumerable<OrderImp> GetOrderByCost()
        {
            return Mapper.Map(_db.Orders.OrderBy(r => r.OrderCost));
        }

        public IEnumerable<OrderImp> GetOrderByCostReverse()
        {
            return Mapper.Map(_db.Orders.OrderByDescending(r => r.OrderCost));

        }

        public void DeactiveOrder(OrderImp order)
        {
            order.Valid = false;
        }

        public void AddOrder(OrderImp order)
        {
            _db.Add(Mapper.Map(order));
            _db.SaveChanges(); 
        }

        public void AddOrderItem(GamesImp Game, OrderImp Order, int quantity)
        {
            OrderGamesImp orderGame = new OrderGamesImp(Order.OrderID, Game.Id, quantity);
            _db.OrderGames.Add(Mapper.Map(orderGame));
            _db.SaveChanges();
            //if (restaurant != null)
            //{
            //    // get the db's version of that restaurant
            //    // (can't use Find with Include)
            //    var contextRestaurant = _db.Restaurant.Include(r => r.Review).First(r => r.Id == restaurant.Id);
            //    restaurant.Reviews.Add(review);
            //    contextRestaurant.Review.Add(Mapper.Map(review));
            //}
            //else
            //{
            //    _db.Add(Mapper.Map(review));
            //}
        }
    }
}
