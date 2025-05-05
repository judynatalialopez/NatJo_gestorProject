using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Comment
    {
        public int commId { get; set; }
        public string texto { get; set; }
        public Member autor { get; set; }  
        public DateTime fcomentario { get; set; }

        public Comment()
        {
        }
        public Comment(int commId, string texto, Member autor, DateTime fcomentario) 
        {
            this.commId = commId;
            this.texto = texto;
            this.autor = autor;
            this.fcomentario = fcomentario;
        }
    }
}
