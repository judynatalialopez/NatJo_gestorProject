using NatJoProject.Pages;
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
    /// Lógica de interacción para BacklogView.xaml
    /// </summary>
    public partial class BacklogView : Window
    {
        public BacklogView()
        {
            InitializeComponent();

            private void Dashboard_Click(object sender, RoutedEventArgs e)
            {
                MainFrame.Navigate(new DashboardPage());
            }

            private void Backlog_Click(object sender, RoutedEventArgs e)
            {
                MainFrame.Navigate(new BacklogPage());
            }
        }
    }
}
