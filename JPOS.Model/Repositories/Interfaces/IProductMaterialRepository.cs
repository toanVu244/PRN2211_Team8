using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IProductMaterialRepository : IGenericRepository<ProductMaterial>
    {
        public Task<List<ProductMaterial>> GetMaterialsByProductID(int pid);
    }
}
