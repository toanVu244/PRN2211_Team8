using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public partial class Request
    {
        public Request()
        {
            RequestsDetails = new HashSet<RequestsDetail>();
        }

        public int RequestId { get; set; }
        public string UserId { get; set; } = null!;
        public int Total { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RequestsDetail> RequestsDetails { get; set; }
    }
}
