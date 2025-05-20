using NatJoProject.Controllers;
using NatJoProject.Models;
using NatJoProject.Views;
using SesionApp = NatJoProject.Session.Session;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NatJoProject.ViewsPrueba;

namespace NatJoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private UserController userController = new UserController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistro(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Close();
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string pwd = txtPwd.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Campos requeridos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool loginExitoso = userController.LoginUser(email, pwd); // Este método debería devolver bool

            if (loginExitoso)
            {
                MessageBox.Show("¡Login exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);

                //Obtener usuario por el login
                User? usuario = userController.GetUserByLogin(email);
                SesionApp.UsuarioActual = usuario;

                
                BacklogView backlogView = new BacklogView();
                backlogView.Show();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos.", "Error de login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdmin(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string pwd = txtPwd.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Campos requeridos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool loginExitoso = userController.LoginUserByAdmin(email, pwd); // Este método debería devolver bool

            if (loginExitoso)
            {
                MessageBox.Show("¡Login exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);

                //Obtener usuario por el login
                User? usuario = userController.GetUserByLogin(email);
                SesionApp.UsuarioActual = usuario;

                Administradores administradores = new Administradores();
                administradores.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos.", "Error de login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrueba_CLick(object sender, RoutedEventArgs e)
        {
            ProyetosPrueba proyetosPrueba = new ProyetosPrueba();
            proyetosPrueba.Show();
            this.Close();
        }
    }
}