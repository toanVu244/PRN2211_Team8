using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductMaterials = new HashSet<ProductMaterial>();
            RequestsDetails = new HashSet<RequestsDetail>();
        }

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

        public virtual Category? Category { get; set; }
        public virtual Design? Design { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<RequestsDetail> RequestsDetails { get; set; }
    }
}
