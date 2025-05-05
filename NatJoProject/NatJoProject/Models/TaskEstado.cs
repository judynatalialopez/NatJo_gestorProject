using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class TaskEstado
    {
        public string estId {  get; set; }
        public string descripcion { get; set; }

        public TaskEstado()
        {
        }
        public TaskEstado (string estId, string descripcion)
        {
            this.estId = estId;
            this.descripcion = descripcion;
        }
    }
}
