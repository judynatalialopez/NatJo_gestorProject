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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using NatJoProject.Pages;
using NatJoProject.ViewsPrueba;

namespace NatJoProject.Views
{
    /// <summary>
    /// Lógica de interacción para Administradores.xaml
    /// </summary>
    public partial class Administradores : Window
    {
        public Administradores()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RolPage());
        }

        private void Button_Paises(object sender, RoutedEventArgs e)
        {
            Paises paises = new Paises();
            paises.Show();
            this.Close();
        }

        private void Button_Estados_Treas(object sender, RoutedEventArgs e)
        {
            Estados_Task estados_Task = new Estados_Task();
            estados_Task.Show();
            this.Close();
        }

        private void Button_Ciudades(object sender, RoutedEventArgs e)
        {
            Ciudades ciudades = new Ciudades();
            ciudades.Show();
            this.Close();
        }

        private void Button_Sexos(object sender, RoutedEventArgs e)
        {
            Sexos sexos = new Sexos();
            sexos.Show();
            this.Close();
        }

        private void Button_Users(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserPage());
        }
    }
}
