using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Dashboard
    {
        public string DboardId { set; get; }
        public User Usuario { set; get; }
        public List<Project> Proyectos {  set; get; }

        public Dashboard()
        {
        }
        public Dashboard(string DboardId, User Usuario, List<Project> Proyectos) 
        {
            this.DboardId = DboardId;
            this.Usuario = Usuario;
            this.Proyectos = Proyectos;
        }
    }
}
