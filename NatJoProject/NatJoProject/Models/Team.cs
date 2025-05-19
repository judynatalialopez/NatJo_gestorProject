using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Team
    {
        public int TeamId {  get; set; }
        public string Nombre { get; set; }
        public char IndActivo { get; set; }
        public List<Member> Miembros { get; set; }
        public Project Proyecto { get; set; }
        public User Owner { get; set; }

        public Team()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Team (int TeamId, string Nombre, char IndActivo, List<Member> Miembros, Project Proyecto, User Owner)
        {
            this.TeamId = TeamId;
            this.Nombre = Nombre;
            this.IndActivo = IndActivo;
            this.Miembros = Miembros;
            this.Proyecto = Proyecto;
            this.Owner = Owner;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Team(string Nombre, char IndActivo, List<Member> Miembros, Project Proyecto, User Owner)
        {
            this.Nombre = Nombre;
            this.IndActivo = IndActivo;
            this.Miembros = Miembros;
            this.Proyecto = Proyecto;
            this.Owner = Owner;
        }
    }
}
