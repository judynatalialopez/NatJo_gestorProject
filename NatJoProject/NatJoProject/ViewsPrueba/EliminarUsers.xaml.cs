using NatJoProject.Services;
using System.Windows;

namespace NatJoProject.ViewsPrueba
{
    public partial class EliminarUsers : Window
    {
        private readonly UserService _userService;

        public EliminarUsers()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtUserId.Text.Trim();

            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Por favor ingrese un ID válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmar eliminación
            var confirmResult = MessageBox.Show($"¿Está seguro que desea eliminar el usuario con ID '{userId}'?",
                                                "Confirmar eliminación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.Yes)
            {
                bool eliminado = _userService.DeleteUser(userId);

                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario. Verifique el ID o intente nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
