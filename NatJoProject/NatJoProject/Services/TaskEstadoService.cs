using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class TaskEstadoService
    {
        public bool InsertEstado(TaskEstado estado)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "INSERT INTO estados_task (estado_id, descripcion) VALUES (@id, @desc)";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", estado.EstId);
                    cmd.Parameters.AddWithValue("@desc", estado.Descripcion);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar estado: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public TaskEstado? GetEstadoById(string id)
        {
            TaskEstado? estado = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM estados_task WHERE estado_id = @id";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estado = new TaskEstado(
                                reader["estado_id"].ToString(),
                                reader["descripcion"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener estado: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return estado;
        }

        public List<TaskEstado> GetAllEstados()
        {
            var lista = new List<TaskEstado>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM estados_task";
                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new TaskEstado(
                            reader["estado_id"].ToString(),
                            reader["descripcion"].ToString()
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar estados: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public bool UpdateEstado(TaskEstado estado)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "UPDATE estados_task SET descripcion = @desc WHERE estado_id = @id";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desc", estado.Descripcion);
                    cmd.Parameters.AddWithValue("@id", estado.EstId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar estado: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteEstado(string id)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM estados_task WHERE estado_id = @id";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar estado: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
