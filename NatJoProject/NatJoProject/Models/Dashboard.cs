using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Dashboard
    {
        public string dboardId { set; get; }
        public User usuario { set; get; }
        public List<Project> proyectos {  set; get; } 

        public Dashboard(string dboardId, User usuario, List<Project> proyectos) 
        {
            this.dboardId = dboardId;
            this.usuario = usuario;
            this.proyectos = proyectos;
        }
    }
}
