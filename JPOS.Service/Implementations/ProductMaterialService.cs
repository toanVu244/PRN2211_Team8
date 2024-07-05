using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class ProductMaterialService : IProductMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductMaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool?> AddMaterialProduct(List<ProductMaterial> listdata)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductMaterial>?> GetmaterialByProductID(int id)
        {
            return _unitOfWork.ProductMaterials.GetMaterialsByProductID(id);
        }

        public async Task<bool> UpdateMaterialProduct(int id, List<ProductMaterial> newUpdate)
        {
            if ( newUpdate != null)
            {
                foreach (var item in newUpdate)
                {
                    item.ProductId = id;
                    await _unitOfWork.ProductMaterials.UpdateAsync(item);
                }

                return true;

            }
            else { 
            return false;
            }
           
        }
    }
}
