using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class ProductMaterialModel
    {
        public int ProductID { get; set; }
        public int MaterialID { get; set; }
        public string? MaterialName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
    public class ProdMatModel
    {
        public int MaterialID { get; set; }
        public string? MaterialName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}
