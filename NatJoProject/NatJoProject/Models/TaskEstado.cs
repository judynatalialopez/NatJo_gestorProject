using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class TaskEstado
    {
        public int estId {  get; set; }
        public string descripcion { get; set; }

        public TaskEstado (int estId, string descripcion)
        {
            this.estId = estId;
            this.descripcion = descripcion;
        }
    }
}
