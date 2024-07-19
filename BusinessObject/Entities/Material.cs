using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Material
    {
        public Material()
        {
            ProductMaterials = new HashSet<ProductMaterial>();
        }

        public int MaterialId { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
