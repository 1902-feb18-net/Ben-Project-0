using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib.InterfaceRepos
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerImp> GetCustomers();

        CustomerImp GetCustomerById(int Id);

        bool IsValidId(int Id);

        CustomerImp GetCustomerByName(string fullName);

    }
}
