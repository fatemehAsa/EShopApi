using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Models;

namespace EShopApi.Interfaces
{
   public interface ISalesPersonsRepository
   {
       IEnumerable<SalesPerson> GetAllSalesPerson();
       Task<SalesPerson> Find(int id);
       Task<SalesPerson> Add(SalesPerson salesPerson);
       Task<SalesPerson> Update(SalesPerson salesPerson);
       Task<SalesPerson> Delete(int id);
       Task<bool> IsExistSalesPerson(int id);
   }
}
