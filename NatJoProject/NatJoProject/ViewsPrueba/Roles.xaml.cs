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
using NatJoProject.Controllers;
using NatJoProject.Models;

namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para Roles.xaml
    /// </summary>
    public partial class Roles : Window
    {
        RolController rolController = new RolController();
        public Roles()
        {
            InitializeComponent();
        }
        private void InsertRol_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("por favor, complete todos los campos.", "validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Rol rol = new Rol
            {
                RolId = txtId.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            try
            {
                rolController.InsertRol(rol);
                MessageBox.Show($"Sexo '{rol.RolId}' insertado con exito.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el sexo '{rol.RolId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

