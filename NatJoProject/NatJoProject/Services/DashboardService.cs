using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class DashboardService
    {
        private readonly UserService userService = new UserService();
        private readonly ProjectService projectService = new ProjectService();

        public bool InsertDashboard(Dashboard dashboard)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO dashboards (dboard_id, user_id) 
                                 VALUES (@dboard_id, @user_id)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@dboard_id", dashboard.DboardId);
                    cmd.Parameters.AddWithValue("@user_id", dashboard.Usuario.Id);
                    result = cmd.ExecuteNonQuery() > 0;
                }

                // Asignar proyectos al dashboard
                foreach (var proj in dashboard.Proyectos)
                {
                    string relQuery = @"INSERT INTO dashboard_proyectos (dboard_id, proj_id)
                                        VALUES (@dboard_id, @proj_id)";
                    using (var cmd = new MySqlCommand(relQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@dboard_id", dashboard.DboardId);
                        cmd.Parameters.AddWithValue("@proj_id", proj.ProjId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar dashboard: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Dashboard? GetDashboardById(string dboardId)
        {
            Dashboard? dashboard = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM dashboards WHERE dboard_id = @dboard_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@dboard_id", dboardId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userId = reader["user_id"].ToString();
                            User? user = userService.GetUserById(userId);
                            dashboard = new Dashboard(dboardId, user!, new List<Project>());
                        }
                    }
                }

                // Cargar proyectos
                if (dashboard != null)
                {
                    dashboard.Proyectos = GetProjectsByDashboardId(dboardId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener dashboard: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return dashboard;
        }

        private List<Project> GetProjectsByDashboardId(string dboardId)
        {
            var proyectos = new List<Project>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = @"SELECT proj_id FROM dashboard_proyectos WHERE dboard_id = @dboard_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@dboard_id", dboardId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projId = reader["proj_id"].ToString();
                            var proyecto = projectService.GetProjectById(projId);
                            if (proyecto != null)
                                proyectos.Add(proyecto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener proyectos del dashboard: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return proyectos;
        }

        public List<Dashboard> GetAllDashboards()
        {
            var dashboards = new List<Dashboard>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM dashboards";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dboardId = reader["dboard_id"].ToString();
                        string userId = reader["user_id"].ToString();
                        User? user = userService.GetUserById(userId);

                        if (user != null)
                        {
                            dashboards.Add(new Dashboard(dboardId, user, GetProjectsByDashboardId(dboardId)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar dashboards: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return dashboards;
        }

        public bool DeleteDashboard(string dboardId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                // Primero borrar las relaciones
                string deleteProjects = "DELETE FROM dashboard_proyectos WHERE dboard_id = @dboard_id";
                using (var cmd = new MySqlCommand(deleteProjects, conexion))
                {
                    cmd.Parameters.AddWithValue("@dboard_id", dboardId);
                    cmd.ExecuteNonQuery();
                }

                // Luego borrar el dashboard
                string deleteDashboard = "DELETE FROM dashboards WHERE dboard_id = @dboard_id";
                using (var cmd = new MySqlCommand(deleteDashboard, conexion))
                {
                    cmd.Parameters.AddWithValue("@dboard_id", dboardId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar dashboard: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
