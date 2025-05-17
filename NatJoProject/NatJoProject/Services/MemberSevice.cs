using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;
using System.Windows;

namespace NatJoProject.Services
{
    public class MemberService
    {
        private readonly UserService userService = new UserService();
        private readonly RolService rolService = new RolService();

        public bool InsertMember(Member member)
        {
            // Primero insertamos al User
            if (!userService.InsertUser(member))
                return false;

            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO miembros (user_id, rol_id, ind_owner, ind_admin)
                                 VALUES (@user_id, @rol_id, @ind_owner, @ind_admin)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@user_id", member.Id);
                    cmd.Parameters.AddWithValue("@rol_id", member.RolUser.RolId);
                    cmd.Parameters.AddWithValue("@ind_owner", member.IndOwner);
                    cmd.Parameters.AddWithValue("@ind_admin", member.IndAdmin);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar Member: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Member? GetMemberById(string userId)
        {
            var user = userService.GetUserById(userId);
            if (user == null)
                return null;

            var conexion = ConexionDB.conectar();
            Member? member = null;

            try
            {
                string query = "SELECT * FROM miembros WHERE user_id = @user_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Rol? rol = rolService.GetRolById(Convert.ToInt32(reader["rol_id"].ToString()));
                            char indOwner = Convert.ToChar(reader["ind_owner"]);
                            char indAdmin = Convert.ToChar(reader["ind_admin"]);

                            member = new Member(
                                user.Id,
                                user.Pnombre,
                                user.Snombre,
                                user.Papellido,
                                user.Sapellido,
                                user.NdocIdent,
                                user.Tipo_docIdent,
                                user.Pais,
                                user.Ciudad,
                                user.Sexo,
                                user.Fnacimiento,
                                user.Ntelefono1,
                                user.Ntelefono2,
                                user.Direccion,
                                user.Login,
                                user.Pwd,
                                user.Email,
                                user.IndBloqueado,
                                user.IndActivo,
                                rol!,
                                indOwner,
                                indAdmin
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener Member: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return member;
        }

        public List<Member> GetAllMembers()
        {
            var lista = new List<Member>();
            var users = userService.GetAllUsers(); // Asume que hay un método para listar todos los User

            foreach (var user in users)
            {
                var member = GetMemberById(user.Id);
                if (member != null)
                    lista.Add(member);
            }

            return lista;
        }

        public bool UpdateMember(Member member)
        {
            bool userUpdated = userService.UpdateUser(member);
            bool memberUpdated = false;

            var conexion = ConexionDB.conectar();

            try
            {
                string query = @"UPDATE miembros 
                                 SET rol_id = @rol_id, ind_owner = @ind_owner, ind_admin = @ind_admin
                                 WHERE user_id = @user_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rol_id", member.RolUser.RolId);
                    cmd.Parameters.AddWithValue("@ind_owner", member.IndOwner);
                    cmd.Parameters.AddWithValue("@ind_admin", member.IndAdmin);
                    cmd.Parameters.AddWithValue("@user_id", member.Id);

                    memberUpdated = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar Member: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return userUpdated && memberUpdated;
        }

        public bool DeleteMember(string userId)
        {
            var conexion = ConexionDB.conectar();
            bool memberDeleted = false;

            try
            {
                string query = "DELETE FROM miembros WHERE user_id = @user_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    memberDeleted = cmd.ExecuteNonQuery() > 0;
                }

                if (memberDeleted)
                    return userService.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Member: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return false;
        }
    }
}
