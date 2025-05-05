using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class SexoController
    {
        private readonly SexoService sexoService = new SexoService();

        public void InsertSexo(Sexo sexo)
        {
            bool result = sexoService.InsertSexo(sexo);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Sexo {sexo.sxId} insertado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el Sexo {sexo.sxId}.");
                Console.ResetColor();
            }
        }

        public void GetSexoById(string sxId)
        {
            var sexo = sexoService.GetSexoById(sxId);

            if (sexo != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Sexo encontrado: {sexo.sxId} - {sexo.descripcion}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró el sexo con ID {sxId}.");
                Console.ResetColor();
            }
        }

        public void UpdateSexo(Sexo sexo)
        {
            bool result = sexoService.UpdateSexo(sexo);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Sexo {sexo.sxId} actualizado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el Sexo {sexo.sxId}.");
                Console.ResetColor();
            }
        }

        public void DeleteSexo(string sxId)
        {
            bool result = sexoService.DeleteSexo(sxId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Sexo {sxId} eliminado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el Sexo {sxId}.");
                Console.ResetColor();
            }
        }

        public void GetAllSexos()
        {
            var sexos = sexoService.GetAllSexos();

            if (sexos.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Total de sexos encontrados: {sexos.Count}");
                foreach (var sexo in sexos)
                {
                    Console.WriteLine($"ID: {sexo.sxId} | Descripción: {sexo.descripcion}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No se encontraron sexos.");
                Console.ResetColor();
            }
        }
    }
}