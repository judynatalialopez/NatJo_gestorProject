using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Pais
    {
        public int PaisId { get; set; }
        public string Nombre { get; set; }
        public string Dominio { get; set; }


        public Pais()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Pais(int PaisId, string Nombre, string Dominio)
        {
            this.PaisId = PaisId;
            this.Nombre = Nombre;
            this.Dominio = Dominio;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Pais(string Nombre, string Dominio)
        {
            this.Nombre = Nombre;
            this.Dominio = Dominio;
        }
    }
}
