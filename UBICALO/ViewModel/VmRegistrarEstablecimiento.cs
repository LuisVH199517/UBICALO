using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UBICALO.Models;
using System.ComponentModel.DataAnnotations;

namespace UBICALO.ViewModel
{
    public class VmRegistrarEstablecimiento
    {
        public int? establecimientoID { get; set; }

        [Required]
        public String nombre { get; set; }
        [Required]
        public String direccion { get; set; }
        [Required]
        public String ruc { get; set; }
        [Required]
        public String latitud { get; set; }
        [Required]
        public String longitud { get; set; }
        [Required]
        public String portal { get; set; }

        public String imagen { get; set; }

                

        public void fill(int? establecimientoID)
        {
            this.establecimientoID = establecimientoID;

            if (establecimientoID.HasValue)
            {
                UbicaloEntities context = new UbicaloEntities();
                Establecimiento establecimiento = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == establecimientoID);

                this.nombre = establecimiento.Nombre;
                this.direccion = establecimiento.Direccion;
                this.ruc = establecimiento.RUC;
                this.latitud = establecimiento.Latitud;
                this.longitud = establecimiento.Longitud;
                this.portal = establecimiento.Portal;
                this.imagen = establecimiento.imagen;
            }
        }
    }

}
