using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.ViewModels
{
    public class MaterialShow
    {
        public int ProductMaterialId { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public double? Quantity { get; set; }
        public int? Price { get; set; }
        public virtual Material Material { get; set; } = null!;
    }
   
}
