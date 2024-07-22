﻿using AutoMapper;
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
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Implementations;

namespace JPOS.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateProduct(ProductModel model, List<ProductMaterialModel> materialofproduct)
        {
            if (model == null)
            {
                return false;
            }
            await ProductRepository.Instance.InsertAsync(_mapper.Map<Product>(model));
            var GetIdProduct = await ProductRepository.Instance.GetLastproduct();
            for (int i = 0; i < materialofproduct.Count(); i++)
            {
                materialofproduct[i].ProductID = GetIdProduct.ProductId;
                await ProductMaterialRepository.Instance.InsertAsync(_mapper.Map<ProductMaterial>(materialofproduct[i]));
            }

            return true;
        }

        public async Task<int> DuplicateProduct(int id)
        {
            var product = await ProductRepository.Instance.GetByIdAsync(id);
            var listProductMaterial = await ProductMaterialRepository.Instance.GetMaterialsByProductID(product.ProductId);
            product.ProductId = 0;
            product.Status = "Pending";
            await ProductRepository.Instance.InsertAsync(product);
            var LastProduct = await ProductRepository.Instance.GetLastproduct();
            for (int i = 0; i < listProductMaterial.Count; i++)
            {
                listProductMaterial[i].ProductId = LastProduct.ProductId;
                listProductMaterial[i].ProductMaterialId = 0;
                await ProductMaterialRepository.Instance.InsertAsync(listProductMaterial[i]);
            }
            return LastProduct.ProductId;
        }

        public async Task<List<ProductModel>?> GetAllProduct()
        {
            List<Product> listProduct = await ProductRepository.Instance.GetAllAsync();
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
                var product = await ProductRepository.Instance.GetByIdAsync(id);
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

        public async Task<Product?> GetProductByIDTest(int? id)
        {
            if (id != null)
            {
                var product = await ProductRepository.Instance.GetByIdAsync(id);
                return product;
            }
            return null;
        }

        public async Task<ProductModel?> GetProductByRequest(string key)
        {
            if (key == null)
            {
                return null;
            }
            var product = await ProductRepository.Instance.GetproductByRequest(key);
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
            var product = await ProductRepository.Instance.GetProductWithMaterialsAsync(productId);
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
                Product product = _mapper.Map<Product>(model);
                ProductRepository.Instance.Detach(product);
                await ProductRepository.Instance.UpdateAsync(product);                
                return true;
            }
            return false;
        }

        public async Task<bool?> DeatachProduct(Product model)
        {
            if (model != null)
            {
                ProductRepository.Instance.Detach(model);
                return true;
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
            var result = await ProductRepository.Instance.DeleteAsync(id);
            return result;
        }

        public async Task<bool?> UpdateProductTest(Product model)
        {
            if (model != null)
            {
                Product product = _mapper.Map<Product>(model);
                ProductRepository.Instance.Detach(product);
                await ProductRepository.Instance.UpdateAsync(product);
                return true;
            }
            return false;
        }
    }




}
