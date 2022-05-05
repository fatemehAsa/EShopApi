using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Models;

namespace EShopApi.Interfaces
{
   public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomer();
        Task<Customer> Add(Customer customer);
        Task<Customer> Find(int id);
        Task<Customer> Remove(int id);
        Task<Customer> Update(Customer customer);
        Task<bool> IsExistCustomer(int id);
        Task<int> CustomerCount();
    }
}
