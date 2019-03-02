using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_0.Lib
{
    public interface IStoreRepository
    {
        IEnumerable<StoreImp> GetStores();

        StoreImp GetStoreByLocation(int Id);
    }
}
