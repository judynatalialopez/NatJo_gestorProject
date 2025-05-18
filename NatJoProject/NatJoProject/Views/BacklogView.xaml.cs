using NatJoProject.Pages;
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

namespace NatJoProject.Views
{
    /// <summary>
    /// Lógica de interacción para BacklogView.xaml
    /// </summary>
    public partial class BacklogView : Window
    {
        public BacklogView()
        {
            InitializeComponent();
            MessageBox.Show(MainFrame != null ? "MainFrame OK" : "MainFrame ES NULL");
        }


        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new DashboardPage());
        }

        private void Proyectos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProjectPage());
        }

        private void Teams_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeamPage());
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            var menu = new ContextMenu();

            var viewProfile = new MenuItem { Header = "Ver perfil" };
            viewProfile.Click += (s, args) => MessageBox.Show("Abriendo perfil...");

            var logout = new MenuItem { Header = "Cerrar sesión" };
            logout.Click += (s, args) => MessageBox.Show("Cerrando sesión...");

            menu.Items.Add(viewProfile);
            menu.Items.Add(logout);

            // Mostrar el menú en la posición del botón
            menu.PlacementTarget = sender as Button;
            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            menu.IsOpen = true;
        }

    }
}
