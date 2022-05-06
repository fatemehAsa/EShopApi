using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Interfaces;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopApi.Services
{
    public class SalesPersonsRepository : ISalesPersonsRepository
    {
        private EShopApi_DBContext _context;

        public SalesPersonsRepository(EShopApi_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<SalesPerson> GetAllSalesPerson()
        {
            return _context.SalesPersons.ToList();
        }

        public async Task<SalesPerson> Find(int id)
        {
            return await _context.SalesPersons.FindAsync(id);
        }

        public async Task<SalesPerson> Add(SalesPerson salesPerson)
        {
            await _context.SalesPersons.AddAsync(salesPerson);
            await _context.SaveChangesAsync();
            return salesPerson;
        }

        public async Task<SalesPerson> Update(SalesPerson salesPerson)
        {
            _context.SalesPersons.Update(salesPerson);
            await _context.SaveChangesAsync();
            return salesPerson;
        }

        public async Task<SalesPerson> Delete(int id)
        {
            var person = await Find(id);
            _context.SalesPersons.Remove(person);
            return person;
        }

        public async Task<bool> IsExistSalesPerson(int id)
        {
            return await _context.SalesPersons.AnyAsync(p => p.SalesPersonId == id);
        }
    }
}
