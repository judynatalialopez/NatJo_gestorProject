using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class TaskEstado
    {
        public char estId {  get; set; }
        public string descripcion { get; set; }

        public TaskEstado (char estId, string descripcion)
        {
            this.estId = estId;
            this.descripcion = descripcion;
        }
    }
}
