using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Sexo
    {
        public string SxId {  get; set; }
        public string Descripcion { get; set; }

        public Sexo()
        {
        }
        public Sexo(string SxId, string Descripcion)
        {
            this.SxId = SxId;
            this.Descripcion = Descripcion;
        }
    }
}