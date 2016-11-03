using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmListarAsociados
    {
        public List<Asociado> asociados { get; set; }
        public String filtro { get; set; }


        public void fill()
        {
            UbicaloEntities context = new UbicaloEntities();

            if (!String.IsNullOrEmpty(filtro))
                this.asociados = context.Asociado.Where(x => x.Usuario.Contains(filtro.ToUpper())).ToList();
            else
                this.asociados = context.Asociado.ToList();
        }
    }
}