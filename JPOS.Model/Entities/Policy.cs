using System;
using System.Collections.Generic;

namespace JPOS.Model.Entities
{
    public partial class Policy
    {
        public int PolicyId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }

        public virtual User? CreateByNavigation { get; set; }
    }
}
