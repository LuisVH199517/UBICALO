using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmEstadoCuenta
    {
        public List<Compra> compras { get; set; }

        public void fill(int establecimientoID)
        {
            UbicaloEntities context = new UbicaloEntities();

            this.compras = context.Compra.Where(x => x.Producto.EstablecimientoID == establecimientoID).ToList();
        }
    }
}