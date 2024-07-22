using BusinessObject.Entities;
using JPOS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IProductService
    {
        public Task<ProductModel?> GetProductByID(int? id);
        public Task<Product?> GetProductByIDTest(int? id);
        public Task<bool?> DeatachProduct(Product model);
        public Task<ProductModel?> GetProductByRequest(string key);
        public Task<List<ProductModel>?> GetAllProduct();
        public Task<int> DuplicateProduct(int id);
        public Task<bool?> UpdateProduct(ProductModel model);
        public Task<bool?> UpdateProductTest(Product model);
        public Task<ProductDetailModel?> GetProductDetail(int id);
        public Task<bool> CreateProduct(ProductModel model, List<ProductMaterialModel> materialofproduct);
        public Task<string> UploadImageToCloudinary(string design);
        Task<bool> DeleteProductAsync(int id);
    }
}
