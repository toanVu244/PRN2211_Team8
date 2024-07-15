using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JPOS.Model.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CatId { get; set; }

        [Required]
        public string? CatName { get; set; }
        [Required]
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
