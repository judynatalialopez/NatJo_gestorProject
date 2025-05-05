using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NatJoProject.Models
{
    public class Task0
    {
        public string taskId {  get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public List<Member> responsable {  get; set; }
        public TaskEstado estado { get; set; }
        public DateTime fEntrerga { get; set; }
        public List<Comment> comentarios { get; set; }

        public Task0()
        {
        }
        public Task0 (string taskId, string titulo, string descripcion, List<Member> responsable, TaskEstado estado, DateTime fEntrerga, List<Comment> comentarios)
        {
            this.taskId = taskId;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.responsable = responsable;
            this.estado = estado;
            this.fEntrerga = fEntrerga;
            this.comentarios = comentarios;
        }
    }
}
