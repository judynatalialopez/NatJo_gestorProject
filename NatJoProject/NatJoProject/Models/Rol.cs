using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Descripcion { get; set; }

        public Rol()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Rol (int RolId, string Descripcion)
        {
            this.RolId = RolId;
            this.Descripcion = Descripcion;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Rol(string Descripcion)
        {
            this.Descripcion = Descripcion;
        }
    }
}
