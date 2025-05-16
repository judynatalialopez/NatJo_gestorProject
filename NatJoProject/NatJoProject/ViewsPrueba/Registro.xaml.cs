using NatJoProject.Controllers;
using NatJoProject.Models;
using NatJoProject.Services;
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

namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {

        private SexoController sexoController = new SexoController();
        private CiudadController ciudadController = new CiudadController();
        private UserController userController = new UserController();

        public Registro()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sexos = sexoController.GetAllSexos();
            cbSexo.ItemsSource = sexos;
            cbSexo.DisplayMemberPath = "Descripcion"; // ya lo tienes para sexo
            cbSexo.SelectedValuePath = "SxId";

            var ciudades = ciudadController.GetAllCiudades();
            cbCiudad.ItemsSource = ciudades;
            cbCiudad.DisplayMemberPath = "Nombre";     // Aquí indicas qué propiedad mostrar
            cbCiudad.SelectedValuePath = "CityId";    // Aquí el valor que tomará al seleccionar

        }

        private void Registrarse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumentoID.Text) || string.IsNullOrWhiteSpace(txtTipoDoc.Text)
        || string.IsNullOrWhiteSpace(txtPrimerNombre.Text) || string.IsNullOrWhiteSpace(txtSegundoNombre.Text)
        || string.IsNullOrWhiteSpace(txtPrimerApellido.Text) || string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
        || string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbSexo.Text)
        || string.IsNullOrWhiteSpace(txtFechaNacimiento.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text)
        || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text)
        || string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out fechaNacimiento))
            {
                MessageBox.Show("Fecha de nacimiento inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener los objetos seleccionados de los combos
            Sexo sexoSeleccionado = (Sexo)cbSexo.SelectedItem;
            Ciudad ciudadSeleccionada = (Ciudad)cbCiudad.SelectedItem;

            // Crear usuario con valores correctos
            User user = new User(
            Id: txtDocumentoID.Text.Trim(),
            Pnombre: txtPrimerNombre.Text.Trim(),
            Snombre: string.IsNullOrWhiteSpace(txtSegundoNombre.Text) ? null : txtSegundoNombre.Text.Trim(),
            Papellido: txtPrimerApellido.Text.Trim(),
            Sapellido: string.IsNullOrWhiteSpace(txtSegundoApellido.Text) ? null : txtSegundoApellido.Text.Trim(),
            NdocIdent: txtDocumentoID.Text.Trim(),
            Tipo_docIdent: txtTipoDoc.Text.Trim(),
            Pais: ciudadSeleccionada.Pais,
            Ciudad: ciudadSeleccionada,
            Sexo: sexoSeleccionado,
            Fnacimiento: DateOnly.FromDateTime(fechaNacimiento),
            Ntelefono1: txtTelefono.Text.Trim(),
            Ntelefono2: txtTelefono.Text.Trim(),
            Direccion: txtDireccion.Text.Trim(),
            Login: txtEmail.Text.Trim(),
            Pwd: txtContraseña.Text.Trim(),
            Email: txtEmail.Text.Trim(),
            IndBloqueado: 'N',
            IndActivo: 'S'
        );

            try
            {
                userController.InsertUser(user);
                MessageBox.Show($"Sexo '{user.Id}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Sexo '{user.Id}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
