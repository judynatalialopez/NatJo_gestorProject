using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class CiudadService
    {
        private readonly PaisService paisService = new PaisService();

        public bool InsertCiudad(Ciudad ciudad)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO ciudades (city_id, nombre, cod_postal, pais_id)
                                 VALUES (@city_id, @nombre, @cod_postal, @pais_id)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@city_id", ciudad.CityId);
                    cmd.Parameters.AddWithValue("@nombre", ciudad.Nombre);
                    cmd.Parameters.AddWithValue("@cod_postal", ciudad.CodPostal);
                    cmd.Parameters.AddWithValue("@pais_id", ciudad.Pais.PaisId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar ciudad: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public List<Ciudad> GetAllCiudades()
        {
            var lista = new List<Ciudad>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM ciudades";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string paisId = reader["pais_id"].ToString();
                        Pais? pais = paisService.GetPaisById(paisId);

                        Ciudad ciudad = new Ciudad(
                            reader["city_id"].ToString(),
                            reader["nombre"].ToString(),
                            reader["cod_postal"].ToString(),
                            pais!
                        );

                        lista.Add(ciudad);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener ciudades: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public Ciudad? GetCiudadById(string cityId)
        {
            Ciudad? ciudad = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM ciudades WHERE city_id = @city_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@city_id", cityId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string paisId = reader["pais_id"].ToString();
                            Pais? pais = paisService.GetPaisById(paisId);

                            ciudad = new Ciudad(
                                reader["city_id"].ToString(),
                                reader["nombre"].ToString(),
                                reader["cod_postal"].ToString(),
                                pais!
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar ciudad: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return ciudad;
        }

        public bool UpdateCiudad(Ciudad ciudad)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE ciudades 
                                 SET nombre = @nombre, cod_postal = @cod_postal, pais_id = @pais_id 
                                 WHERE city_id = @city_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", ciudad.Nombre);
                    cmd.Parameters.AddWithValue("@cod_postal", ciudad.CodPostal);
                    cmd.Parameters.AddWithValue("@pais_id", ciudad.Pais.PaisId);
                    cmd.Parameters.AddWithValue("@city_id", ciudad.CityId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar ciudad: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteCiudad(string cityId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM ciudades WHERE city_id = @city_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@city_id", cityId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar ciudad: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
