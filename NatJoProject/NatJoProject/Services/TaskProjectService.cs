using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NatJoProject.Models;
using NatJoProject.Database;

namespace NatJoProject.Services
{
    public class TaskProjectService
    {
        private readonly MemberService memberService = new MemberService();
        private readonly TaskEstadoService estadoService = new TaskEstadoService();
        private readonly CommentService commentService = new CommentService();

        public bool InsertTaskProject(TaskProject task)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"INSERT INTO tasks (task_id, titulo, descripcion, estado_id, f_entrega)
                                 VALUES (@task_id, @titulo, @descripcion, @estado_id, @f_entrega)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@task_id", task.TaskId);
                    cmd.Parameters.AddWithValue("@titulo", task.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", task.Descripcion);
                    cmd.Parameters.AddWithValue("@estado_id", task.estado.EstId);
                    cmd.Parameters.AddWithValue("@f_entrega", task.Fentrerga);

                    result = cmd.ExecuteNonQuery() > 0;
                }

                foreach (var miembro in task.Responsable)
                {
                    string relQuery = "INSERT INTO responsables_tarea (task_id, user_id) VALUES (@task_id, @user_id)";
                    using (var relCmd = new MySqlCommand(relQuery, conexion))
                    {
                        relCmd.Parameters.AddWithValue("@task_id", task.TaskId);
                        relCmd.Parameters.AddWithValue("@user_id", miembro.id);
                        relCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar tarea: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public TaskProject? GetTaskProjectById(string taskId)
        {
            TaskProject? task = null;
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM tasks WHERE task_id = @task_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@task_id", taskId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var estado = estadoService.GetEstadoById(reader["estado_id"].ToString());
                            var fechaEntrega = Convert.ToDateTime(reader["f_entrega"]);

                            task = new TaskProject(
                                reader["task_id"].ToString(),
                                reader["titulo"].ToString(),
                                reader["descripcion"].ToString(),
                                new List<Member>(),
                                estado!,
                                fechaEntrega,
                                new List<Comment>()
                            );
                        }
                    }
                }

                if (task != null)
                {
                    task.responsable = GetResponsables(task.taskId);

                    if (int.TryParse(task.taskId, out int taskIdAsInt))
                    {
                        task.comentarios = memberService.GetCommentById(taskIdAsInt);//REVISAR CONVERSIÓN STRING A INT
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] taskId '{task.taskId}' no es un número válido.");
                        task.comentarios = new List<Comment>();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tarea: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return task;
        }

        private List<Member> GetResponsables(string taskId)
        {
            var lista = new List<Member>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT user_id FROM responsables_tarea WHERE task_id = @task_id";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@task_id", taskId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var miembro = memberService.GetMemberById(reader["user_id"].ToString());
                            if (miembro != null)
                                lista.Add(miembro);
                        }
                    }
                }
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public List<TaskProject> GetAllTaskProjects()
        {
            var lista = new List<TaskProject>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT task_id FROM tasks";

                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = GetTaskProjectById(reader["task_id"].ToString());
                        if (task != null)
                            lista.Add(task);
                    }
                }
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return lista;
        }

        public bool UpdateTaskProject(TaskProject task)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = @"UPDATE tasks 
                                 SET titulo = @titulo, descripcion = @descripcion, estado_id = @estado_id, f_entrega = @f_entrega 
                                 WHERE task_id = @task_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@titulo", task.titulo);
                    cmd.Parameters.AddWithValue("@descripcion", task.descripcion);
                    cmd.Parameters.AddWithValue("@estado_id", task.estado.estId);
                    cmd.Parameters.AddWithValue("@f_entrega", task.fEntrerga);
                    cmd.Parameters.AddWithValue("@task_id", task.taskId);

                    result = cmd.ExecuteNonQuery() > 0;
                }

                new MySqlCommand($"DELETE FROM responsables_tarea WHERE task_id = '{task.taskId}'", conexion).ExecuteNonQuery();

                foreach (var miembro in task.responsable)
                {
                    string relQuery = "INSERT INTO responsables_tarea (task_id, user_id) VALUES (@task_id, @user_id)";
                    using (var relCmd = new MySqlCommand(relQuery, conexion))
                    {
                        relCmd.Parameters.AddWithValue("@task_id", task.taskId);
                        relCmd.Parameters.AddWithValue("@user_id", miembro.id);
                        relCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar tarea: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }

        public List<TaskProject> GetTasksByProjectId(string projId)
        {
            var tasks = new List<TaskProject>();
            var conexion = ConexionDB.conectar();

            try
            {
                string query = "SELECT * FROM TaskProject WHERE proj_id = @proj_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@proj_id", projId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var taskId = reader["task_id"].ToString();
                            var memberId = reader["member_id"].ToString();
                            var estadoId = reader["estado_id"].ToString();

                            var member = memberService.GetMemberById(memberId);
                            var estado = taskestadoService.GetTaskEstadoById(estadoId);
                            var comments = commentService.GetCommentsByTaskId(taskId);

                            var task = new TaskProject(
                                taskId,
                                reader["titulo"].ToString(),
                                reader["descripcion"].ToString(),
                                reader["proj_id"].ToString(),
                                member,
                                estado,
                                comments,
                                Convert.ToDateTime(reader["f_inicio"]),
                                Convert.ToDateTime(reader["f_entrega"])
                            );

                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tareas del proyecto: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return tasks;
        }


        public bool DeleteTaskProject(string taskId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                using (var cmd1 = new MySqlCommand("DELETE FROM responsables_tarea WHERE task_id = @task_id", conexion))
                {
                    cmd1.Parameters.AddWithValue("@task_id", taskId);
                    cmd1.ExecuteNonQuery();
                }

                using (var cmd2 = new MySqlCommand("DELETE FROM comentarios WHERE task_id = @task_id", conexion))
                {
                    cmd2.Parameters.AddWithValue("@task_id", taskId);
                    cmd2.ExecuteNonQuery();
                }

                using (var cmd = new MySqlCommand("DELETE FROM tasks WHERE task_id = @task_id", conexion))
                {
                    cmd.Parameters.AddWithValue("@task_id", taskId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar tarea: " + ex.Message);
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }
    }
}
