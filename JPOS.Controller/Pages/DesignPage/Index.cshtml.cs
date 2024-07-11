using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.DesignPage
{
    public class IndexModel : PageModel
    {
        private readonly IDesignService _designService;

        public IndexModel(IDesignService designService)
        {
            _designService = designService;
        }

        public IList<Design> Design { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_designService.GetAllDesignAsync == null)
            {
                return;
            }
            Design = await _designService.GetAllDesignAsync();
        }
    }
}
