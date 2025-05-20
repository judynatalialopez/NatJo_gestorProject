using NatJoProject.Controllers;
using NatJoProject.Models;
using NatJoProject.ViewsPrueba;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NatJoProject.Pages
{
    public partial class RolPage : Page
    {
        private readonly RolController rolController = new RolController();

        public RolPage()
        {
            InitializeComponent();
            Loaded += RolPage_Loaded;
        }

        private async void RolPage_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarRoles();
        }

        private async Task CargarRoles()
        {
            try
            {
                List<Rol> roles = await Task.Run(() => rolController.GetAllRoles());
                dgRoles.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAgregarRol(object sender, RoutedEventArgs e)
        {
            Roles roles = new Roles();
            roles.Show();
        }

        private void btnEditarRol(object sender, RoutedEventArgs e)
        {
            
            Rol? rolSeleccionado = dgRoles.SelectedItem as Rol;

            if (rolSeleccionado != null)
            {
             
                var ventanaEditar = new EditarRoles(rolSeleccionado);
                bool? resultado = ventanaEditar.ShowDialog();

                
                if (resultado == true)
                {
                    _ = CargarRoles();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un rol para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminar(object sender, RoutedEventArgs e)
        {
            EliminarRoles eliminarRoles = new EliminarRoles();
            bool? resultado = eliminarRoles.ShowDialog();

            if (resultado == true)
            {
                _ = CargarRoles(); // Recargar la tabla si se eliminó un rol
            }
        }

    }
}
