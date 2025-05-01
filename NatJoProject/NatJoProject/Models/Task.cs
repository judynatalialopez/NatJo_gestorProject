using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Task
    {
        private int taskId {  get; set; }
        private string titulo { get; set; }
        private string descripcion { get; set; }
        private List<Member> responsable {  get; set; }
        private TaskEstado estado { get; set; }
        private DateTime fEntrerga { get; set; }
        private List<Comment> comentarios { get; set; }

    }
}
