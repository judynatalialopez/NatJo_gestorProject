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
    public class CommentService
    {
        private readonly MemberService memberService = new MemberService();

        public bool InsertComment(Comment comment)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO comentarios (texto, autor_id, fecha_comentario) 
                                 VALUES (@texto, @autor_id, @fecha)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@texto", comment.texto);
                    cmd.Parameters.AddWithValue("@autor_id", comment.autor.id);
                    cmd.Parameters.AddWithValue("@fecha", comment.fcomentario);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar comentario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public Comment? GetCommentById(int commentId)
        {
            Comment? comment = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM comentarios WHERE comment_id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", commentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var autorId = reader["autor_id"].ToString();
                            var autor = memberService.GetMemberById(autorId!);

                            if (autor != null)
                            {
                                comment = new Comment(
                                    Convert.ToInt32(reader["comment_id"]),
                                    reader["texto"].ToString(),
                                    autor,
                                    Convert.ToDateTime(reader["fecha_comentario"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return comment;
        }

        public List<Comment> GetAllComments()
        {
            var lista = new List<Comment>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM comentarios";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var autorId = reader["autor_id"].ToString();
                        var autor = memberService.GetMemberById(autorId!);

                        if (autor != null)
                        {
                            lista.Add(new Comment(
                                Convert.ToInt32(reader["comment_id"]),
                                reader["texto"].ToString(),
                                autor,
                                Convert.ToDateTime(reader["fecha_comentario"])
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar comentarios: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public bool UpdateComment(Comment comment)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE comentarios 
                                 SET texto = @texto, autor_id = @autor_id, fecha_comentario = @fecha 
                                 WHERE comment_id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@texto", comment.texto);
                    cmd.Parameters.AddWithValue("@autor_id", comment.autor.id);
                    cmd.Parameters.AddWithValue("@fecha", comment.fcomentario);
                    cmd.Parameters.AddWithValue("@id", comment.commId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar comentario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public bool DeleteComment(int commentId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM comentarios WHERE comment_id = @id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", commentId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar comentario: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
