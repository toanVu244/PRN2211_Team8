using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;

namespace JPOS.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;
        private readonly IMapper _mapper;
        private readonly IProductMaterialRepository productMaterialRepository;

        public ProductService(IProductRepository productrepository, IMapper mapper,IProductMaterialRepository productMaterialRepository)
        {
            _productrepository = productrepository;
            _mapper = mapper;
            this.productMaterialRepository = productMaterialRepository;
        }

        public async Task<bool> CreateProduct(ProductModel model, List<ProductMaterialModel> materialofproduct)
        {
            if (model == null)
            {
                return false;
            }
            await _productrepository.InsertAsync(_mapper.Map<Product>(model));
            var GetIdProduct = await _productrepository.GetLastproduct();
            for (int i = 0; i < materialofproduct.Count(); i++)
            {
                materialofproduct[i].ProductID = GetIdProduct.ProductId;
                await productMaterialRepository.InsertAsync(_mapper.Map<ProductMaterial>(materialofproduct[i]));
            }

            return true;
        }

        public async Task<int> DuplicateProduct(int id)
        {
            var product = await _productrepository.GetByIdAsync(id);
            var listProductMaterial = await productMaterialRepository.GetMaterialsByProductID(product.ProductId);
            product.ProductId = 0;
            product.Status = "Pending";
            await _productrepository.InsertAsync(product);
            var LastProduct = await _productrepository.GetLastproduct();
            for (int i = 0; i < listProductMaterial.Count; i++)
            {
                listProductMaterial[i].ProductId = LastProduct.ProductId;
                listProductMaterial[i].ProductMaterialId = 0;
                await productMaterialRepository.InsertAsync(listProductMaterial[i]);
            }
            return LastProduct.ProductId;
        }

        public async Task<List<ProductModel>?> GetAllProduct()
        {
            List<Product> listProduct = await _productrepository.GetAllAsync();
            List<ProductModel> products = new List<ProductModel>();
            foreach (var product in listProduct)
            {
                var productModel = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    PriceMaterial = product.PriceMaterial,
                    PriceDesign = product.PriceDesign,
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
            if (id != null)
            {
                var product = await _productrepository.GetByIdAsync(id);
                var productModel = new ProductModel
                {

                    CreateDate = product.CreateDate,
                    CreateBy = product.CreateBy,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Image = product.Image,
                    Status = product.Status,
                    ProcessPrice = product.ProcessPrice,
                    PriceDesign = product.PriceDesign,
                    PriceMaterial = product.PriceMaterial
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
            var product = await _productrepository.GetproductByRequest(key);
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
            var product = await _productrepository.GetProductWithMaterialsAsync(productId);
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
            if (model != null)
            {
                await _productrepository.UpdateAsync(_mapper.Map<Product>(model));                return true;
            }
            return false;

        }




        public async Task<string> UploadImageToCloudinary(string design)
        {
            try
            {
                Account account = new Account(
                    "dkyv1vp1c",
                    "741931712965645",
                    "07xC7_CmYUhX3yGwPkdSe08uRM0"
                );

                Cloudinary cloudinary = new Cloudinary(account);


                if (design != null && design.StartsWith("data:image/"))
                {

                    string base64Image = design.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription("image.jpg", stream)
                        };

                        var uploadResult = await cloudinary.UploadAsync(uploadParams);

                        return uploadResult.Url.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image to Cloudinary: {ex.Message}");
            }

            return null; ;
        }



        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _productrepository.DeleteAsync(id);
            return result;
        }
    }




}
