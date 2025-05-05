using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Pais
    {
        public String paisId { get; set; }
        public String nombre { get; set; }
        public String dominio { get; set; }


        public Pais()
        {
        }

        public Pais(string paisId, string nombre, string dominio)
        {
            this.paisId = paisId;
            this.nombre = nombre;
            this.dominio = dominio;
        }
    }
}
