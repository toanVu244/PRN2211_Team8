
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class ProductMaterialRepository : GenericRepository<ProductMaterial>, IProductMaterialRepository
    {
        private readonly JPOS_DatabaseContext _context;

        public ProductMaterialRepository(JPOS_DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductMaterial>> GetMaterialsByProductID(int pid)
        {
            return await _context.ProductMaterials.Where(pm => pm.ProductId == pid).ToListAsync();
        }
    }
}
