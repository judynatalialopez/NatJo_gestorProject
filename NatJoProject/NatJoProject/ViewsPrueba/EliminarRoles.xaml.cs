
    using NatJoProject.Controllers;
    using NatJoProject.Models;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using MySql.Data.MySqlClient;
using NatJoProject.Database;

    namespace NatJoProject.ViewsPrueba
    {
        public partial class EliminarRoles : Window
        {
            private readonly RolController rolController = new RolController();

            public EliminarRoles()
            {
                InitializeComponent();
                CargarRoles();
            }

            private void CargarRoles()
            {
                cmbRoles.ItemsSource = null;
                List<Rol> roles = rolController.GetAllRoles();

                if (roles.Count == 0)
                {
                    txtMensaje.Text = "No hay roles disponibles para eliminar.";
                    cmbRoles.IsEnabled = false;
                    EliminarButton.IsEnabled = false;
                }
                else
                {
                    txtMensaje.Text = "";
                    cmbRoles.IsEnabled = true;
                    EliminarButton.IsEnabled = true;
                    cmbRoles.ItemsSource = roles;
                    cmbRoles.DisplayMemberPath = "Descripcion"; // Mostramos la descripción
                    cmbRoles.SelectedValuePath = "RolId";       // Por si necesitas el ID
                }
            }
        public bool DeleteRol(int rolId)
        {
            var conexion = ConexionDB.conectar();
            bool result = false;

            try
            {
                string query = "DELETE FROM roles WHERE rol_id = @rol_id";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rol_id", rolId);
                    result = cmd.ExecuteNonQuery() > 0; // Devuelve true si se eliminó algo
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Rol: " + ex.Message);
                throw; // Propaga el error para atraparlo en la UI
            }
            finally
            {
                ConexionDB.desconectar(conexion);
            }

            return result;
        }


        private void Cancelar_Click(object sender, RoutedEventArgs e)
            {
                Close();
            }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
    }

