using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Design
    {
        public Design()
        {
            Products = new HashSet<Product>();
        }

        public int DesignId { get; set; }
        public string? CreateBy { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
