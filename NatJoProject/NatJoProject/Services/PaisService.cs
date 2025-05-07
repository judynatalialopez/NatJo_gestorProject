using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class PaisService
    {
        public bool InsertPais(Pais pais)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO paises (pais_id, nombre, dominio)
                                 VALUES (@pais_id, @nombre, @dominio)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@pais_id", pais.PaisId);
                    cmd.Parameters.AddWithValue("@nombre", pais.Nombre);
                    cmd.Parameters.AddWithValue("@dominio", pais.Dominio);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar país: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public List<Pais> GetAllPaises()
        {
            var lista = new List<Pais>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM paises";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pais pais = new Pais(
                            reader["pais_id"].ToString(),
                            reader["nombre"].ToString(),
                            reader["dominio"].ToString()
                        );
                        lista.Add(pais);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener países: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public Pais? GetPaisById(string paisId)
        {
            Pais? pais = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM paises WHERE pais_id = @pais_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@pais_id", paisId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pais = new Pais(
                                reader["pais_id"].ToString(),
                                reader["nombre"].ToString(),
                                reader["dominio"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar país: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return pais;
        }

        public bool UpdatePais(Pais pais)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE paises 
                                 SET nombre = @nombre, dominio = @dominio 
                                 WHERE pais_id = @pais_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", pais.Nombre);
                    cmd.Parameters.AddWithValue("@dominio", pais.Dominio);
                    cmd.Parameters.AddWithValue("@pais_id", pais.PaisId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar país: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeletePais(string paisId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM paises WHERE pais_id = @pais_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@pais_id", paisId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar país: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
