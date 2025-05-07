using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Project
    {
        public string ProjId {  get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; } 
        public List<TaskProject> Tasks { get; set; }
        public Team Team { get; set; }
        public DateTime Finicio { get; set; }
        public DateTime Fterminacion { get; set;}

        public Project()
        {
        }
        public Project(string ProjId, string Nombre, string Descripcion, List<TaskProject> Tasks, Team Team, DateTime Finicio, DateTime Fterminacion) 
        {
            this.ProjId = ProjId;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Tasks = Tasks;
            this.Team = Team;
            this.Finicio = Finicio;
            this.Fterminacion = Fterminacion;
        }
    }
}
