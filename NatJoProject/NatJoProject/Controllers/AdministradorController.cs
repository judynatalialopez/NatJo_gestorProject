using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatJoProject.Models;
using NatJoProject.Services;
using NatJoProject.Views;

namespace NatJoProject.Controllers
{

    namespace NatJoProject.Controllers
    {
        public class AdministradorController
        {
            private readonly AdministradorService adminService = new AdministradorService();

            public void InsertAdministrador(Administrador admin)
            {
                bool result = adminService.InsertAdministrador(admin);

                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[INFO] Administrador insertado con éxito: {admin.Email}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] No se pudo insertar el administrador: {admin.Email}");
                    Console.ResetColor();
                }
            }

            public void GetAdministradorById(int adminId)
            {
                var admin = adminService.GetAdministradorById(adminId);

                if (admin != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Administrador encontrado: {admin.P_Nombre} {admin.P_Apellido} ({admin.Email})");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Administrador con ID {adminId} no encontrado.");
                    Console.ResetColor();
                }
            }

            public void UpdateAdministrador(Administrador admin)
            {
                bool result = adminService.UpdateAdministrador(admin);

                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[INFO] Administrador {admin.AdminId} actualizado con éxito.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] No se pudo actualizar el administrador {admin.AdminId}.");
                    Console.ResetColor();
                }
            }

            public void DeleteAdministrador(int adminId)
            {
                bool result = adminService.DeleteAdministrador(adminId);

                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[INFO] Administrador {adminId} eliminado con éxito.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] No se pudo eliminar el administrador {adminId}.");
                    Console.ResetColor();
                }
            }

            public void ListarAdministradores()
            {
                var lista = adminService.GetAllAdministradores();

                if (lista.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No hay administradores registrados.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Lista de Administradores ===");
                foreach (var admin in lista)
                {
                    Console.WriteLine($"{admin.AdminId}: {admin.P_Nombre} {admin.S_Nombre} {admin.P_Apellido} {admin.S_Apellido} - {admin.Email}");
                }
                Console.ResetColor();
            }
        }
    }
}

