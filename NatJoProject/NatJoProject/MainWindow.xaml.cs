using NatJoProject.Controllers;
using NatJoProject.Views;
using NatJoProject.ViewsPrueba;
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

                BacklogView backlogView = new BacklogView();
                backlogView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos.", "Error de login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}