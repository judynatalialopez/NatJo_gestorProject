using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Sexo
    {
        public char sxId {  get; set; }
        public string descripcion { get; set; }

        public Sexo(char sxId, string descripcion)
        {
            this.sxId = sxId;
            this.descripcion = descripcion;
        }
    }
}