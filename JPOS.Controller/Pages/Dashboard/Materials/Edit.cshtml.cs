using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using JPOS.Model.Models;

namespace JPOS.Controller.Pages.Dashboard.Materials
{
    public class EditModel : PageModel
    {
        private readonly IMaterialService _materialService;

        public EditModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [BindProperty]
        public MaterialModel Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _materialService.GetmaterialByID(id.Value);
            if (material == null)
            {
                return NotFound();
            }

            Material = new MaterialModel
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Price = material.Price,
                Quantity = material.Quantity,
                TotalPrice = material.TotalPrice,
                Status = material.Status
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                await _materialService.UpdateMaterial(Material);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return RedirectToPage("./Index");
        }
    }
}
