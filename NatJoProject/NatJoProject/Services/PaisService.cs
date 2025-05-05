using System;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;


namespace NatJoProject.Services
{
    public class PaisService
    {
        public static Pais getpais(string paisId)
        {
            Pais pais = new Pais();

            string query = "SELECT * FROM pais WHERE paisid = @paisId";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("paisId", paisId);
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pais.paisId = reader.GetString("paisId");
                            pais.nombre = reader.GetString("nombre");
                            pais.dominio = reader.GetString("dominio");
                        }
                    }
                }
            }

            return pais;
        }

        public Boolean SetPais(Pais pais)
        {
            string queryInsert = "INSERT INTO pais (paisId, nombre, dominio) VALUES (@paisId, @nombre, @dominio)";
            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryInsert, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@paisId", pais.paisId);
                    command.Parameters.AddWithValue("@nombre", pais.nombre);
                    command.Parameters.AddWithValue("@dominio", pais.dominio);

                    mySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean DeletePais(string paisId)
        {
            String queryDelete = "DELETE FROM pais WHERE paisId = " + paisId;

            using (MySqlConnection MySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryDelete, MySqlConnection))
                {
                    MySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean UpdatePais(Pais pais)
        {
            string queryUpdate = "UPDATE pais SET  nombre = @nombre, dominio = @dominio WHERE paisId = @paisId";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryUpdate, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@nombre", pais.nombre);
                    command.Parameters.AddWithValue("@dominio", pais.dominio);
                    command.Parameters.AddWithValue("@paisId", pais.paisId);

                    mySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
