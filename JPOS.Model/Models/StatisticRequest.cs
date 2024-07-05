using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class StatisticRequest
    {
       public string Time { get; set; }
        public int OrderExist { get; set; } = 0;
        public int OrderCustome { get; set; } = 0;

        public int OrderDesign { get; set; } = 0;
    }
}
