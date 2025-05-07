using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Rol
    {
        public string RolId { get; set; }
        public string Descripcion { get; set; }

        public Rol()
        {
        }
        public Rol (string RolId, string Descripcion)
        {
            this.RolId = RolId;
            this.Descripcion = Descripcion;
        }
    }
}
