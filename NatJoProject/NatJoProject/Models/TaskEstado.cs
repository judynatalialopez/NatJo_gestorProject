using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class TaskEstado
    {
        public string EstId {  get; set; }
        public string Descripcion { get; set; }

        public TaskEstado()
        {
        }
        public TaskEstado (string EstId, string Descripcion)
        {
            this.EstId = EstId;
            this.Descripcion = Descripcion;
        }
    }
}
