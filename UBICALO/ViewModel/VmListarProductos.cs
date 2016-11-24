using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmListarProductos
    {
        public IEnumerable<Producto> productos { get; set; }
        public String filtro { get; set; }

        public void fill(int estableciminetoID)
        {
            UbicaloEntities context = new UbicaloEntities();

            var query = context.Producto.Where(x => x.EstablecimientoID == estableciminetoID).AsQueryable();

            if (!String.IsNullOrEmpty(filtro))
                query = query.Where(x => x.Nombre.Contains(filtro.ToUpper()));

            this.productos = query;
        }
    }
}