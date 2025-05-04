using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Project
    {
        public int projId {  get; set; }
        public string nombre { get; set; }
        public string descripcion {  get; set; } 
        public List<Task> tasks { get; set; }
        public Team team { get; set; }
        public DateTime fInicio { get; set; }
        public DateTime fterminacion { get; set;}

        public Project(int projId, string nombre, string descripcion, List<Task> tasks, Team team, DateTime fInicio, DateTime fterminacion) 
        {
            this.projId = projId;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.tasks = tasks;
            this.team = team;
            this.fInicio = fInicio;
            this.fterminacion = fterminacion;
        }
    }
}
