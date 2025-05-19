using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Lógica de interacción para Sexos.xaml
    /// </summary>
    public partial class Sexos : Window
    {
        SexoController sexoController = new SexoController();
        public Sexos()
        {
            InitializeComponent();
        }

        private void InsertSexo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("por favor, complete todos los campos.", "validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Sexo sexo = new Sexo
            {
                Descripcion = txtDescripcion.Text.Trim()
            };
            try
            {
                sexoController.InsertSexo(sexo);
                MessageBox.Show($"Rol '{sexo.SxId}' insertado con exito.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo insertar el sexo '{sexo.SxId}'.\n\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
            
        }
        
    }
}
