using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Sexo
    {
        public int sxId {  get; set; }
        public string descripcion { get; set; }

        public Sexo(int sxId, string descripcion)
        {
            this.sxId = sxId;
            this.descripcion = descripcion;
        }
    }
}