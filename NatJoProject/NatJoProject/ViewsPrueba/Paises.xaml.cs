using NatJoProject.Controllers;
using NatJoProject.Models;
using System.Windows;


namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para Paises.xaml
    /// </summary>
    public partial class Paises : Window
    {
        private PaisController paisController = new PaisController();
        public Paises()
        {
            InitializeComponent();
        }
        private void InsertPais_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDominio.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Pais pais = new Pais
            {
                PaisId = txtId.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Dominio = txtDominio.Text.Trim()
            };

            try
            {
                paisController.InsertPais(pais);
                MessageBox.Show($"País '{pais.PaisId}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el país '{pais.PaisId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

}