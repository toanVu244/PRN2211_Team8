using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class RequestModel
    {
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? ProductID { get; set; }
        public string? Image {  get; set; }
        public int? Type { get; set; } // 1= exist; 2= custom ; 3 = new design.
    }
}
