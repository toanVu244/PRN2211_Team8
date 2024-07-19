using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IProductMaterialService
    {
        public Task<List<ProductMaterial>?> GetmaterialByProductID(int id);

        public Task<bool?> AddMaterialProduct(List<ProductMaterial> listdata);

        public Task<bool> UpdateMaterialProduct(int id, List<ProductMaterial> newUpdate);
    }
}
