using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NatJoProject.Services; 

namespace NatJoProject.Pages
{
    public partial class ProjectPage : Page
    {
        
        public ObservableCollection<Task> Tareas { get; set; }

        public ProjectPage()
        {
            InitializeComponent();
            this.DataContext = this; 
            LoadTasks(); 
        }

        
        public async void LoadTasks()
        {
           
            var taskService = new TaskProjectService(); 
            var tasks = await taskService.GetTasksForProjectAsync();

            if (tasks != null)
            {
                Tareas = new ObservableCollection<Task>(tasks);
                OnPropertyChanged(nameof(Tareas));
            }
            else
            {
                Tareas = new ObservableCollection<Task>(); 
            }
        }
    }

    public class Task
    {
        public string Titulo { get; set; }
        public Estado Estado { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fentrerga { get; set; }
        public ObservableCollection<Responsable> Responsable { get; set; }
        public ObservableCollection<Comentario> Comentarios { get; set; }
    }

    public class Responsable
    {
        public string Pnombre { get; set; }
        public string Papellido { get; set; }
    }

    public class Comentario
    {
        public DateTime Fcomentario { get; set; }
        public string Autor { get; set; }
        public string Texto { get; set; }
    }

    public class Estado
    {
        public string Descripcion { get; set; }
    }
}
