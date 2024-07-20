using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Request
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Status { get; set; }
        public int? ProductId { get; set; }
        public string? Image { get; set; }
        public int? Type { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
