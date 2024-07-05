using System;
using System.Collections.Generic;

namespace JPOS.Model.Entities
{
    public partial class Feedback
    {
        public int FeedBackId { get; set; }
        public string? Content { get; set; }
        public int? Rate { get; set; }
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
