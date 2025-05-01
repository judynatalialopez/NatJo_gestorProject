using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Project
    {
        private int projId {  get; set; }
        private string nombre { get; set; }
        private string descripcion {  get; set; } 
        private List<Task> tasks { get; set; }
        private Team team { get; set; }
        private DateTime fInicio { get; set; }
        private DateTime fterminacion { get; set;}  
    }
}
