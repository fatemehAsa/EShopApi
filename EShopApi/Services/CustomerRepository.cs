using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Interfaces;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EShopApi.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private EShopApi_DBContext _context;
        private IMemoryCache _cache;

        public CustomerRepository(EShopApi_DBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return _context.Customers.ToList();
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Find(int id)
        {
            var cache = _cache.Get<Customer>(id);
            if (cache!=null)
            {
                return cache;
            }
            else
            {
             var customer=await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
                var cacheOption=new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId, customer, cacheOption);
                return customer;
            }
        }

        public async Task<Customer> Remove(int id)
        {
            var person = await _context.Customers.SingleAsync(c => c.CustomerId == id);
            _context.Customers.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }


        public async Task<Customer> Update(Customer customer)
        {
             _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> IsExistCustomer(int id)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<int> CustomerCount()
        {
            return await _context.Customers.CountAsync();
        }
    }
}
