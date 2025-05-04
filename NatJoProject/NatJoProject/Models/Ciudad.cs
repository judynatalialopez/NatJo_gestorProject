using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Ciudad
    {
        public string cityId { get; set; }
        public string nombre { get; set; }
        public string codPostal { get; set; }
        public Pais pais { get; set; } 

        public Ciudad(String cityId, String nombre, String codPostal, Pais pais) 
        {
            this.cityId = cityId;
            this.nombre = nombre;
            this.codPostal = codPostal;
            this.pais = pais;
        }
    }
}
