using NatJoProject.Pages;
using NatJoProject.Models;
using NatJoProject.Controllers;
using SesionApp = NatJoProject.Session.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NatJoProject.Views
{
    /// <summary>
    /// Lógica de interacción para BacklogView.xaml
    /// </summary>
    public partial class BacklogView : Window
    {
        public BacklogView()
        {
            InitializeComponent();

            var usuario = Session.Session.UsuarioActual;
            if (usuario != null)
            {
                // Primer nombre y primer apellido
                string nombreUsuario = $"{usuario.Pnombre?.Split(' ')[0]} {usuario.Sapellido?.Split(' ')[0]}";
                UserDisplayName.Text = nombreUsuario;
            }

        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage());
        }

        private void Proyectos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProjectPage());
        }

        private void Teams_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeamPage());
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            var usuario = Session.Session.UsuarioActual;

            if (usuario != null)
            {
                string nombreCompleto = $"{usuario.Pnombre} {usuario.Snombre} {usuario.Sapellido} {usuario.Papellido}".Replace("  ", " ");
                UserNameText.Text = $"Nombre: {nombreCompleto}";
                UserDocText.Text = $"Documento: {usuario.Tipo_docIdent} {usuario.NdocIdent}";
                UserLocationText.Text = $"Ubicación: {usuario.Ciudad?.Nombre}, {usuario.Pais?.Nombre}";
                UserEmailText.Text = $"Email: {usuario.Email}";

                // Mostrar popup
                UserPopup.PlacementTarget = sender as Button;
                UserPopup.IsOpen = true;
            }
            else
            {
                MessageBox.Show("No hay ningún usuario en sesión.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "¿Estás seguro de que deseas cerrar sesión?",
                "Confirmar cierre de sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Cierra la ventana actual
            }
        }

        private void InsertarProyecto_Click(object sender, RoutedEventArgs e)
        {
            CrearProyecto crearProyecto = new CrearProyecto();
            crearProyecto.Owner = this;  // Aquí estableces la ventana padre
            crearProyecto.ShowDialog();
        }
    }
}