using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;
using System.Windows;

namespace NatJoProject.Services
{
    public class TeamService
    {
        private readonly MemberService memberService;
        private readonly ProjectService projectService;
        private readonly UserService userService;

        // Constructor vacío (por defecto)
        public TeamService(){}

        // Constructor con inyección manual
        public TeamService(MemberService memberService, ProjectService projectService, UserService userService)
        {
            this.memberService = memberService;
            this.projectService = projectService;
            this.userService = userService;
        }

        public int InsertTeam(Team team)
        {
            var conexion = ConexionDB.conectar();
            int insertedTeamId = 0;

            try
            {
                string query = @"INSERT INTO teams (nombre, ind_activo, proj_id, owner_id)
                         VALUES (@nombre, @ind_activo, @project_id, @owner_id)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", team.Nombre);
                    cmd.Parameters.AddWithValue("@ind_activo", team.IndActivo);
                    cmd.Parameters.AddWithValue("@project_id", team.Proyecto.ProjId);
                    cmd.Parameters.AddWithValue("@owner_id", team.Owner.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        insertedTeamId = (int)cmd.LastInsertedId;
                        team.TeamId = insertedTeamId;
                    }
                }

                if (insertedTeamId > 0 && team.Miembros != null)
                {
                    foreach (var miembro in team.Miembros)
                    {
                        string miembroQuery = @"INSERT INTO team_members (team_id, member_id)
                                        VALUES (@team_id, @member_id)";

                        using (var cmd = new MySqlCommand(miembroQuery, conexion))
                        {
                            cmd.Parameters.AddWithValue("@team_id", insertedTeamId);
                            cmd.Parameters.AddWithValue("@member_id", miembro.Id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar team: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return insertedTeamId;
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
                            int projectId = Convert.ToInt32(reader["project_id"].ToString());
                            string ownerId = reader["owner_id"].ToString();

                            Project? proyecto = projectService.GetProjectById(projectId);
                            User? owner = userService.GetUserById(ownerId);

                            team = new Team
                            {
                                TeamId = Convert.ToInt32(reader["team_id"].ToString()),
                                Nombre = reader["nombre"].ToString(),
                                IndActivo = Convert.ToChar(reader["ind_activo"]),
                                Proyecto = proyecto!,
                                Owner = owner!,
                                Miembros = new List<Member>()
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
                                    team.Miembros.Add(miembro);
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
                    cmd.Parameters.AddWithValue("@nombre", team.Nombre);
                    cmd.Parameters.AddWithValue("@ind_activo", team.IndActivo);
                    cmd.Parameters.AddWithValue("@project_id", team.Proyecto.ProjId);
                    cmd.Parameters.AddWithValue("@owner_id", team.Proyecto.ProjId);
                    cmd.Parameters.AddWithValue("@team_id", team.TeamId);

                    result = cmd.ExecuteNonQuery() > 0;
                }

                // Actualizar miembros: primero borramos todos y luego insertamos los nuevos
                string deleteQuery = "DELETE FROM team_members WHERE team_id = @team_id";
                using (var cmd = new MySqlCommand(deleteQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@team_id", team.TeamId);
                    cmd.ExecuteNonQuery();
                }

                foreach (var miembro in team.Miembros)
                {
                    string insertQuery = @"INSERT INTO team_members (team_id, member_id) 
                                           VALUES (@team_id, @member_id)";
                    using (var cmd = new MySqlCommand(insertQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@team_id", team.TeamId);
                        cmd.Parameters.AddWithValue("@member_id", miembro.Id);
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
