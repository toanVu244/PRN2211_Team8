using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }

        public virtual User? CreateByNavigation { get; set; }
    }
}
