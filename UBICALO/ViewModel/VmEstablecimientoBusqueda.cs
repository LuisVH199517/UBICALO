using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmEstablecimientoBusqueda
    {
        public String filtro { get; set; }
        public List<Establecimiento> lista { get; set; }


        public void fill()
        {
            UbicaloEntities context = new UbicaloEntities();

            var query = context.Establecimiento.AsQueryable();
            if (!String.IsNullOrEmpty(filtro))
                query = query.Where(x => x.Nombre.Contains(filtro.ToUpper()));
            lista = query.ToList();
        }
    }

}