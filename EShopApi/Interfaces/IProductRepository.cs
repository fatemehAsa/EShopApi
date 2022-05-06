using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Models;

namespace EShopApi.Interfaces
{
   public interface IProductRepository
   {
       IEnumerable<Product> GetAllProducts();
       Task<Product> Find(int id);
       Task<Product> AddProduct(Product product);
       Task<Product> Update(Product product);
       Task<Product> Delete(int id);
       Task<bool> IsExistProduct(int id);
   }
}
