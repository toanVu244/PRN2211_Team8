using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class ProductDetailModel
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public int? MaterialPrice { get; set; }
        public int? ProcessPrice { get; set; }
        public int? DesignPrice { get; set; }
        public List<ProdMatModel?> Materials { get; set; }
        public string? CategoryName { get; set; }
    }
}
