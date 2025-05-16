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
using NatJoProject.Models;

namespace NatJoProject.ViewsPrueba
{
    /// <summary>
    /// Lógica de interacción para Estados_Task.xaml
    /// </summary>
    public partial class Estados_Task : Window
    {
        TaskEstadoController taskestadoController = new TaskEstadoController();
        public Estados_Task()
        {
            InitializeComponent();
        }       
        
        private void InsertEstado(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskEstado taskestado = new TaskEstado
            {
                EstId = txtId.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };
            try
            {
                taskestadoController.InsertEstado(taskestado);
                MessageBox.Show($"Sexo '{taskestado.EstId}' insertado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el Sexo '{taskestado.EstId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}