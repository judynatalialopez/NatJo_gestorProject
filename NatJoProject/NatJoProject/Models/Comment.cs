using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Comment
    {
        public int CommId { get; set; }
        public string Texto { get; set; }
        public Member Autor { get; set; }  
        public DateTime Fcomentario { get; set; }

        public Comment()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public Comment(int CommId, string Texto, Member Autor, DateTime Fcomentario) 
        {
            this.CommId = CommId;
            this.Texto = Texto;
            this.Autor = Autor;
            this.Fcomentario = Fcomentario;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public Comment(string Texto, Member Autor, DateTime Fcomentario)
        {
            this.Texto = Texto;
            this.Autor = Autor;
            this.Fcomentario = Fcomentario;
        }
    }
}
