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
    /// Lógica de interacción para Ciudades.xaml
    /// </summary>
    public partial class Ciudades : Window
    {
        private PaisController paisController = new PaisController();
        private CiudadController ciudadController = new CiudadController();
        public Ciudades()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var paises = paisController.GetAllPaises();
            cbPais.ItemsSource = paises;
            cbPais.DisplayMemberPath = "Nombre";
            cbPais.SelectedValuePath = "PaisId";
        }

        private void RegistrarCiudad_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)
            || string.IsNullOrWhiteSpace(txtCodPostal.Text)
            || string.IsNullOrWhiteSpace(cbPais.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Pais paisSeleccionado = (Pais)cbPais.SelectedItem;

            Ciudad ciudad = new Ciudad(
                    Nombre: txtNombre.Text.Trim(),
                    CodPostal: txtCodPostal.Text.Trim(),
                    Pais: paisSeleccionado
            );
            try
            {
                ciudadController.InsertCiudad(ciudad);
                MessageBox.Show($"Sexo '{ciudad.CityId}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Sexo '{ciudad.CityId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }
}
