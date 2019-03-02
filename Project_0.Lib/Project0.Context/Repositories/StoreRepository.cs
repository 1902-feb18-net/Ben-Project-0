using Microsoft.EntityFrameworkCore;
using Project0.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_0.Lib
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
            //int num = 1;
            //foreach (StoreImp store in _db.Stores)
            //{
            //    Console.WriteLine($"{num++}. {store.Location}");
            //}
            return Mapper.Map(_db.Stores.Include(s => s.StoreId));

        }

        public StoreImp GetStoreByLocation(int Id)
        {
            //goes through _Data looking for the specified location
            //returns the store at the specified location
            //return _Data.First(s => s.Location == location);
            return Mapper.Map(_db.Stores.First(s => s.StoreId == Id));
        }
    }
}
