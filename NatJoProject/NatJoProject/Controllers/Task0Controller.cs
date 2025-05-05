using System;
using System.Collections.Generic;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class Task0Controller
    {
        private readonly Task0Service task0Service = new Task0Service();

        // Método para obtener tareas por proyecto
        public void GetTasksByProjectId(string projId)
        {
            var tasks = task0Service.GetTasksByProjectId(projId);

            Console.ForegroundColor = tasks != null && tasks.Count > 0 ? ConsoleColor.Cyan : ConsoleColor.Yellow;
            Console.WriteLine(tasks != null && tasks.Count > 0
                ? $"Se encontraron {tasks.Count} tareas para el proyecto con ID {projId}:"
                : $"No se encontraron tareas para el proyecto con ID {projId}.");

            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.taskId} | Título: {task.titulo} | Estado: {task.estado.descripcion}");
                }
            }

            Console.ResetColor();
        }

        public void InsertTask0(Task0 task)
        {
            bool result = task0Service.InsertTask0(task);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {task.taskId} insertada con éxito."
                : $"[ERROR] No se pudo insertar la Tarea {task.taskId}.");
            Console.ResetColor();
        }

        public void GetTask0ById(string taskId)
        {
            var task = task0Service.GetTask0ById(taskId);
            if (task != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Tarea encontrada: {task.titulo} | Estado: {task.estado.descripcion}");
                Console.WriteLine($"Descripción: {task.descripcion}");
                Console.WriteLine($"Fecha Entrega: {task.fEntrerga}");

                Console.WriteLine("Responsables:");
                foreach (var m in task.responsable)
                    Console.WriteLine($" - {m.pNombre} {m.pApellido}");

                Console.WriteLine("Comentarios:");
                foreach (var c in task.comentarios)
                    Console.WriteLine($" - [{c.fcomentario}] {c.autor}: {c.texto}");

                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró la tarea con ID {taskId}.");
                Console.ResetColor();
            }
        }

        public void UpdateTask0(Task0 task)
        {
            bool result = task0Service.UpdateTask0(task);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {task.taskId} actualizada con éxito."
                : $"[ERROR] No se pudo actualizar la tarea {task.taskId}.");
            Console.ResetColor();
        }

        public void DeleteTask0(string taskId)
        {
            bool result = task0Service.DeleteTask0(taskId);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {taskId} eliminada con éxito."
                : $"[ERROR] No se pudo eliminar la tarea {taskId}.");
            Console.ResetColor();
        }

        public void GetAllTask0s()
        {
            var tasks = task0Service.GetAllTask0s();

            Console.ForegroundColor = tasks.Count > 0 ? ConsoleColor.Cyan : ConsoleColor.Yellow;
            Console.WriteLine(tasks.Count > 0
                ? $"Se encontraron {tasks.Count} tareas:"
                : "No se encontraron tareas.");

            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.taskId} | Título: {task.titulo} | Estado: {task.estado.descripcion}");
            }

            Console.ResetColor();
        }
    }
}
