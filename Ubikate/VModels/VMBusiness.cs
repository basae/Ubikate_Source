using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VModels
{
    public class VMBusiness
    {
        public int? businessid { get; set; }
        public string name { get; set; }
        public Address address { get; set; }
        public int proprietaryid { get; set; }
        public int businesstype { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
