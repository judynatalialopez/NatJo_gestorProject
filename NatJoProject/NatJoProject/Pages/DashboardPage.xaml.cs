using NatJoProject.Models;
using NatJoProject.Controllers;
using SesionApp = NatJoProject.Session.Session;
using NatJoProject.Views;
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
            try
            {
                // Evitar Task.Run para acceso a datos que ya es asíncrono o rápido
                await CargarProyectosSeguro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar el dashboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CargarProyectosSeguro()
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
                Height = 180, // un poco más alto para el botón
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
                Margin = new Thickness(0, 10, 0, 6),
                Foreground = Brushes.SteelBlue,
                FontSize = 12
            });

            var btnDetalles = new Button
            {
                Content = "Ver detalles",
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Margin = new Thickness(0, 10, 0, 0)
            };

            // Evento click para abrir nueva ventana pasando proyecto
            btnDetalles.Click += (s, e) => AbrirVentanaDetallesProyecto(proyecto.ProjId);

            stack.Children.Add(btnDetalles);

            border.Child = stack;
            return border;
        }

        private void AbrirVentanaDetallesProyecto(int projId)
        {
            MessageBox.Show($"Buscando proyecto con ID: {projId}");  // Para confirmar el ID

            Project proyectoCompleto = projectController.GetProjectById(projId);

            if (proyectoCompleto == null)
            {
                MessageBox.Show($"Proyecto con ID {projId} no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var asignarTarea = new AsignarTarea(proyectoCompleto);
            asignarTarea.Owner = Application.Current.MainWindow;
            asignarTarea.ShowDialog();
        }

    }
}

