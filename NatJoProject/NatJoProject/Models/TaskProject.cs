using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NatJoProject.Models
{
    public class TaskProject
    {
        public int TaskId {  get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<Member> Responsable {  get; set; }
        public TaskEstado Estado { get; set; }
        public DateTime Fentrerga { get; set; }
        public List<Comment> Comentarios { get; set; }

        public TaskProject()
        {
        }

        //METODO CONSTRUCTOR QUE RECIBE ID
        public TaskProject (int TaskId, string Titulo, string Descripcion, List<Member> Responsable, TaskEstado Estado, DateTime Fentrerga, List<Comment> Comentarios)
        {
            this.TaskId = TaskId;
            this.Titulo = Titulo;
            this.Descripcion = Descripcion;
            this.Responsable = Responsable;
            this.Estado = Estado;
            this.Fentrerga = Fentrerga;
            this.Comentarios = Comentarios;
        }

        //METODO CONSTRUCTOR QUE NO RECIBE ID
        public TaskProject(string Titulo, string Descripcion, List<Member> Responsable, TaskEstado Estado, DateTime Fentrerga)
        {
            this.Titulo = Titulo;
            this.Descripcion = Descripcion;
            this.Responsable = Responsable;
            this.Estado = Estado;
            this.Fentrerga = Fentrerga;
            this.Comentarios = new List<Comment>(); // inicializo para evitar null reference
        }
    }
}
