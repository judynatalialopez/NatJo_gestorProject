using System;
using System.Collections.Generic;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class CommentController
    {
        private readonly CommentService commentService = new CommentService();

        public void InsertComment(Comment comment)
        {
            bool result = commentService.InsertComment(comment);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? $"[INFO] Comentario insertado." : $"[ERROR] No se pudo insertar.");
            Console.ResetColor();
        }

        public void GetCommentById(int id)
        {
            var comment = commentService.GetCommentById(id);
            if (comment != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {comment.commId}, Texto: {comment.texto}, Autor: {comment.autor.pNombre}, Fecha: {comment.fcomentario}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Comentario no encontrado.");
            }
            Console.ResetColor();
        }

        public void GetAllComments()
        {
            var lista = commentService.GetAllComments();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Listado de Comentarios:");
            foreach (var c in lista)
            {
                Console.WriteLine($"ID: {c.commId} | Texto: {c.texto} | Autor: {c.autor.pNombre} {c.autor.pApellido} | Fecha: {c.fcomentario}");
            }
            Console.ResetColor();
        }

        public void UpdateComment(Comment comment)
        {
            bool result = commentService.UpdateComment(comment);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? "[INFO] Comentario actualizado." : "[ERROR] No se pudo actualizar.");
            Console.ResetColor();
        }

        public void DeleteComment(int id)
        {
            bool result = commentService.DeleteComment(id);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? "[INFO] Comentario eliminado." : "[ERROR] No se pudo eliminar.");
            Console.ResetColor();
        }
    }
}
