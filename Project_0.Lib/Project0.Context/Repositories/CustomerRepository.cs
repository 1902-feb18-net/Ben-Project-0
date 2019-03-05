using Project_0.Lib;
using Project_0.Lib.InterfaceRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Context.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Project0Context _db;

        public CustomerRepository(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<CustomerImp> GetCustomers()
        {
            return Mapper.Map(_db.Customers);
        }

        public CustomerImp GetCustomerById(int Id)
        {
            return Mapper.Map(_db.Customers.Find(Id));
        }
    }
}
