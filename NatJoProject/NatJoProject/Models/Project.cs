using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Project
    {
        public int ProjId {  get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; } 
        public List<TaskProject> Tasks { get; set; }
        public Team? Team { get; set; }
        public DateTime Finicio { get; set; }
        public DateTime Fterminacion { get; set;}

        public Project()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Project(int ProjId, string Nombre, string Descripcion, DateTime Finicio, DateTime Fterminacion)
        {
            this.ProjId = ProjId;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Finicio = Finicio;
            this.Fterminacion = Fterminacion;
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Project(int ProjId, string Nombre, string Descripcion, List<TaskProject> Tasks, Team Team, DateTime Finicio, DateTime Fterminacion) 
        {
            this.ProjId = ProjId;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Tasks = Tasks;
            this.Team = Team;
            this.Finicio = Finicio;
            this.Fterminacion = Fterminacion;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Project(string Nombre, string Descripcion, Team Team, DateTime Finicio, DateTime Fterminacion)
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Team = Team;
            this.Finicio = Finicio;
            this.Fterminacion = Fterminacion;
        }
    }
}
