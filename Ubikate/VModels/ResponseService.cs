using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VModels
{
    public class ResponseService<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Result { get; set; }

        public ResponseService()
        {
            Error = false;
            Message = string.Empty;
            Result = null;
            Result = new List<T>();
        }
    }
}
