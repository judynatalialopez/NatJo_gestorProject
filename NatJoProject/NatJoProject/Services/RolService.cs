using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class RolService
    {
        public bool InsertRol(Rol rol)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "INSERT INTO roles (rol_id, descripcion) VALUES (@rol_id, @descripcion)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rol_id", rol.rolId);
                    cmd.Parameters.AddWithValue("@descripcion", rol.descripcion);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar Rol: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Rol? GetRolById(string rolId)
        {
            var conexion = ConexionDB.conectar();
            Rol? rol = null;

            try
            {
                string query = "SELECT * FROM roles WHERE rol_id = @rol_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rol_id", rolId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rol = new Rol
                            {
                                rolId = reader["rol_id"].ToString(),
                                descripcion = reader["descripcion"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener Rol: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return rol;
        }

        public List<Rol> GetAllRoles()
        {
            var roles = new List<Rol>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM roles";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rol rol = new Rol
                            {
                                rolId = reader["rol_id"].ToString(),
                                descripcion = reader["descripcion"].ToString()
                            };
                            roles.Add(rol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Roles: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return roles;
        }

        public bool UpdateRol(Rol rol)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "UPDATE roles SET descripcion = @descripcion WHERE rol_id = @rol_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@descripcion", rol.descripcion);
                    cmd.Parameters.AddWithValue("@rol_id", rol.rolId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar Rol: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteRol(string rolId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM roles WHERE rol_id = @rol_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rol_id", rolId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Rol: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
