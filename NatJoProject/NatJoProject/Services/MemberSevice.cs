using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

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
                    cmd.Parameters.AddWithValue("@user_id", member.id);
                    cmd.Parameters.AddWithValue("@rol_id", member.rolUser.rolId);
                    cmd.Parameters.AddWithValue("@ind_owner", member.indOwner);
                    cmd.Parameters.AddWithValue("@ind_admin", member.indAdmin);

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
                            Rol? rol = rolService.GetRolById(reader["rol_id"].ToString());
                            char indOwner = Convert.ToChar(reader["ind_owner"]);
                            char indAdmin = Convert.ToChar(reader["ind_admin"]);

                            member = new Member(
                                user.id,
                                user.pNombre,
                                user.sNombre,
                                user.pApellido,
                                user.sApellido,
                                user.ndocIdent,
                                user.tipo_docIdent,
                                user.pais,
                                user.ciudad,
                                user.sexo,
                                user.fNacimiento,
                                user.nTelefono1,
                                user.nTelefono2,
                                user.direccion,
                                user.login,
                                user.pwd,
                                user.email,
                                user.indBloqueado,
                                user.indActivo,
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
                var member = GetMemberById(user.id);
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
                    cmd.Parameters.AddWithValue("@rol_id", member.rolUser.rolId);
                    cmd.Parameters.AddWithValue("@ind_owner", member.indOwner);
                    cmd.Parameters.AddWithValue("@ind_admin", member.indAdmin);
                    cmd.Parameters.AddWithValue("@user_id", member.id);

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
