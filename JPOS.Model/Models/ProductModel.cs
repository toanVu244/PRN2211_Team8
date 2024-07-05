using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string? CreateBy { get; set; }
        public int? PriceMaterial { get; set; }
        public int? PriceDesign { get; set; }
        public int? ProcessPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? DesignId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
    }
}
