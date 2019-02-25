using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class StoreRepository
    {
        private readonly ICollection<Store> _Data;

        public Store GetStoreByLocation(string location)
        {
            //goes through _Data looking for the specified location
            //returns the store at the specified location
        }
    }
}
