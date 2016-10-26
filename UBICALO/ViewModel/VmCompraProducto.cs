using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmCompraProducto
    {
        public int productoID { get; set; }

        public Producto producto { get; set; }

        public void fill()
        {
            UbicaloEntities context = new UbicaloEntities();
            producto = context.Producto.FirstOrDefault(x => x.ProductoID == productoID);

        }
    }
}