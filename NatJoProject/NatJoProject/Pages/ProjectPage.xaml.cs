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
    public partial class ProjectPage : Page
    {
        private readonly ProjectController projectController = new ProjectController();
        private readonly TaskProjectController taskProjectController = new TaskProjectController();
        private readonly TeamController teamController = new TeamController();
        private Project proyectoActual;

        public ProjectPage(int projId)
        {
            InitializeComponent();
            CargarProyecto(projId);
        }

        private void CargarProyecto(int projId)
        {
            proyectoActual = projectController.GetProjectById(projId);

            if (proyectoActual == null)
            {
                MessageBox.Show("No se pudo cargar el proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            txtNombreProyecto.Text = proyectoActual.Nombre;
            txtDescripcionProyecto.Text = proyectoActual.Descripcion;
            txtFechasProyecto.Text = $"Inicio: {proyectoActual.Finicio:dd/MM/yyyy} - Fin: {proyectoActual.Fterminacion:dd/MM/yyyy}";

            CargarTareas(projId);
            CargarMiembrosDelEquipo();
        }

        private void CargarTareas(int projId)
        {
            var tareas = taskProjectController.GetTasksByProjectId(projId); // ✅ CORRECTO

            PanelTareas.Children.Clear();

            if (tareas == null || tareas.Count == 0)
            {
                var sinTareas = new TextBlock
                {
                    Text = "No hay tareas asociadas al proyecto.",
                    Foreground = System.Windows.Media.Brushes.Gray,
                    FontStyle = FontStyles.Italic
                };
                PanelTareas.Children.Add(sinTareas);
                return;
            }

            foreach (var tarea in tareas)
            {
                var tareaText = new TextBlock
                {
                    Text = $"- {tarea.Titulo}: {tarea.Descripcion} (Entrega: {tarea.Fentrega:dd/MM/yyyy})",
                    Margin = new Thickness(0, 5, 0, 0)
                };
                PanelTareas.Children.Add(tareaText);
            }
        }



        private void AgregarMiembro_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmailMiembro.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Debe ingresar un email válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var teamId = proyectoActual.Team?.TeamId;

            if (teamId == null)
            {
                MessageBox.Show("Este proyecto no tiene un equipo asignado.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            teamController.AddUserToTeam(teamId.Value, email);
            txtEmailMiembro.Clear();
            CargarMiembrosDelEquipo();

        }

        private void CargarMiembrosDelEquipo()
        {
            PanelMiembros.Children.Clear();

            var team = proyectoActual.Team;
            if (team == null || team.Miembros == null || team.Miembros.Count == 0)
            {
                var sinMiembros = new TextBlock
                {
                    Text = "Este proyecto no tiene miembros asignados.",
                    Foreground = Brushes.Gray,
                    FontStyle = FontStyles.Italic
                };
                PanelMiembros.Children.Add(sinMiembros);
                return;
            }

            foreach (var miembro in team.Miembros)
            {
                var miembroText = new TextBlock
                {
                    Text = $"- {miembro.Pnombre} {miembro.Papellido} ({miembro.RolUser?.Descripcion ?? "Sin rol"})",
                    Margin = new Thickness(0, 5, 0, 0)
                };
                PanelMiembros.Children.Add(miembroText);
            }
        }

    }
}
