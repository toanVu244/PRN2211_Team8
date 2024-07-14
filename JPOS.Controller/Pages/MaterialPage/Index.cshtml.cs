using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Model.Models;

namespace JPOS.Controller.Pages.MaterialPage
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialService _context;

        public IndexModel(IMaterialService _service)
        {
            _context = _service;
        }

        public IList<Material> Material { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GetAllmaterial != null)
            {
                Material = await _context.GetAllmaterial();
            }
        }
    }
}
