using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NatJoProject.Database
{
    public class ConexionDB
    {
        private static readonly string connectionString = "server=localhost;Database=natjoproject;Uid=root;Pwd=;";

        public static MySqlConnection conectar()
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = new MySqlConnection(connectionString);
                conexion.Open();
                Console.WriteLine("Conexion establecida con exito");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR DDE CONEXION: " + ex.Message);
            }
            return conexion;
        }

        public static void desconectar(MySqlConnection conexion)
        {
            if (conexion != null)
            {
                try
                {
                    conexion.Close();
                    Console.WriteLine("La conexion se he cerrado");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: mal cierre de la conexion - " + ex.Message);
                }
            }
                
        }
    }
}
