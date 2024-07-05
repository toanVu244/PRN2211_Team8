using System;
using System.Collections.Generic;

namespace JPOS.Model.Entities
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string? RequestId { get; set; }
        public string UserId { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
