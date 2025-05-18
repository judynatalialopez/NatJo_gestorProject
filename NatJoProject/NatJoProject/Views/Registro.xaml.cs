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

            // Validar número de documento (exactamente 10 caracteres, solo dígitos)
            string documento = txtDocumentoID.Text.Trim();
            if (documento.Length != 10 || !documento.All(char.IsDigit))
            {
                MessageBox.Show("El número de documento debe tener exactamente 10 dígitos numéricos.", "Validación Documento", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que nombres y apellidos solo contengan letras (sin números ni espacios)
            bool ValidarSoloLetras(string texto) =>
                !string.IsNullOrEmpty(texto) && texto.All(c => char.IsLetter(c));

            if (!ValidarSoloLetras(txtPrimerNombre.Text.Trim()))
            {
                MessageBox.Show("El primer nombre solo debe contener letras, sin números ni espacios.", "Validación Nombre", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtSegundoNombre.Text) && !ValidarSoloLetras(txtSegundoNombre.Text.Trim()))
            {
                MessageBox.Show("El segundo nombre solo debe contener letras, sin números ni espacios.", "Validación Nombre", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ValidarSoloLetras(txtPrimerApellido.Text.Trim()))
            {
                MessageBox.Show("El primer apellido solo debe contener letras, sin números ni espacios.", "Validación Apellido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtSegundoApellido.Text) && !ValidarSoloLetras(txtSegundoApellido.Text.Trim()))
            {
                MessageBox.Show("El segundo apellido solo debe contener letras, sin números ni espacios.", "Validación Apellido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out fechaNacimiento))
            {
                MessageBox.Show("Fecha de nacimiento inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar número de teléfono (exactamente 10 dígitos)
            string telefono = txtTelefono.Text.Trim();
            if (telefono.Length != 10 || !telefono.All(char.IsDigit))
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 10 dígitos numéricos.", "Validación Teléfono", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Antes de crear el usuario, validar si el email ya existe:
            if (userController.VerifyDocId(txtDocumentoID.Text.Trim()))
            {
                MessageBox.Show("El documento ingresado ya está registrado. Por favor, use otro documento.", "Validación Documento", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Antes de crear el usuario, validar si el email ya existe:
            if (userController.VerifyEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("El email ingresado ya está registrado. Por favor, use otro email.", "Validación Email", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;  
            }

            if (string.IsNullOrWhiteSpace(txtDocumentoID.Text) || string.IsNullOrWhiteSpace(txtTipoDoc.Text)
                || string.IsNullOrWhiteSpace(txtPrimerNombre.Text) || string.IsNullOrWhiteSpace(txtSegundoNombre.Text)
                || string.IsNullOrWhiteSpace(txtPrimerApellido.Text) || string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                || string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbSexo.Text)
                || string.IsNullOrWhiteSpace(txtFechaNacimiento.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text)
                || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContraseña.Password)
                || string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener los objetos seleccionados de los combos
            Sexo sexoSeleccionado = (Sexo)cbSexo.SelectedItem;
            Ciudad ciudadSeleccionada = (Ciudad)cbCiudad.SelectedItem;

            // Crear usuario con valores correctos
            User user = new User(
                Id: documento,
                Pnombre: txtPrimerNombre.Text.Trim(),
                Snombre: string.IsNullOrWhiteSpace(txtSegundoNombre.Text) ? null : txtSegundoNombre.Text.Trim(),
                Papellido: txtPrimerApellido.Text.Trim(),
                Sapellido: string.IsNullOrWhiteSpace(txtSegundoApellido.Text) ? null : txtSegundoApellido.Text.Trim(),
                NdocIdent: documento,
                Tipo_docIdent: txtTipoDoc.Text.Trim(),
                Pais: ciudadSeleccionada.Pais,
                Ciudad: ciudadSeleccionada,
                Sexo: sexoSeleccionado,
                Fnacimiento: DateOnly.FromDateTime(fechaNacimiento),
                Ntelefono1: telefono,
                Ntelefono2: telefono,
                Direccion: txtDireccion.Text.Trim(),
                Login: txtEmail.Text.Trim(),
                Pwd: txtContraseña.Password.Trim(),
                Email: txtEmail.Text.Trim(),
                IndBloqueado: 'N',
                IndActivo: 'S'
            );

            try
            {
                userController.InsertUser(user);
                MessageBox.Show($"Usuario '{user.Id}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Usuario '{user.Id}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
