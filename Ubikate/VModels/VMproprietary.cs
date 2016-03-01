using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VModels
{
    public class VMproprietary
    {
        public int? proprietaryid { get; set; }
        public string rfc { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public bool subscribed { get; set; }
        public DateTime regDate { get; set; }
    }
}
