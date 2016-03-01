using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VModels
{
    public class BusinessMap
    {
        public int businessid { get; set; }
        public string name { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public int businessType { get; set; }
    }
}
