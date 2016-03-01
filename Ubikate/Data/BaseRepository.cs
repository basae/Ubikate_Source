using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseRepository
    {
        public static string sqlConnection = ConfigurationManager.ConnectionStrings["UbikateConecction"].ConnectionString;
    }
}
