using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Comment
    {
        private int commId { get; set; }
        private string texto { get; set; }
        private Member autor { get; set; }  
        private DateTime fcomentario { get; set; }
    }
}
