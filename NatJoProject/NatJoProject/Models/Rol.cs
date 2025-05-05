using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Rol
    {
        public string rolId { get; set; }
        public string descripcion { get; set; }

        public Rol()
        {
        }
        public Rol (string rolId, string descripcion)
        {
            this.rolId = rolId;
            this.descripcion = descripcion;
        }
    }
}
