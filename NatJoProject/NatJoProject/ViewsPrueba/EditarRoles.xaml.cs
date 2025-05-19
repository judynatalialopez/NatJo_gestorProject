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
    public partial class EditarRoles : Window
    {
        private Rol rolActual;
        private RolController rolController = new RolController();

        public EditarRoles(Rol rol)
        {
            InitializeComponent();
            rolActual = rol;

            txtIdRol.Text = rol.RolId.ToString();
            txtDescripcion.Text = rol.Descripcion;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            rolActual.Descripcion = txtDescripcion.Text.Trim();

            // Llama al controller para actualizar
            rolController.UpdateRol(rolActual);

            MessageBox.Show("Rol actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

