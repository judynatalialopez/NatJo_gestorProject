using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class TaskEstado
    {
        public int EstId {  get; set; }
        public string Descripcion { get; set; }

        public TaskEstado()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public TaskEstado (int EstId, string Descripcion)
        {
            this.EstId = EstId;
            this.Descripcion = Descripcion;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public TaskEstado(string Descripcion)
        {
            this.Descripcion = Descripcion;
        }
    }
}
