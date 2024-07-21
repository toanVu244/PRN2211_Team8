using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Repository.Repositories.Implementations;

namespace JPOS.Service.Implementations
{
    public class ProductMaterialService : IProductMaterialService
    {
        /*private readonly IProductMaterialRepository _productmaterialrepository;
        public ProductMaterialService(IProductMaterialRepository productmaterialrepository)
        {
            _productmaterialrepository = productmaterialrepository;
        }*/
        public Task<bool?> AddMaterialProduct(List<ProductMaterial> listdata)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductMaterial>?> GetmaterialByProductID(int id)
        {
            return ProductMaterialRepository.Instance.GetMaterialsByProductID(id);
        }

        public async Task<bool> UpdateMaterialProduct(int id, List<ProductMaterial> newUpdate)
        {
            if (newUpdate != null)
            {

                for (int i = 0;i < newUpdate.Count; i ++) {
                    await ProductMaterialRepository.Instance.UpdateAsync(newUpdate[i]);
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
