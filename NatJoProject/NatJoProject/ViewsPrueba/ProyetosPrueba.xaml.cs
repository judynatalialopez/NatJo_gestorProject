using NatJoProject.Services;
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

namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para ProyetosPrueba.xaml
    /// </summary>
    public partial class ProyetosPrueba : Window
    {
        private readonly ProjectService projectService;

        public ProyetosPrueba()
        {
            InitializeComponent();
            projectService = new ProjectService();
        }

        private void LoadProjects_Click(object sender, RoutedEventArgs e)
        {
            var proyectos = projectService.GetProjectsByUserId("1047037318");

            // Asigna directamente la lista sin modificar nada
            ProjectsDataGrid.ItemsSource = proyectos;
        }
    }

}
