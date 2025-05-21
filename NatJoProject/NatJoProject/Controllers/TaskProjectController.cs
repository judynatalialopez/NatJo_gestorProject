using System;
using System.Collections.Generic;
using System.Windows;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class TaskProjectController
    {
        private readonly TaskProjectService task0Service = new TaskProjectService();

        // Método para obtener tareas por proyecto
        public void GetTasksByProjectId(int projId)
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
                    Console.WriteLine($"ID: {task.TaskId} | Título: {task.Titulo} | Estado: {task.Estado.Descripcion}");
                }
            }

            Console.ResetColor();
        }

        public bool InsertTask0(TaskProject task)
        {
            bool result = task0Service.InsertTaskProject(task);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {task.TaskId} insertada con éxito."
                : $"[ERROR] No se pudo insertar la Tarea {task.TaskId}.");
            Console.ResetColor();

            return result;
        }

        public void GetTask0ById(int taskId)
        {
            var task = task0Service.GetTaskProjectById(taskId);
            if (task != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Tarea encontrada: {task.Titulo} | Estado: {task.Estado.Descripcion}");
                Console.WriteLine($"Descripción: {task.Descripcion}");
                Console.WriteLine($"Fecha Entrega: {task.Fentrerga}");

                Console.WriteLine("Responsables:");
                foreach (var m in task.Responsable)
                    Console.WriteLine($" - {m.Pnombre} {m.Papellido}");

                Console.WriteLine("Comentarios:");
                foreach (var c in task.Comentarios)
                    Console.WriteLine($" - [{c.Fcomentario}] {c.Autor}: {c.Texto}");

                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró la tarea con ID {taskId}.");
                Console.ResetColor();
            }
        }

        public void UpdateTask0(TaskProject task)
        {
            bool result = task0Service.UpdateTaskProject(task);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {task.TaskId} actualizada con éxito."
                : $"[ERROR] No se pudo actualizar la tarea {task.TaskId}.");
            Console.ResetColor();
        }

        public void DeleteTask0(int taskId)
        {
            bool result = task0Service.DeleteTaskProject(taskId);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result
                ? $"[INFO] Tarea {taskId} eliminada con éxito."
                : $"[ERROR] No se pudo eliminar la tarea {taskId}.");
            Console.ResetColor();
        }

        public void GetAllTaskProjects()
        {
            var tasks = task0Service.GetAllTaskProjects();

            Console.ForegroundColor = tasks.Count > 0 ? ConsoleColor.Cyan : ConsoleColor.Yellow;
            Console.WriteLine(tasks.Count > 0
                ? $"Se encontraron {tasks.Count} tareas:"
                : "No se encontraron tareas.");

            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.TaskId} | Título: {task.Titulo} | Estado: {task.Estado.Descripcion}");
            }

            Console.ResetColor();
        }
    }
}
