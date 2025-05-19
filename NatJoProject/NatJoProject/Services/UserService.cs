using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;
using System.Windows;

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
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@pNombre", user.Pnombre);
                    cmd.Parameters.AddWithValue("@sNombre", user.Snombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pApellido", user.Papellido);
                    cmd.Parameters.AddWithValue("@sApellido", user.Sapellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ndocIdent", user.NdocIdent);
                    cmd.Parameters.AddWithValue("@tipo_docIdent", user.Tipo_docIdent);
                    cmd.Parameters.AddWithValue("@pais_id", user.Pais.PaisId);
                    cmd.Parameters.AddWithValue("@ciudad_id", user.Ciudad.CityId);
                    cmd.Parameters.AddWithValue("@sexo_id", user.Sexo.SxId);
                    cmd.Parameters.AddWithValue("@fNacimiento", user.Fnacimiento.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@nTelefono1", user.Ntelefono1);
                    cmd.Parameters.AddWithValue("@nTelefono2", user.Ntelefono2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", user.Direccion);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@pwd", user.Pwd);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@indBloqueado", user.IndBloqueado);
                    cmd.Parameters.AddWithValue("@indActivo", user.IndActivo);

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
                            reader["sApellido"].ToString(),
                            reader["ndocIdent"].ToString(),
                            reader["tipo_docIdent"].ToString(),
                            new Pais { PaisId = Convert.ToInt32(reader["pais_id"].ToString()) },
                            new Ciudad { CityId = Convert.ToInt32(reader["ciudad_id"].ToString()) },
                            new Sexo { SxId = Convert.ToInt32(reader["sexo_id"].ToString()) },
                            DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                            reader["nTelefono1"].ToString(),
                            reader["nTelefono2"] == DBNull.Value ? "" : reader["nTelefono2"].ToString(),
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
                                reader["sApellido"].ToString(),
                                reader["ndocIdent"].ToString(),
                                reader["tipo_docIdent"].ToString(),
                                new Pais { PaisId = Convert.ToInt32(reader["pais_id"].ToString()) },
                                new Ciudad { CityId = Convert.ToInt32(reader["ciudad_id"].ToString()) },
                                new Sexo { SxId = Convert.ToInt32(reader["sexo_id"].ToString()) },
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                                reader["nTelefono1"].ToString(),
                                reader["nTelefono2"] == DBNull.Value ? "" : reader["nTelefono2"].ToString(),
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

        public bool UserLogin(string email, string pwd)
        {
            var conexion = ConexionDB.conectar();
            bool loginSuccess = false;

            try
            {
                string query = @"SELECT 1 FROM users WHERE email = @email AND pwd = @pwd LIMIT 1";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        loginSuccess = reader.Read(); // Si devuelve al menos una fila, el login es correcto
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar login: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return loginSuccess;
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
                                reader["sApellido"].ToString(),
                                reader["pApellido"].ToString(),
                                reader["ndocIdent"].ToString(),
                                reader["tipo_docIdent"].ToString(),
                                new Pais { PaisId = Convert.ToInt32(reader["pais_id"].ToString()) },
                                new Ciudad { CityId = Convert.ToInt32(reader["ciudad_id"].ToString()) },
                                new Sexo { SxId = Convert.ToInt32(reader["sexo_id"].ToString()) },
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fNacimiento"])),
                                reader["nTelefono1"].ToString(),
                                reader["nTelefono2"] == DBNull.Value ? "" : reader["nTelefono2"].ToString(),
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

        public bool VerifyEmail(string email)
        {
            var conexion = ConexionDB.conectar();
            bool existe = false;

            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE email = @Email";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar email: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return existe;
        }

        public bool VerifyDocId(string docId)
        {
            var conexion = ConexionDB.conectar();
            bool existe = false;

            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE ndocIdent = @ndocIdent";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ndocIdent", docId);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar ndocIdent: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return existe;
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
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@pNombre", user.Pnombre);
                    cmd.Parameters.AddWithValue("@sNombre", user.Snombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pApellido", user.Papellido);
                    cmd.Parameters.AddWithValue("@sApellido", user.Sapellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ndocIdent", user.NdocIdent);
                    cmd.Parameters.AddWithValue("@tipo_docIdent", user.Tipo_docIdent);
                    cmd.Parameters.AddWithValue("@pais_id", user.Pais.PaisId);
                    cmd.Parameters.AddWithValue("@ciudad_id", user.Ciudad.CityId);
                    cmd.Parameters.AddWithValue("@sexo_id", user.Sexo.SxId);
                    cmd.Parameters.AddWithValue("@fNacimiento", user.Fnacimiento.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@nTelefono1", user.Ntelefono1);
                    cmd.Parameters.AddWithValue("@nTelefono2", user.Ntelefono2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", user.Direccion);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@pwd", user.Pwd);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@indBloqueado", user.IndBloqueado);
                    cmd.Parameters.AddWithValue("@indActivo", user.IndActivo);

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
