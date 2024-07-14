using AutoMapper;
using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateProduct(ProductModel model, List<ProductMaterialModel> materialofproduct)
        {
           if (model == null)
            {
                return false;
            }
            await _unitOfWork.Products.InsertAsync(_mapper.Map<Product>(model));
            var GetIdProduct = await _unitOfWork.Products.GetLastproduct();
            for (int i = 0; i < materialofproduct.Count(); i++)
            {
                materialofproduct[i].ProductID = GetIdProduct.ProductId;
                await _unitOfWork.ProductMaterials.InsertAsync(_mapper.Map<ProductMaterial>(materialofproduct[i]));
            }

            return true;
        }

        public async Task<int> DuplicateProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            var listProductMaterial = await _unitOfWork.ProductMaterials.GetMaterialsByProductID(product.ProductId);
            product.ProductId = 0;
            product.Status = "Pending";
            await _unitOfWork.Products.InsertAsync(product);
            var LastProduct = await _unitOfWork.Products.GetLastproduct();          
            for (int i = 0; i < listProductMaterial.Count; i++)
            {
                listProductMaterial[i].ProductId = LastProduct.ProductId;
                listProductMaterial[i].ProductMaterialId = 0;
                await _unitOfWork.ProductMaterials.InsertAsync(listProductMaterial[i]);
            }
            return LastProduct.ProductId;
        }

        public async Task<List<ProductModel>?> GetAllProduct()
        {
            List<Product> listProduct = await _unitOfWork.Products.GetAllAsync();
            List<ProductModel> products = new List<ProductModel>();
            foreach (var product in listProduct)
            {
                var productModel = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Image = product.Image,
                    Status = product.Status,
                    ProcessPrice = product.ProcessPrice,
                    CategoryId = product.CategoryId
                };
                products.Add(productModel);
            }
            return products;
        }

        public async Task<ProductModel?> GetProductByID(int? id)
        {
            if (id != null) {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                var productModel = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Image = product.Image,
                    Status = product.Status,
                    ProcessPrice = product.ProcessPrice,
                };
                return productModel;
            }
           return null;
        }

        public async Task<ProductModel?> GetProductByRequest(string key)
        {
            if (key == null)
            {
                return null;
            }
            var product =await _unitOfWork.Products.GetproductByRequest(key);
            var productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Image = product.Image,
                Status = product.Status,
                ProcessPrice = product.ProcessPrice,
            };
            return productModel;
        }

        public async Task<ProductDetailModel?> GetProductDetail(int productId)
        {
            var product = await _unitOfWork.Products.GetProductWithMaterialsAsync(productId);
            if (product == null)
            {
                return null;
            }

            var productDetailModel = new ProductDetailModel
            {
                ProductID = productId,
                ProductName = product.ProductName,
                Description = product.Description,
                Image = product.Image,
                Status = product.Status,
                MaterialPrice = product.PriceMaterial,
                ProcessPrice = product.ProcessPrice,
                DesignPrice = product.PriceDesign,
                CategoryName = product.Category?.CatName,
                Materials = product.ProductMaterials.Select(pm => new ProdMatModel
                {
                    MaterialID = pm.MaterialId,
                    MaterialName = pm.Material?.Name,
                    Quantity = pm.Quantity,
                    Price = pm.Price,
                }).ToList()
            };

            return productDetailModel;
        }

        public async Task<bool?> UpdateProduct(ProductModel model)
        {
           if(model != null)
            {
                await _unitOfWork.Products.UpdateAsync(_mapper.Map<Product>(model));
                await _unitOfWork.CompleteAsync();
                return true;
            }
           return false;
            
        }
    }
}
