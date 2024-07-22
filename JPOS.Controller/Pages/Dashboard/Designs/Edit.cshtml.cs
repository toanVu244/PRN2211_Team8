using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using JPOS.Service.Tools;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class EditModel : PageModel
    {
        private readonly IDesignService _service;
        private readonly IProductService productService;

        public EditModel(IDesignService _repo, IProductService _productService)
        {
            _service = _repo;
            productService = _productService;
        }
        
        [BindProperty]
        public Design Design { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage ="Description is required !!")]
        public string Description {  get; set; }

        [BindProperty]
        [RequiredFile(ErrorMessage = "This must not be null")]
        public IFormFile ImageFiles { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _service.GetAllDesignAsync == null)
            {
                return NotFound();
            }

            var design =  await _service.GetDesignByIdAsync(id);
            if (design == null)
            {
                return NotFound();
            }
            Design = design;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var design = await _service.GetDesignByIdAsync(Design.DesignId);
                design.Description =  Description;
                string linkIMG = await ConvertImageToBase64AndUpload(ImageFiles);
                design.Picture = linkIMG;
                await _service.UpdateDesignAsync(design);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

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
