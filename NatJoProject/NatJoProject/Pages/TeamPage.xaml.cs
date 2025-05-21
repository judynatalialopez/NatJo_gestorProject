using NatJoProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SesionApp = NatJoProject.Session.Session;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NatJoProject.Models;
using NatJoProject.Views;

namespace NatJoProject.Pages
{
    /// <summary>
    /// Lógica de interacción para TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        private readonly ProjectController projectController = new ProjectController();

        public TeamPage()
        {
            InitializeComponent();
            Loaded += TeamPage_Loaded;
        }

        private async void TeamPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Evitar Task.Run para acceso a datos que ya es asíncrono o rápido
                await CargarTeams();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar los teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CargarTeams()
        {
            try
            {
                var userId = SesionApp.UsuarioActual?.Id;
                if (string.IsNullOrEmpty(userId))
                {
                    Dispatcher.Invoke(() =>
                    {
                        txtSinTeams.Text = "Error: usuario no autenticado.";
                        txtSinTeams.Visibility = Visibility.Visible;
                    });
                    return;
                }

                var proyectos = projectController.MostrarProyectosPorUsuario(userId);

                if (proyectos == null || proyectos.Count == 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        wrapTeams.Children.Clear();
                        txtSinTeams.Visibility = Visibility.Visible;
                    });
                    return;
                }

                Dispatcher.Invoke(() =>
                {
                    wrapTeams.Children.Clear();
                    txtSinTeams.Visibility = Visibility.Collapsed;

                    foreach (var proyecto in proyectos)
                    {
                        var card = CrearCardTeam(proyecto);
                        wrapTeams.Children.Add(card);
                    }
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                    MessageBox.Show("Error al cargar el dashboard: " + ex.Message));
            }
        }

        private Border CrearCardTeam(Project proyecto)
        {
            var border = new Border
            {
                Width = 280,
                Height = 220, // un poco más alto para los botones extras
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
                Text = proyecto.Team.Nombre,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(33, 47, 61)),
                Margin = new Thickness(0, 0, 0, 6)
            });

            stack.Children.Add(new TextBlock
            {
                Text = $"Proyecto asociado: {proyecto.Nombre}",
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.DimGray,
                FontSize = 13
            });

            int cantidadMiembros = proyecto.Team.Miembros?.Count ?? 0;
            stack.Children.Add(new TextBlock
            {
                Text = $"Miembros: {cantidadMiembros}",
                FontSize = 14,
                Foreground = Brushes.DimGray,
                Margin = new Thickness(0, 0, 0, 6)
            });

            // 
            var btnVer = new Button
            {
                Content = "Administrar team",
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Margin = new Thickness(0, 6, 0, 0)
            };
            btnVer.Click += (s, e) => IrVistaMiembros(proyecto.ProjId);
            stack.Children.Add(btnVer);

            border.Child = stack;
            return border;
        }

        // Métodos stubs para los botones nuevos
        private void IrVistaMiembros(int projId)
        {
            Project proyectoCompleto = projectController.GetProjectById(projId);

            if (proyectoCompleto == null)
            {
                MessageBox.Show($"Proyecto con ID {projId} no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var vistaMiembros  = new VistaMiembros(proyectoCompleto);
            vistaMiembros.Owner = Application.Current.MainWindow;
            vistaMiembros.ShowDialog();
        }
    }
}
