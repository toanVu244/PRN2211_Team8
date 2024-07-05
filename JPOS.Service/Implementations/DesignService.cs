using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    public class DesignService : IDesignService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateDesignAsync(Design design,int idProduct)
        {
            try
            {
                Account account = new Account(
                    "dkyv1vp1c",      // replace with your Cloudinary cloud name
                    "741931712965645",         // replace with your Cloudinary API key
                    "07xC7_CmYUhX3yGwPkdSe08uRM0"       // replace with your Cloudinary API secret
                );

                Cloudinary cloudinary = new Cloudinary(account);

                // Ensure design.Picture is not null and starts with "data:image/"
                if (design.Picture != null && design.Picture.StartsWith("data:image/"))
                {
                    // Split base64 string and decode the image bytes
                    string base64Image = design.Picture.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Image);

                    // Convert byte[] to Stream
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        // Upload the image to Cloudinary
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription("image.jpg", stream) // Use MemoryStream here
                        };

                        var uploadResult = await cloudinary.UploadAsync(uploadParams);

                        // Update design.Picture with Cloudinary image URL
                        design.Picture = uploadResult.Url.ToString();

                        // Insert the design into database using your _unitOfWork or repository
                        if (await _unitOfWork.Designs.InsertAsync(design))
                        {
                           /* var designupdate = await _unitOfWork.Designs.GetLastDesign();
                            var updateProduct = await _unitOfWork.Products.GetByIdAsync(idProduct);
                            updateProduct.DesignID = designupdate.DesignID;
                            await _unitOfWork.Products.UpdateAsync(updateProduct);*/
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error uploading image to Cloudinary: {ex.Message}");
            }

            return false;
        }


        public Task<bool> DeleteDesignAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Design>?> GetAllDesignAsync()
        { 
           return await _unitOfWork.Designs.GetAllDesign();
        }

        public Task<Design> GetDesignByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDesignAsync(Design design)
        {

            try
            {
                Account account = new Account(
                    "dkyv1vp1c",      // replace with your Cloudinary cloud name
                    "741931712965645",         // replace with your Cloudinary API key
                    "07xC7_CmYUhX3yGwPkdSe08uRM0"       // replace with your Cloudinary API secret
                );

                Cloudinary cloudinary = new Cloudinary(account);

                // Ensure design.Picture is not null and starts with "data:image/"
                if (design.Picture != null && design.Picture.StartsWith("data:image/"))
                {
                    // Split base64 string and decode the image bytes
                    string base64Image = design.Picture.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Image);

                    // Convert byte[] to Stream
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        // Upload the image to Cloudinary
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription("image.jpg", stream) // Use MemoryStream here
                        };

                        var uploadResult = await cloudinary.UploadAsync(uploadParams);

                        // Update design.Picture with Cloudinary image URL
                        design.Picture = uploadResult.Url.ToString();

                        // Insert the design into database using your _unitOfWork or repository
                        if (await _unitOfWork.Designs.UpdateAsync(design))
                        {
                          
                            return await _unitOfWork.CompleteAsync() >= 0;
                           
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image to Cloudinary: {ex.Message}");
            }

            return false;
        }


       
        }
    }

