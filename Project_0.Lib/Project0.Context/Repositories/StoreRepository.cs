﻿using Microsoft.EntityFrameworkCore;
using Project0.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_0.Lib;


namespace Project_0.Context.Repo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Project0Context _db;

        public StoreRepository(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<StoreImp> GetStores()
        {
            return Mapper.Map(_db.Stores.Include(s => s.Orders));
        }

        public StoreImp GetStoreByLocation(int Id)
        {
            var value = _db.Stores.First(s => s.StoreId == Id) ?? throw new ArgumentException("Enter valid store  ID");
            return Mapper.Map(value);
        }

        public bool IsValidId(int Id)
        {
            var value = _db.Stores.Find(Id);
            return value != null;
        }

        public InventoryImp GetInventory(GamesImp game, StoreImp store)
        {
            var value = _db.Inventory.First(i => i.GameId == game.Id && i.StoreId == store.IDNumber);
            return Mapper.Map(value);
        }

        public void RemoveFromStock(int quantity, GamesImp game, StoreImp store)
        {
            var value = _db.Inventory.First(i => i.GameId == game.Id && i.StoreId == store.IDNumber);
            value.GameRemaining -= quantity;
            _db.SaveChanges();
        }

        public void RemoveDeluxeFromStock(int quantity, int storeId)
        {
            var value = _db.Stores.First(s => s.StoreId == storeId);
            value.DeluxePackageRemaining -= quantity;
            _db.SaveChanges();
        }
    }
}
