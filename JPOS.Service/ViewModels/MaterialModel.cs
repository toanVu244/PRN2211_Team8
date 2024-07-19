using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class MaterialModel
    {
        public int MaterialId { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? TotalPrice { get; set; }
        public string? Status { get; set; }
    }
}
