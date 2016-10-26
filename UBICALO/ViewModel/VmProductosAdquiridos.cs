using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;
using UBICALO.Helpers;

namespace UBICALO.ViewModel
{
    public class VmProductosAdquiridos
    {
        public List<CrossCompra> listaCrossCompra { get; set; }


        public void fill(int clienteid)
        {
            listaCrossCompra = new List<CrossCompra>();
            UbicaloEntities context = new UbicaloEntities();

            List<Compra> compras = context.Compra.Where(x => x.ClienteID == clienteid).ToList();


            foreach (var p in compras)
            {
                CrossCompra obj = new CrossCompra();
                obj.compraID = p.CompraID;
                obj.qr = p.QR;


                Producto compra = context.Producto.FirstOrDefault(x => x.ProductoID == p.ProductoID);
                obj.nombre = compra.Nombre;
                obj.costo = compra.Costo;

                Establecimiento estab = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == compra.EstablecimientoID);
                obj.local = estab.Nombre;

                this.listaCrossCompra.Add(obj);
            }

        }
    }
}