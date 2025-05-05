using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class UserService
    {
        public bool InsertUser(User user)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO users 
                                (id, pNombre, sNombre, pApellido, sApellido, ndocIdent, tipo_docIdent, 
                                 pais_id, ciudad_id, sexo_id, fNacimiento, nTelefono1, nTelefono2, 
                                 direccion, login, pwd, email, indBloqueado, indActivo) 
                                VALUES 
                                (@id, @pNombre, @sNombre, @pApellido, @sApellido, @ndocIdent, @tipo_docIdent,
                                 @pais_id, @ciudad_id, @sexo_id, @fNacimiento, @nTelefono1, @nTelefono2,
                                 @direccion, @login, @pwd, @email, @indBloqueado, @indActivo)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", user.id);
                    cmd.Parameters.AddWithValue("@pNombre", user.pNombre);
                    cmd.Parameters.AddWithValue("@sNombre", user.sNombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pApellido", user.pApellido);
                    cmd.Parameters.AddWithValue("@sApellido", user.sApellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ndocIdent", user.ndocIdent);
                    cmd.Parameters.AddWithValue("@tipo_docIdent", user.tipo_docIdent);
                    cmd.Parameters.AddWithValue("@pais_id", user.pais.paisId);
                    cmd.Parameters.AddWithValue("@ciudad_id", user.ciudad.cityId);
                    cmd.Parameters.AddWithValue("@sexo_id", user.sexo.sxId);
                    cmd.Parameters.AddWithValue("@fNacimiento", user.fNacimiento.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@nTelefono1", user.nTelefono1);
                    cmd.Parameters.AddWithValue("@nTelefono2", user.nTelefono2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", user.direccion);
                    cmd.Parameters.AddWithValue("@login", user.login);
                    cmd.Parameters.AddWithValue("@pwd", user.pwd);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@indBloqueado", user.indBloqueado);
                    cmd.Parameters.AddWithValue("@indActivo", user.indActivo);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar usuario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public List<User> GetAllUsers()
        {
            var usuarios = new List<User>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM users";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User(
                            reader["id"].ToString(),
                            reader["pNombre"].ToString(),
                            reader["sNombre"]?.ToString(),
                            reader["pApellido"].ToString(),
                            reader["ndocIdent"].ToString(),
                            reader["tipo_docIdent"].ToString(),
                            new Pais { paisId = reader["pais_id"].ToString() },
                            new Ciudad { cityId = reader["ciudad_id"].ToString() },
                            new Sexo { sxId = reader["sexo_id"].ToString() },
                            DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                            Convert.ToInt32(reader["nTelefono1"]),
                            reader["nTelefono2"] == DBNull.Value ? 0 : Convert.ToInt32(reader["nTelefono2"]),
                            reader["direccion"].ToString(),
                            reader["login"].ToString(),
                            reader["pwd"].ToString(),
                            reader["email"].ToString(),
                            Convert.ToChar(reader["indBloqueado"]),
                            Convert.ToChar(reader["indActivo"])
                        );

                        usuarios.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuarios: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return usuarios;
        }

        public User? GetUserById(string id)
        {
            var conexion = ConexionDB.conectar();
            User? user = null;

            try
            {
                string query = "SELECT * FROM users WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader["id"].ToString(),
                                reader["pNombre"].ToString(),
                                reader["sNombre"]?.ToString(),
                                reader["pApellido"].ToString(),
                                reader["ndocIdent"].ToString(),
                                reader["tipo_docIdent"].ToString(),
                                new Pais { paisId = reader["pais_id"].ToString() },
                                new Ciudad { cityId = reader["ciudad_id"].ToString() },
                                new Sexo { sxId = reader["sexo_id"].ToString() },
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                                Convert.ToInt32(reader["nTelefono1"]),
                                reader["nTelefono2"] == DBNull.Value ? 0 : Convert.ToInt32(reader["nTelefono2"]),
                                reader["direccion"].ToString(),
                                reader["login"].ToString(),
                                reader["pwd"].ToString(),
                                reader["email"].ToString(),
                                Convert.ToChar(reader["indBloqueado"]),
                                Convert.ToChar(reader["indActivo"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar usuario por ID: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return user;
        }

        public User? GetUserByLogin(string login)
        {
            var conexion = ConexionDB.conectar();
            User? user = null;

            try
            {
                string query = "SELECT * FROM users WHERE login = @login";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@login", login);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader["id"].ToString(),
                                reader["pNombre"].ToString(),
                                reader["sNombre"]?.ToString(),
                                reader["pApellido"].ToString(),
                                reader["ndocIdent"].ToString(),
                                reader["tipo_docIdent"].ToString(),
                                new Pais { paisId = reader["pais_id"].ToString() },
                                new Ciudad { cityId = reader["ciudad_id"].ToString() },
                                new Sexo { sxId = reader["sexo_id"].ToString() },
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                                Convert.ToInt32(reader["nTelefono1"]),
                                reader["nTelefono2"] == DBNull.Value ? 0 : Convert.ToInt32(reader["nTelefono2"]),
                                reader["direccion"].ToString(),
                                reader["login"].ToString(),
                                reader["pwd"].ToString(),
                                reader["email"].ToString(),
                                Convert.ToChar(reader["indBloqueado"]),
                                Convert.ToChar(reader["indActivo"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar usuario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return user;
        }

        public bool UpdateUser(User user)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE users SET
                                    pNombre = @pNombre,
                                    sNombre = @sNombre,
                                    pApellido = @pApellido,
                                    sApellido = @sApellido,
                                    ndocIdent = @ndocIdent,
                                    tipo_docIdent = @tipo_docIdent,
                                    pais_id = @pais_id,
                                    ciudad_id = @ciudad_id,
                                    sexo_id = @sexo_id,
                                    fNacimiento = @fNacimiento,
                                    nTelefono1 = @nTelefono1,
                                    nTelefono2 = @nTelefono2,
                                    direccion = @direccion,
                                    login = @login,
                                    pwd = @pwd,
                                    email = @email,
                                    indBloqueado = @indBloqueado,
                                    indActivo = @indActivo
                                 WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", user.id);
                    cmd.Parameters.AddWithValue("@pNombre", user.pNombre);
                    cmd.Parameters.AddWithValue("@sNombre", user.sNombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pApellido", user.pApellido);
                    cmd.Parameters.AddWithValue("@sApellido", user.sApellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ndocIdent", user.ndocIdent);
                    cmd.Parameters.AddWithValue("@tipo_docIdent", user.tipo_docIdent);
                    cmd.Parameters.AddWithValue("@pais_id", user.pais.paisId);
                    cmd.Parameters.AddWithValue("@ciudad_id", user.ciudad.cityId);
                    cmd.Parameters.AddWithValue("@sexo_id", user.sexo.sxId);
                    cmd.Parameters.AddWithValue("@fNacimiento", user.fNacimiento.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@nTelefono1", user.nTelefono1);
                    cmd.Parameters.AddWithValue("@nTelefono2", user.nTelefono2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", user.direccion);
                    cmd.Parameters.AddWithValue("@login", user.login);
                    cmd.Parameters.AddWithValue("@pwd", user.pwd);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@indBloqueado", user.indBloqueado);
                    cmd.Parameters.AddWithValue("@indActivo", user.indActivo);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteUser(string id)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM users WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
