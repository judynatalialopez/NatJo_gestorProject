using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Services
{
    public class CiudadService
    {
        string stringConex = "server=localhost; user=root; database=natjoproject; password=; port=3306;";

        public Ciudad GetCiudad(string cityId){
            Ciudad ciudad = new Ciudad();
            string query = "select * from ciudad where cityId =" + cityId;


        }
    }
}
