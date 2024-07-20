
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private static ProductRepository _instance;

        public static ProductRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductRepository();
                }
                return _instance;
            }
        }
        public async Task<List<Product>?> GetAllproduct()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetLastproduct()
        {
            var lastRequest = await _context.Products
               .OrderByDescending(r => r.ProductId)
               .FirstOrDefaultAsync();
            return lastRequest;
        }

        public async Task<Product?> GetproductByRequest(string key)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.CreateBy == key);
        }

        public async Task<Product?> GetProductWithMaterialsAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }
    }
}
