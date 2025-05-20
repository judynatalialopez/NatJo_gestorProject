using NatJoProject.Models;
using NatJoProject.Controllers;
using SesionApp = NatJoProject.Session.Session;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NatJoProject.Pages
{
    /// <summary>
    /// Lógica de interacción para DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        private readonly ProjectController projectController = new ProjectController();

        public DashboardPage()
        {
            InitializeComponent();
            Loaded += DashboardPage_Loaded;
        }

        private async void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => CargarProyectosSeguro());
        }
        private void CargarProyectosSeguro()
        {
            try
            {
                var userId = SesionApp.UsuarioActual?.Id;
                if (string.IsNullOrEmpty(userId))
                {
                    Dispatcher.Invoke(() =>
                    {
                        txtSinProyectos.Text = "Error: usuario no autenticado.";
                        txtSinProyectos.Visibility = Visibility.Visible;
                    });
                    return;
                }

                var proyectos = projectController.MostrarProyectosPorUsuario(userId);

                if (proyectos == null || proyectos.Count == 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        wrapProyectos.Children.Clear();
                        txtSinProyectos.Visibility = Visibility.Visible;
                    });
                    return;
                }

                Dispatcher.Invoke(() =>
                {
                    wrapProyectos.Children.Clear();
                    txtSinProyectos.Visibility = Visibility.Collapsed;

                    foreach (var proyecto in proyectos)
                    {
                        var card = CrearCardProyecto(proyecto);
                        wrapProyectos.Children.Add(card);
                    }
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                    MessageBox.Show("Error al cargar el dashboard: " + ex.Message));
            }
        }


        private Border CrearCardProyecto(Project proyecto)
        {
            var border = new Border
            {
                Width = 280,
                Height = 160,
                Margin = new Thickness(10),
                CornerRadius = new CornerRadius(12),
                Background = Brushes.White,
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    ShadowDepth = 2,
                    Color = Colors.Gray,
                    Opacity = 0.4
                }
            };

            var stack = new StackPanel
            {
                Margin = new Thickness(15)
            };

            stack.Children.Add(new TextBlock
            {
                Text = proyecto.Nombre,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(33, 47, 61)),
                Margin = new Thickness(0, 0, 0, 6)
            });

            stack.Children.Add(new TextBlock
            {
                Text = proyecto.Descripcion,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.DimGray,
                FontSize = 13
            });

            stack.Children.Add(new TextBlock
            {
                Text = $"Inicio: {proyecto.Finicio:dd/MM/yyyy} - Fin: {proyecto.Fterminacion:dd/MM/yyyy}",
                Margin = new Thickness(0, 10, 0, 0),
                Foreground = Brushes.SteelBlue,
                FontSize = 12
            });

            border.Child = stack;
            return border;
        }
    }
}

