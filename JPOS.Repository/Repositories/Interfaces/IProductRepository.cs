
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<Product?> GetproductByRequest(string key);

        public Task<List<Product>?> GetAllproduct();

        public Task<Product?> GetLastproduct();
        public Task<Product?> GetProductWithMaterialsAsync(int id);
    }
}
