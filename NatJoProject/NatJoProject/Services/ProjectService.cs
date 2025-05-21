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
        private readonly TeamService teamService;
        private readonly TaskProjectService task0Service;

        // Constructor vacío si se desea (solo si vas a configurar luego)
        public ProjectService() { }

        // Constructor con inyección manual
        public ProjectService(TeamService teamService, TaskProjectService taskService)
        {
            this.teamService = teamService;
            this.task0Service = taskService;
        }

        public int InsertProject(Project project)
        {
            int newId = 0;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = @"INSERT INTO proyectos (nombre, descripcion, team_id, f_inicio, f_terminacion)
                         VALUES (@nombre, @descripcion, @team_id, @f_inicio, @f_terminacion)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", project.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", project.Descripcion);
                    cmd.Parameters.AddWithValue("@team_id", project.Team != null ? (object)project.Team.TeamId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@f_inicio", project.Finicio);
                    cmd.Parameters.AddWithValue("@f_terminacion", project.Fterminacion);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        newId = (int)cmd.LastInsertedId;
                        project.ProjId = newId; // Opcional, si quieres actualizar el objeto también
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return newId; // retorna el ID o 0 si falló
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

                            project = new Project(
                                Convert.ToInt32(reader["proj_id"].ToString()),
                                reader["nombre"].ToString(),
                                reader["descripcion"].ToString(),
                                Convert.ToDateTime(reader["f_inicio"]),
                                Convert.ToDateTime(reader["f_terminacion"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return project;
        }

        public List<Project> GetProjectsByUserId(string userId)
        {
            var proyectosDict = new Dictionary<int, Project>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = @"
                        SELECT DISTINCT
                          p.proj_id,
                          p.nombre AS proj_nombre,
                          p.descripcion,
                          p.f_inicio,
                          p.f_terminacion,
                          t.team_id,
                          t.nombre AS team_nombre,
                          t.ind_activo,
                          t.owner_id,
                          u.id AS member_id,
                          u.pNombre,
                          u.pApellido,
                          u.login,
                          m.rol_id,
                          r.descripcion AS rol_desc
                        FROM proyectos p
                        JOIN teams t ON t.proj_id = p.proj_id
                        JOIN team_members tm ON tm.team_id = t.team_id
                        JOIN users u ON u.id = tm.member_id
                        JOIN miembros m ON m.user_id = u.id
                        JOIN roles r ON r.rol_id = m.rol_id
                        WHERE t.team_id IN (
                            SELECT team_id FROM team_members WHERE member_id = @user_id
                        );";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int projId = Convert.ToInt32(reader["proj_id"]);

                            if (!proyectosDict.TryGetValue(projId, out Project project))
                            {
                                project = new Project(
                                    projId,
                                    reader["proj_nombre"].ToString(),
                                    reader["descripcion"].ToString(),
                                    Convert.ToDateTime(reader["f_inicio"]),
                                    Convert.ToDateTime(reader["f_terminacion"])
                                );

                                // Crea Team usando el constructor completo
                                var team = new Team(
                                    TeamId: Convert.ToInt32(reader["team_id"]),
                                    Nombre: reader["team_nombre"].ToString(),
                                    IndActivo: Convert.ToChar(reader["ind_activo"]),
                                    Miembros: new List<Member>(),        // se llenará más abajo
                                    Proyecto: project,
                                    Owner: new User(reader["owner_id"].ToString()) // puedes luego convertirlo a Member si quieres
                                );

                                project.Team = team;
                                proyectosDict.Add(projId, project);
                            }

                            // Agrega miembro si no existe
                            var teamMembers = project.Team.Miembros;
                            string memberId = reader["member_id"].ToString();
                            if (!teamMembers.Any(m => m.Id == memberId))
                            {
                                // Crear el Rol
                                Rol rol = new Rol(
                                    RolId: Convert.ToInt32(reader["rol_id"]),
                                    Descripcion: reader["rol_desc"].ToString()
                                );

                                // Crear el Member con el constructor que recibe el Rol
                                var member = new Member(
                                    Id: memberId,
                                    RolUser: rol,
                                    IndOwner: 'N',  // puedes cambiar esto si tienes estos datos en la tabla
                                    IndAdmin: 'N'
                                );

                                // Asignar los campos faltantes
                                member.Pnombre = reader["pNombre"].ToString();
                                member.Papellido = reader["pApellido"].ToString();
                                member.Login = reader["login"].ToString();

                                // Agregar al equipo
                                teamMembers.Add(member);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener proyectos por usuario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return proyectosDict.Values.ToList();
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
                MessageBox.Show("Error al obtener proyectos: " + ex.Message);
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
