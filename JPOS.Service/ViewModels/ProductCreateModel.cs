using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class ProductCreateModel
    {
        public ProductModel? Product { get; set; }
        public List<ProductMaterialModel>? Materials { get; set; }
    }
}
