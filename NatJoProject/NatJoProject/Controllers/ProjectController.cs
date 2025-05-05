using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine($"[INFO] Proyecto {project.projId} insertado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo insertar el proyecto {project.projId}.");
        }

        public void GetProjectById(string projId)
        {
            var project = projectService.GetProjectById(projId);
            if (project != null)
            {
                Console.WriteLine($"Proyecto: {project.nombre} - {project.descripcion} - {project.fInicio:yyyy-MM-dd} a {project.fterminacion:yyyy-MM-dd}");
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
                    Console.WriteLine($"- {p.projId}: {p.nombre} ({p.fInicio:yyyy-MM-dd})");
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
                Console.WriteLine($"[INFO] Proyecto {project.projId} actualizado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo actualizar el proyecto {project.projId}.");
        }

        public void DeleteProject(string projId)
        {
            if (projectService.DeleteProject(projId))
                Console.WriteLine($"[INFO] Proyecto {projId} eliminado correctamente.");
            else
                Console.WriteLine($"[ERROR] No se pudo eliminar el proyecto {projId}.");
        }
    }
}
