using NatJoProject.Models;
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
using System.Windows.Shapes;
using NatJoProject.Controllers;

namespace NatJoProject.Views
{
    /// <summary>
    /// Lógica de interacción para CrearProyecto.xaml
    /// </summary>
    public partial class CrearProyecto : Window
    {

        RolController rolController = new RolController();
        UserController userController = new UserController();
        ProjectController projectController = new ProjectController();
        MemberController memberController = new MemberController();
        TeamController teamController = new TeamController();

        public CrearProyecto()
        {
            InitializeComponent();
        }

        private void InsertarProyecto_Click(object sender, RoutedEventArgs e)
        {

            // Validar que nombres y apellidos solo contengan letras (sin números ni espacios)
            bool ValidarSoloLetras(string texto) =>
                !string.IsNullOrEmpty(texto) && texto.All(c => char.IsLetter(c));

            if (!ValidarSoloLetras(txtNombre.Text.Trim()))
            {
                MessageBox.Show("El primer nombre solo debe contener letras, sin números ni espacios.", "Validación Nombre", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ValidarSoloLetras(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("La descripcion solo debe contener letras, sin números.", "Validación Descripcion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaInicio;
            if (!DateTime.TryParse(txtFechaInicio.Text.Trim(), out fechaInicio))
            {
                MessageBox.Show("Fecha de nacimiento inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaFinal;
            if (!DateTime.TryParse(txtFechaFinal.Text.Trim(), out fechaFinal))
            {
                MessageBox.Show("Fecha de nacimiento inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text)
                || string.IsNullOrWhiteSpace(txtFechaInicio.Text) || string.IsNullOrWhiteSpace(txtFechaFinal.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //OBTENER ID DEL USUARIO
            string idUser = SesionApp.UsuarioActual.Id;

            // Crear objeto User para el owner
            User owner = new User { Id = idUser };

            //OBTENER ROL ID
            Rol? rol = rolController.GetRolById(2);

            //Crear miembros
            Member member = new Member(
                Id: idUser,
                RolUser: rol,
                IndOwner: 'S',
                IndAdmin: 'S'
            );
            memberController.InsertMember(member);

            // Crear lista de miembros
            List<Member> miembros = new List<Member> { member };

            // Crear usuario con valores correctos
            Project project = new Project(
                Nombre: txtNombre.Text,
                Descripcion: txtDescripcion.Text,
                Team: null,
                Finicio: fechaInicio,
                Fterminacion: fechaFinal
            );
            // Insertar proyecto y obtener ID generado
            int projectId = projectController.InsertProject(project);

            //Crear teams
            Team team = new Team(
                Nombre: txtNombre.Text,
                IndActivo: 'N',
                Miembros: miembros,
                Proyecto: project,
                Owner: owner  
            );
            int teamId = teamController.InsertTeam(team);

            project.Team = team; // Asocias el team completo
            projectController.UpdateProject(project);


            try
            {
                MessageBox.Show($"Proyecto insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Proyecto.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            BacklogView backlogView = new BacklogView();
            backlogView.Show();
            this.Close();
        }
    }
}
