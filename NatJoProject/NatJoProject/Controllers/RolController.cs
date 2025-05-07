using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class RolController
    {
        private readonly RolService rolService = new RolService();

        public void InsertRol(Rol rol)
        {
            bool result = rolService.InsertRol(rol);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Rol {rol.RolId} insertado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el Rol {rol.RolId}.");
                Console.ResetColor();
            }
        }

        public void GetRolById(string rolId)
        {
            var rol = rolService.GetRolById(rolId);

            if (rol != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Rol encontrado: {rol.Descripcion}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Rol con ID {rolId} no encontrado.");
                Console.ResetColor();
            }
        }

        public void UpdateRol(Rol rol)
        {
            bool result = rolService.UpdateRol(rol);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Rol {rol.RolId} actualizado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el Rol {rol.RolId}.");
                Console.ResetColor();
            }
        }

        public void DeleteRol(string rolId)
        {
            bool result = rolService.DeleteRol(rolId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Rol {rolId} eliminado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el Rol {rolId}.");
                Console.ResetColor();
            }
        }
    }
}