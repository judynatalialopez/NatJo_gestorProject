using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Ciudad
    {
        public string cityId { get; set; }
        public string nombre { get; set; }
        public string codPostal { get; set; }
        public Pais pais { get; set; } 

        public Ciudad(string cityId, string nombre, string codPostal, Pais pais) 
        {
            this.cityId = cityId;
            this.nombre = nombre;
            this.codPostal = codPostal;
            this.pais = pais;
        }
    }
}
