using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;
using System.Windows;

namespace NatJoProject.Services
{
    public class ProjectService
    {
        private readonly TeamService teamService = new TeamService(new MemberService());
        private readonly TaskProjectService task0Service = new TaskProjectService();

        public bool InsertProject(Project project)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO proyectos (nombre, descripcion, team_id, f_inicio, f_terminacion)
                                 VALUES (@nombre, @descripcion, @team_id, @f_inicio, @f_terminacion)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", project.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", project.Descripcion);
                    cmd.Parameters.AddWithValue("@team_id", project.Team.TeamId);
                    cmd.Parameters.AddWithValue("@f_inicio", project.Finicio);
                    cmd.Parameters.AddWithValue("@f_terminacion", project.Fterminacion);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Project? GetProjectById(int projId)
        {
            Project? project = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM proyectos WHERE proj_id = @proj_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@proj_id", projId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var teamId = reader["team_id"].ToString();
                            var team = teamService.GetTeamById(teamId);

                            var tasks = task0Service.GetTasksByProjectId(projId);

                            project = new Project(
                                Convert.ToInt32(reader["proj_id"].ToString()),
                                reader["nombre"].ToString(),
                                reader["descripcion"].ToString(),
                                tasks,
                                team ?? new Team(),
                                Convert.ToDateTime(reader["f_inicio"]),
                                Convert.ToDateTime(reader["f_terminacion"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return project;
        }

        public List<Project> GetAllProjects()
        {
            var lista = new List<Project>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM proyectos";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int projId = Convert.ToInt32(reader["proj_id"].ToString());
                        var team = teamService.GetTeamById(reader["team_id"].ToString());
                        var tasks = task0Service.GetTasksByProjectId(projId);

                        var project = new Project(
                            projId,
                            reader["nombre"].ToString(),
                            reader["descripcion"].ToString(),
                            tasks,
                            team ?? new Team(),
                            Convert.ToDateTime(reader["f_inicio"]),
                            Convert.ToDateTime(reader["f_terminacion"])
                        );

                        lista.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener proyectos: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public bool UpdateProject(Project project)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE proyectos
                                 SET nombre = @nombre, descripcion = @descripcion, team_id = @team_id,
                                     f_inicio = @f_inicio, f_terminacion = @f_terminacion
                                 WHERE proj_id = @proj_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", project.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", project.Descripcion);
                    cmd.Parameters.AddWithValue("@team_id", project.Team.TeamId);
                    cmd.Parameters.AddWithValue("@f_inicio", project.Finicio);
                    cmd.Parameters.AddWithValue("@f_terminacion", project.Fterminacion);
                    cmd.Parameters.AddWithValue("@proj_id", project.ProjId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteProject(int projId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM proyectos WHERE proj_id = @proj_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@proj_id", projId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
