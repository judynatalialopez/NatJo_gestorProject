using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Pais
    {
        public String PaisId { get; set; }
        public String Nombre { get; set; }
        public String Dominio { get; set; }


        public Pais()
        {
        }

        public Pais(string PaisId, string Nombre, string Dominio)
        {
            this.PaisId = PaisId;
            this.Nombre = Nombre;
            this.Dominio = Dominio;
        }
    }
}
