using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Team
    {
        public string teamId {  get; set; }
        public string nombre { get; set; }
        public char indActivo { get; set; }
        public List<Member> miembros { get; set; }
        public Project proyecto { get; set; }
        public User owner { get; set; }

        public Team (string teamId, string nombre, char indActivo, List<Member> miembros, Project proyecto, User owner)
        {
            this.teamId = teamId;
            this.nombre = nombre;
            this.indActivo = indActivo;
            this.miembros = miembros;
            this.proyecto = proyecto;
            this.owner = owner;
        }
    }
}
