using System;
using System.Collections.Generic;
using System.Windows;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class PaisController
    {
        private readonly PaisService paisService = new PaisService();

        public void InsertPais(Pais pais)
        {
            bool result = paisService.InsertPais(pais);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] País {pais.PaisId} insertado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el País {pais.PaisId}.");
                Console.ResetColor();
            }
        }

        public void GetPaisById(int paisId)
        {
            var pais = paisService.GetPaisById(paisId);

            if (pais != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"País encontrado: {pais.Nombre} ({pais.Dominio})");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"País con ID {paisId} no encontrado.");
                Console.ResetColor();
            }
        }

        public void UpdatePais(Pais pais)
        {
            bool result = paisService.UpdatePais(pais);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] País {pais.PaisId} actualizado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el País {pais.PaisId}.");
                Console.ResetColor();
            }
        }

        public void DeletePais(int paisId)
        {
            bool result = paisService.DeletePais(paisId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] País {paisId} eliminado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el País {paisId}.");
                Console.ResetColor();
            }
        }
    }
}