using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Pais
    {
        public String paisId { get; set; }
        public String nombre { get; set; }
        public String dominio { get; set; }


        public Pais()
        {
        }

        public Pais(String paisId, String nombre, String dominio)
        {
            this.paisId = paisId;
            this.nombre = nombre;
            this.dominio = dominio;
        }
    }
}
