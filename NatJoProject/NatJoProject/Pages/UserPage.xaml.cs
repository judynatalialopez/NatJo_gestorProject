using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NatJoProject.Controllers;
using NatJoProject.Models;
using NatJoProject.ViewsPrueba;

namespace NatJoProject.Pages
{
    public partial class UserPage : Page
    {
        private readonly UserController userController = new UserController();

        public UserPage()
        {
            InitializeComponent();
            Loaded += UserPage_Loaded;
        }

        private async void UserPage_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarUsers();
        }

        private async Task CargarUsers()
        {
            try
            {
                List<User> usuarios = await Task.Run(() => userController.GetAllUsers());
                dgUsers.ItemsSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminarUsuario(object sender, RoutedEventArgs e)
        {
            EliminarUsers eliminarUsers = new EliminarUsers();
            eliminarUsers.Show();
        }
    }
}
