using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmEstablecimientoInfo
    {
        public int establecimientoID { get; set; }
        public string establecimientoNombre { get; set; }

        public List<Producto> productos { get; set; }


        public void fill()
        {
            UbicaloEntities context = new UbicaloEntities();

            var query = context.Producto.AsQueryable();
            if (!(establecimientoID == -1))
                query = context.Producto.Where(x => x.EstablecimientoID == establecimientoID);

            productos = query.ToList();

            Establecimiento e = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == establecimientoID);
            establecimientoNombre = e.Nombre;
        }
    }
}