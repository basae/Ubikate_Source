using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace VModels
{
    public class VMlogin
    {
        
        public long userId { get; set; }
        [Required(ErrorMessage="El Nombre de Usuario es Requerido")]
        public string username { get; set; }
        [Required(ErrorMessage="Contraseña Requerida")]
        public string password { get; set; }
        public string realName { get; set; }
    }
}
