using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Sexo
    {
        public int SxId {  get; set; }
        public string Descripcion { get; set; }

        public Sexo()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Sexo(int SxId, string Descripcion)
        {
            this.SxId = SxId;
            this.Descripcion = Descripcion;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Sexo(string Descripcion)
        {
            this.Descripcion = Descripcion;
        }
    }
}