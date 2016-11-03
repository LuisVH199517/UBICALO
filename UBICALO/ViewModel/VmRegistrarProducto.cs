using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;
using System.ComponentModel.DataAnnotations;

namespace UBICALO.ViewModel
{
    public class VmRegistrarProducto
    {
        public int? productoID { get; set; }
        

        [Required]
        public String nombre { get; set; }
        [Required]
        public String descripcion { get; set; }
        public double costo { get; set; }
        public int establecimiento { get; set; }
        public int categoria { get; set; }

        public String image { get; set; }


        public List<Categoria> categorias { get; set; }

        public UbicaloEntities context;

        public VmRegistrarProducto()
        {
            categoria = 0;
        }

        public void fill(UbicaloEntities context, int? productoID)
        {
            this.productoID = productoID;
            categorias = context.Categoria.ToList();

            if (productoID.HasValue)
            {
                Producto producto = context.Producto.FirstOrDefault(x => x.ProductoID == productoID);

                this.nombre = producto.Nombre;
                this.descripcion = producto.Descripcion;
                this.costo = producto.Costo;
                this.establecimiento = producto.EstablecimientoID;
                this.categoria = producto.CategoriaID;
                this.image = producto.imagen;             
            }
            
        }
    }
}