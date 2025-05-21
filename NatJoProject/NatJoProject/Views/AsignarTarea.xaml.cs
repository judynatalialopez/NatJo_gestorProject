using NatJoProject.Controllers;
using NatJoProject.Models;
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
    /// Lógica de interacción para AsignarTarea.xaml
    /// </summary>
    public partial class AsignarTarea : Window
    {
        private Project proyectoActual;
        Project project;

        //CONTROLLERS
        private TaskProjectController taskProjectController = new TaskProjectController();
        private TaskEstadoController taskEstadoController = new TaskEstadoController();

        public AsignarTarea(Project proyecto)
        {
            InitializeComponent();
            proyectoActual = proyecto;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (proyectoActual.Team != null && proyectoActual.Team.Miembros != null)
            {

                cmbMiembros.ItemsSource = proyectoActual.Team.Miembros;
                cmbMiembros.DisplayMemberPath = "Pnombre"; 
                cmbMiembros.SelectedValuePath = "Id";
            }
            else
            {
                MessageBox.Show("No hay miembros en el equipo o el equipo es null.");
            }
        }

        private void InsertarProyecto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AsignarTarea_Click(object sender, RoutedEventArgs e)
        {

            //OBTENER EL OBJETO TASK ESTADO POR ID
            var estadoTask = taskEstadoController.GetEstadoById(4);

            try
            {
                // Crear la tarea con los datos del formulario
                var taskProject = new TaskProject
                {
                    Titulo = txtTitulo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Responsable = new List<Member> { (Member)cmbMiembros.SelectedItem },
                    Estado = estadoTask,
                    Fentrerga = DateTime.Now.AddDays(7), 
                    Comentarios = new List<Comment>()
                };

                // Llamamos al método que guarda la tarea y asigna responsable
                bool resultado = taskProjectController.InsertTask0(taskProject);

                if (resultado)
                {
                    MessageBox.Show("Tarea asignada correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al asignar la tarea.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
