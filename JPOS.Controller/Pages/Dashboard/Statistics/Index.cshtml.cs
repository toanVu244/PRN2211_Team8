using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using JPOS.Model.Models;
using System;

namespace JPOS.Controller.Pages.Dashboard.Statistics
{
    public class IndexModel : PageModel
    {
        private readonly IRequestService _requestService;

        public IndexModel(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public List<StatisticRequest> Statistics { get; set; } = new List<StatisticRequest>();

        public async Task OnGetAsync()
        {
            var statistics = await _requestService.GetRequestStatistic(DateTime.Now.Year);
            if (statistics != null)
            {
                Statistics = statistics;
            }
        }

        public async Task<JsonResult> OnGetGetStatisticsAsync(int year)
        {
            var statistics = await _requestService.GetRequestStatistic(year);
            return new JsonResult(statistics);
        }

    }
}
