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

        public void InsertProject(Project project)
        {
            if (projectService.InsertProject(project))
                Console.WriteLine($"[INFO] Proyecto {project.ProjId} insertado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo insertar el proyecto {project.ProjId}.");
        }

        public void GetProjectById(int projId)
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
