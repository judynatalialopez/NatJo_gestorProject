using System;
using System.Collections.Generic;
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
                Console.WriteLine($"Dashboard: {d.dboardId} - Usuario: {d.usuario.pNombre} {d.usuario.pApellido}");
                Console.WriteLine("Proyectos:");
                foreach (var p in d.proyectos)
                    Console.WriteLine($"\t{p.nombre} - {p.descripcion}");
            }
        }

        public void BuscarPorId(string id)
        {
            var dashboard = dashboardService.GetDashboardById(id);
            if (dashboard != null)
            {
                Console.WriteLine($"Dashboard: {dashboard.dboardId} - Usuario: {dashboard.usuario.pNombre} {dashboard.usuario.pApellido}");
                Console.WriteLine("Proyectos:");
                foreach (var p in dashboard.proyectos)
                    Console.WriteLine($"\t{p.nombre} - {p.descripcion}");
            }
            else
            {
                Console.WriteLine("Dashboard no encontrado.");
            }
        }

        public void EliminarDashboard(string id)
        {
            if (dashboardService.DeleteDashboard(id))
                Console.WriteLine("Dashboard eliminado correctamente.");
            else
                Console.WriteLine("No se pudo eliminar el dashboard.");
        }
    }
}
