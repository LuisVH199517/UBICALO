using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBICALO.Models;

namespace UBICALO.ViewModel
{
    public class VmEstaMapa
    {

        public List<Establecimiento> lista { get; set; }

        public void fill()
        {

            UbicaloEntities context = new UbicaloEntities();

            this.lista = context.Establecimiento.ToList();
        }

    }
}