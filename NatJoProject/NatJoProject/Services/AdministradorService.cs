using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NatJoProject.Database;
using NatJoProject.Models;

namespace NatJoProject.Services
{
    public class AdministradorService
    {
        public bool InsertAdministrador(Administrador admin)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO administradores 
                                (email, pwd, p_nombre, s_nombre, p_apellido, s_apellido)
                                VALUES (@Email, @Pwd, @PNombre, @SNombre, @PApellido, @SApellido)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@Pwd", admin.Pwd);
                    cmd.Parameters.AddWithValue("@PNombre", admin.P_Nombre);
                    cmd.Parameters.AddWithValue("@SNombre", admin.S_Nombre);
                    cmd.Parameters.AddWithValue("@PApellido", admin.P_Apellido);
                    cmd.Parameters.AddWithValue("@SApellido", admin.S_Apellido);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar administrador: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public List<Administrador> GetAllAdministradores()
        {
            var lista = new List<Administrador>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM administradores";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Administrador admin = new Administrador(
                            Convert.ToInt32(reader["admin_id"]),
                            reader["email"].ToString(),
                            reader["pwd"].ToString(),
                            reader["p_nombre"].ToString(),
                            reader["s_nombre"].ToString(),
                            reader["p_apellido"].ToString(),
                            reader["s_apellido"].ToString()
                        );
                        lista.Add(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener administradores: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public Administrador? GetAdministradorById(int adminId)
        {
            Administrador? admin = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM administradores WHERE admin_id = @admin_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@admin_id", adminId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Administrador(
                                Convert.ToInt32(reader["admin_id"]),
                                reader["email"].ToString(),
                                reader["pwd"].ToString(),
                                reader["p_nombre"].ToString(),
                                reader["s_nombre"].ToString(),
                                reader["p_apellido"].ToString(),
                                reader["s_apellido"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar administrador: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return admin;
        }

        public bool UpdateAdministrador(Administrador admin)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE administradores 
                                 SET email = @Email, pwd = @Pwd, 
                                     p_nombre = @PNombre, s_nombre = @SNombre, 
                                     p_apellido = @PApellido, s_apellido = @SApellido
                                 WHERE admin_id = @AdminId";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@Pwd", admin.Pwd);
                    cmd.Parameters.AddWithValue("@PNombre", admin.P_Nombre);
                    cmd.Parameters.AddWithValue("@SNombre", admin.S_Nombre);
                    cmd.Parameters.AddWithValue("@PApellido", admin.P_Apellido);
                    cmd.Parameters.AddWithValue("@SApellido", admin.S_Apellido);
                    cmd.Parameters.AddWithValue("@AdminId", admin.AdminId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar administrador: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteAdministrador(int adminId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM administradores WHERE admin_id = @admin_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@admin_id", adminId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar administrador: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}

