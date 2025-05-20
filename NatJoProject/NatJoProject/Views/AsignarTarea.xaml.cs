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

        public AsignarTarea(Project proyecto)
        {
            InitializeComponent();
            proyectoActual = proyecto;
        }
    }
}
