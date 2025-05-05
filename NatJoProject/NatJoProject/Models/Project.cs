using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Project
    {
        public string projId {  get; set; }
        public string nombre { get; set; }
        public string descripcion {  get; set; } 
        public List<Task0> Task0s { get; set; }
        public Team team { get; set; }
        public DateTime fInicio { get; set; }
        public DateTime fterminacion { get; set;}

        public Project()
        {
        }
        public Project(string projId, string nombre, string descripcion, List<Task0> Task0s, Team team, DateTime fInicio, DateTime fterminacion) 
        {
            this.projId = projId;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.Task0s = Task0s;
            this.team = team;
            this.fInicio = fInicio;
            this.fterminacion = fterminacion;
        }
    }
}
