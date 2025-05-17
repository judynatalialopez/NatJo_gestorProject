using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Dashboard
    {
        public int DboardId { set; get; }
        public User Usuario { set; get; }
        public List<Project> Proyectos {  set; get; }

        public Dashboard()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Dashboard(int DboardId, User Usuario, List<Project> Proyectos) 
        {
            this.DboardId = DboardId;
            this.Usuario = Usuario;
            this.Proyectos = Proyectos;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Dashboard(User Usuario, List<Project> Proyectos)
        {
            this.Usuario = Usuario;
            this.Proyectos = Proyectos;
        }
    }
}
