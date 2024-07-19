using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;

namespace JPOS.Service.Implementations
{
    public class ProductMaterialService : IProductMaterialService
    {
        private readonly IProductMaterialRepository _productmaterialrepository;
        public ProductMaterialService(IProductMaterialRepository productmaterialrepository)
        {
            _productmaterialrepository = productmaterialrepository;
        }
        public Task<bool?> AddMaterialProduct(List<ProductMaterial> listdata)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductMaterial>?> GetmaterialByProductID(int id)
        {
            return _productmaterialrepository.GetMaterialsByProductID(id);
        }

        public async Task<bool> UpdateMaterialProduct(int id, List<ProductMaterial> newUpdate)
        {
            if (newUpdate != null)
            {
                foreach (var item in newUpdate)
                {
                    item.ProductId = id;
                    await _productmaterialrepository.UpdateAsync(item);
                }

                return true;

            }
            else
            {
                return false;
            }

        }
    }
}
