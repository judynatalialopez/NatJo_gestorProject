using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Ciudad
    {
        public int CityId { get; set; }
        public string Nombre { get; set; }
        public string CodPostal { get; set; }
        public Pais Pais { get; set; }

        public Ciudad()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Ciudad(int CityId, string Nombre, string CodPostal, Pais Pais) 
        {
            this.CityId = CityId;
            this.Nombre = Nombre;
            this.CodPostal = CodPostal;
            this.Pais = Pais;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Ciudad(string Nombre, string CodPostal, Pais Pais)
        {
            this.Nombre = Nombre;
            this.CodPostal = CodPostal;
            this.Pais = Pais;
        }
    }
}
