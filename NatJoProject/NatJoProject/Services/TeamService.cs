using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class TeamService
    {
        private readonly MemberService memberService = new MemberService();
        private readonly ProjectService projectService = new ProjectService();
        private readonly UserService userService = new UserService();

        public bool InsertTeam(Team team)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO teams (team_id, nombre, ind_activo, project_id, owner_id)
                                 VALUES (@team_id, @nombre, @ind_activo, @project_id, @owner_id)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", team.teamId);
                    cmd.Parameters.AddWithValue("@nombre", team.nombre);
                    cmd.Parameters.AddWithValue("@ind_activo", team.indActivo);
                    cmd.Parameters.AddWithValue("@project_id", team.proyecto.projId);
                    cmd.Parameters.AddWithValue("@owner_id", team.owner.id);

                    result = cmd.ExecuteNonQuery() > 0;
                }

                if (result && team.miembros != null)
                {
                    foreach (var miembro in team.miembros)
                    {
                        string miembroQuery = @"INSERT INTO team_members (team_id, member_id)
                                                VALUES (@team_id, @member_id)";

                        using (var cmd = new MySqlCommand(miembroQuery, conexion))
                        {
                            cmd.Parameters.AddWithValue("@team_id", team.teamId);
                            cmd.Parameters.AddWithValue("@member_id", miembro.id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar team: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Team? GetTeamById(string teamId)
        {
            Team? team = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM teams WHERE team_id = @team_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", teamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string projectId = reader["project_id"].ToString();
                            string ownerId = reader["owner_id"].ToString();

                            Project? proyecto = projectService.GetProjectById(projectId);
                            User? owner = userService.GetUserById(ownerId);

                            team = new Team
                            {
                                teamId = reader["team_id"].ToString(),
                                nombre = reader["nombre"].ToString(),
                                indActivo = Convert.ToChar(reader["ind_activo"]),
                                proyecto = proyecto!,
                                owner = owner!,
                                miembros = new List<Member>()
                            };
                        }
                    }
                }

                if (team != null)
                {
                    string miembrosQuery = "SELECT member_id FROM team_members WHERE team_id = @team_id";

                    using (var cmd = new MySqlCommand(miembrosQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@team_id", teamId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string memberId = reader["member_id"].ToString();
                                Member? miembro = memberService.GetMemberById(memberId);
                                if (miembro != null)
                                    team.miembros.Add(miembro);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener team: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return team;
        }

        public List<Team> GetAllTeams()
        {
            var lista = new List<Team>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT team_id FROM teams";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string teamId = reader["team_id"].ToString();
                        Team? team = GetTeamById(teamId);
                        if (team != null)
                            lista.Add(team);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener lista de equipos: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public bool UpdateTeam(Team team)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE teams 
                                 SET nombre = @nombre, ind_activo = @ind_activo, 
                                     project_id = @project_id, owner_id = @owner_id 
                                 WHERE team_id = @team_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", team.nombre);
                    cmd.Parameters.AddWithValue("@ind_activo", team.indActivo);
                    cmd.Parameters.AddWithValue("@project_id", team.proyecto.projId);
                    cmd.Parameters.AddWithValue("@owner_id", team.owner.id);
                    cmd.Parameters.AddWithValue("@team_id", team.teamId);

                    result = cmd.ExecuteNonQuery() > 0;
                }

                // Actualizar miembros: primero borramos todos y luego insertamos los nuevos
                string deleteQuery = "DELETE FROM team_members WHERE team_id = @team_id";
                using (var cmd = new MySqlCommand(deleteQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", team.teamId);
                    cmd.ExecuteNonQuery();
                }

                foreach (var miembro in team.miembros)
                {
                    string insertQuery = @"INSERT INTO team_members (team_id, member_id) 
                                           VALUES (@team_id, @member_id)";
                    using (var cmd = new MySqlCommand(insertQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@team_id", team.teamId);
                        cmd.Parameters.AddWithValue("@member_id", miembro.id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar team: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteTeam(string teamId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                // Borrar miembros asociados
                string deleteMembers = "DELETE FROM team_members WHERE team_id = @team_id";
                using (var cmd = new MySqlCommand(deleteMembers, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", teamId);
                    cmd.ExecuteNonQuery();
                }

                // Borrar el team
                string deleteTeam = "DELETE FROM teams WHERE team_id = @team_id";
                using (var cmd = new MySqlCommand(deleteTeam, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", teamId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar team: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
