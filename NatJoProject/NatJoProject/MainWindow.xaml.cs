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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnIrRegistrarPais(object sender, RoutedEventArgs e)
        {
            try
            {
                Roles roles = new Roles();
                roles.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ventana de registro: " + ex.Message);
            }
        }

        private void BtnRegistrarse(object sender, RoutedEventArgs e)
        {
            try
            {
                Registro registro = new Registro();
                registro.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ventana de registro: " + ex.Message);
            }
        }
    }
}