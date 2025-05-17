using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NatJoProject.Models;
using NatJoProject.Services;
using System.Windows;

namespace NatJoProject.Controllers
{
    public class TaskEstadoController
    {
        private readonly TaskEstadoService estadoService = new TaskEstadoService();

        public void InsertEstado(TaskEstado estado)
        {
            bool result = estadoService.InsertEstado(estado);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? $"[INFO] Estado {estado.EstId} insertado." : $"[ERROR] No se pudo insertar el estado.");
            Console.ResetColor();
        }

        public void GetEstadoById(int id)
        {
            var estado = estadoService.GetEstadoById(id);
            if (estado != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Estado encontrado: {estado.EstId} - {estado.Descripcion}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Estado no encontrado.");
            }
            Console.ResetColor();
        }

        public void UpdateEstado(TaskEstado estado)
        {
            bool result = estadoService.UpdateEstado(estado);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? $"[INFO] Estado actualizado." : $"[ERROR] No se pudo actualizar.");
            Console.ResetColor();
        }

        public void DeleteEstado(int id)
        {
            bool result = estadoService.DeleteEstado(id);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? $"[INFO] Estado eliminado." : $"[ERROR] No se pudo eliminar.");
            Console.ResetColor();
        }

        public void GetAllEstados()
        {
            var lista = estadoService.GetAllEstados();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Listado de Estados:");
            foreach (var est in lista)
            {
                Console.WriteLine($"ID: {est.EstId} | Descripción: {est.Descripcion}");
            }
            Console.ResetColor();
        }
    }
}
