using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Service.Interfaces;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using JPOS.Service.Implementations;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class CreateModel : PageModel
    {
        private readonly IDesignService _service;
        private readonly IProductService productService;

        public CreateModel(IDesignService _repo, IProductService _productService)
        {
            _service = _repo;
            productService = _productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Design Design { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageFiles { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Design == null)
            {
                return Page();
            }
            string linkIMG = await ConvertImageToBase64AndUpload(ImageFiles);
            Design.Picture = linkIMG;
            Design.CreateBy = HttpContext.Session.GetString("UserName");
            Design.CreateDate = DateTime.Now;
            await _service.CreateDesign(Design);
            return RedirectToPage("./Index");
        }

        private async Task<string> ConvertImageToBase64AndUpload(IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return await productService.UploadImageToCloudinary($"data:{image.ContentType};base64,{base64String}");
            }
        }
    }
}
