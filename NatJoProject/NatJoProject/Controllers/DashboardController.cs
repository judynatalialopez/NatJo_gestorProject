using System;
using System.Collections.Generic;
using System.Windows;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class DashboardController
    {
        private readonly DashboardService dashboardService = new DashboardService();

        public void CrearDashboard(Dashboard dashboard)
        {
            if (dashboardService.InsertDashboard(dashboard))
                Console.WriteLine("Dashboard creado exitosamente.");
            else
                Console.WriteLine("Error al crear dashboard.");
        }

        public void MostrarTodos()
        {
            var dashboards = dashboardService.GetAllDashboards();
            foreach (var d in dashboards)
            {
                Console.WriteLine($"Dashboard: {d.DboardId} - Usuario: {d.Usuario.Pnombre} {d.Usuario.Papellido}");
                Console.WriteLine("Proyectos:");
                foreach (var p in d.Proyectos)
                    Console.WriteLine($"\t{p.Nombre} - {p.Descripcion}");
            }
        }

        public List<Project> GetProjectsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<Project>();


            return dashboardService.GetProjectsByUserId(userId);
        }

        public void BuscarPorId(int id)
        {
            var dashboard = dashboardService.GetDashboardById(id);
            if (dashboard != null)
            {
                Console.WriteLine($"Dashboard: {dashboard.DboardId} - Usuario: {dashboard.Usuario.Pnombre} {dashboard.Usuario.Papellido}");
                Console.WriteLine("Proyectos:");
                foreach (var p in dashboard.Proyectos)
                    Console.WriteLine($"\t{p.Nombre} - {p.Descripcion}");
            }
            else
            {
                Console.WriteLine("Dashboard no encontrado.");
            }
        }

        public void EliminarDashboard(int id)
        {
            if (dashboardService.DeleteDashboard(id))
                Console.WriteLine("Dashboard eliminado correctamente.");
            else
                Console.WriteLine("No se pudo eliminar el dashboard.");
        }
    }
}
