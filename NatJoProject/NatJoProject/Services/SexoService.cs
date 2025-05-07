using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class SexoService
    {
        public bool InsertSexo(Sexo sexo)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "INSERT INTO sexos (sx_id, descripcion) VALUES (@sx_id, @descripcion)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@sx_id", sexo.sxId);
                    cmd.Parameters.AddWithValue("@descripcion", sexo.descripcion);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar Sexo: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Sexo? GetSexoById(string sxId)
        {
            var conexion = ConexionDB.conectar();
            Sexo? sexo = null;

            try
            {
                string query = "SELECT * FROM sexos WHERE sx_id = @sx_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@sx_id", sxId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sexo = new Sexo
                            {
                                sxId = reader["sx_id"].ToString(),
                                descripcion = reader["descripcion"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener Sexo: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return sexo;
        }

        public List<Sexo> GetAllSexos()
        {
            var sexos = new List<Sexo>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM sexos";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sexo sexo = new Sexo
                            {
                                sxId = reader["sx_id"].ToString(),
                                descripcion = reader["descripcion"].ToString()
                            };
                            sexos.Add(sexo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Sexos: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return sexos;
        }

        public bool UpdateSexo(Sexo sexo)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "UPDATE sexos SET descripcion = @descripcion WHERE sx_id = @sx_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@descripcion", sexo.descripcion);
                    cmd.Parameters.AddWithValue("@sx_id", sexo.sxId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar Sexo: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteSexo(string sxId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM sexos WHERE sx_id = @sx_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@sx_id", sxId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Sexo: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
