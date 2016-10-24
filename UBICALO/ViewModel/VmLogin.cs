using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace UBICALO.ViewModel
{
    public class VmLogin
    {
        [Required]
        public String usuario { get; set; }
        [Required]
        public String clave { get; set; }
        public String correo { get; set; }
        public String imagen { get; set; }
        public String rol { get; set; }
        public String FbID { get; set; }


    }
}