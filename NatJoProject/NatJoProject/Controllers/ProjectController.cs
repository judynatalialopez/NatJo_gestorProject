using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class ProjectController
    {
        private readonly ProjectService projectService = new ProjectService();

        public int InsertProject(Project project)
        {
            int id = projectService.InsertProject(project);

            if (id > 0)
            {
                Console.WriteLine($"[INFO] Proyecto insertado correctamente con ID: {id}.");
            }
            else
            {
                Console.WriteLine($"[ERROR] No se pudo insertar el proyecto.");
            }

            return id;
        }

        public Project? GetProjectById(int projId)
        {
            var project = projectService.GetProjectById(projId);
            if (project != null)
            {
                Console.WriteLine($"Proyecto: {project.Nombre} - {project.Descripcion} - {project.Finicio:yyyy-MM-dd} a {project.Fterminacion:yyyy-MM-dd}");
            }
            else
            {
                Console.WriteLine($"[WARNING] Proyecto con ID {projId} no encontrado.");
            }

            return project;
        }

        public void GetAllProjects()
        {
            var projects = projectService.GetAllProjects();
            if (projects.Count > 0)
            {
                Console.WriteLine($"Se encontraron {projects.Count} proyectos:");
                foreach (var p in projects)
                {
                    Console.WriteLine($"- {p.ProjId}: {p.Nombre} ({p.Finicio:yyyy-MM-dd})");
                }
            }
            else
            {
                Console.WriteLine("No hay proyectos registrados.");
            }
        }
        public List<Project> MostrarProyectosPorUsuario(string userId)
        {
            var projects = projectService.GetProjectsByUserId(userId);

            if (projects.Count > 0)
            {
                Console.WriteLine($"Se encontraron {projects.Count} proyectos para el usuario {userId}:");
                foreach (var p in projects)
                {
                    Console.WriteLine($"- {p.ProjId}: {p.Nombre} ({p.Finicio:yyyy-MM-dd})");
                }
            }
            else
            {
                Console.WriteLine($"No hay proyectos registrados para el usuario {userId}.");
            }

            return projects;
        }

        public void UpdateProject(Project project)
        {
            if (projectService.UpdateProject(project))
                Console.WriteLine($"[INFO] Proyecto {project.ProjId} actualizado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo actualizar el proyecto {project.ProjId}.");
        }

        public void DeleteProject(int projId)
        {
            if (projectService.DeleteProject(projId))
                Console.WriteLine($"[INFO] Proyecto {projId} eliminado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo eliminar el proyecto {projId}.");
        }
    }
}
