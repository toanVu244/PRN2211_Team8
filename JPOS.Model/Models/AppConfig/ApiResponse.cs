using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models.AppConfig
{
    public class ApiResponse
    {
        public bool? isSuccess;
        public int? Code { get; set; }
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
