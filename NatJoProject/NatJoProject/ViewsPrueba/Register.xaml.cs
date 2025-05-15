using NatJoProject.Controllers;
using NatJoProject.Models;
using System.Windows;

namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        private SexoController sexoController = new SexoController();

        public Register()
        {
            InitializeComponent();
        }

        private void InsertarSexo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Sexo sexo = new Sexo
            {
                SxId = txtId.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            try {
                sexoController.InsertSexo(sexo);
                MessageBox.Show($"Sexo '{sexo.SxId}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Sexo '{sexo.SxId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
