using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class FeedbackModel
    {
        public int FeedBackID { get; set; }
        public string? Content { get; set; }
        public int? Rate { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
    }
}
