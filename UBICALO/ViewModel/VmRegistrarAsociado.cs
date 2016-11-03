using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmRegistrarAsociado
    {
        public int? asociadoID { get; set; }

        [Required]
        public String usuario { get; set; }
        [Required]
        public String clave { get; set; }

        public int establecimientoId { get; set; }
        public String imagen { get; set; }

        public List<Establecimiento> establecimientos { get; set; }

        
        public void fill(int? asociadoID)
        {
            UbicaloEntities context = new UbicaloEntities();

            this.asociadoID = asociadoID;

            if (asociadoID.HasValue)
            {
                Asociado asociado = context.Asociado.FirstOrDefault(x => x.AsociadoID == asociadoID);

                this.usuario = asociado.Usuario;
                this.clave = asociado.Clave;
                this.establecimientoId = asociado.EstablecimientoID;
                this.imagen = asociado.Foto;
            }

            establecimientos = context.Establecimiento.ToList();
        }
    }

}
