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
        private string teamId {  get; set; }
        private string nombre { get; set; }
        private char indActivo { get; set; }
        private List<Member> miembros { get; set; }
        private Project proyecto { get; set; }
        private User owner { get; set; }
    }
}
