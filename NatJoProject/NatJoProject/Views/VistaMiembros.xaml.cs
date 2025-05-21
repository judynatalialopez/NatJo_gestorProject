using NatJoProject.Controllers;
using NatJoProject.Models;
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
    /// Lógica de interacción para VistaMiembros.xaml
    /// </summary>
    public partial class VistaMiembros : Window
    {

        private TeamController teamController = new TeamController();

        private Project proyectoActual;
        Project project;

        public VistaMiembros(Project proyecto)
        {
            InitializeComponent();
            proyectoActual = proyecto;

            CargarMiembros();
        }

        private void CargarMiembros()
        {
            // Suponiendo que proyectoActual.Team.Miembros es una lista de objetos Miembro con propiedades Nombre, Apellido, Email, etc.
            // Si es null o vacío, muestra mensaje o lista vacía
            if (proyectoActual.Team?.Miembros != null && proyectoActual.Team.Miembros.Any())
            {
                dataGridMiembros.ItemsSource = proyectoActual.Team.Miembros;
            }
            else
            {
                MessageBox.Show("No hay miembros en este equipo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEliminarMiembro_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el miembro (user) de la fila donde se hizo click
            var button = sender as Button;
            if (button == null) return;

            var miembro = button.DataContext as Member; // Cambia 'Miembro' al tipo que usas en el DataGrid
            if (miembro == null) return;

            // Confirmar eliminación (opcional)
            var resultado = MessageBox.Show($"¿Eliminar a {miembro.Pnombre} del equipo?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado != MessageBoxResult.Yes) return;

            // Llamar a método para eliminar al miembro
            bool eliminado = EliminarMiembroDelEquipo(miembro.Id); // o el campo ID que tengas

            if (eliminado)
            {
                MessageBox.Show("Miembro eliminado del equipo.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Actualizar lista y refrescar DataGrid
                proyectoActual.Team.Miembros.Remove(miembro);
                dataGridMiembros.ItemsSource = null;
                dataGridMiembros.ItemsSource = proyectoActual.Team.Miembros;
            }
            else
            {
                MessageBox.Show("Error al eliminar el miembro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Método simulado para eliminar miembro, cambia según tu implementación real
        private bool EliminarMiembroDelEquipo(string userId)
        {
            try
            {
                // Aquí llamas tu servicio o controlador real
                teamController.DeleteUserFromTeam(proyectoActual.Team.TeamId, userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        private void BtnAgregarMiembro_Click(object sender, RoutedEventArgs e)
        {
            string email = txtCorreo.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Por favor ingresa un correo válido.");
                return;
            }

           teamController.AddUserToTeam(proyectoActual.Team.TeamId, email);

        }
    }
}
