using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class CiudadController
    {
        private readonly CiudadService ciudadService = new CiudadService();

        public void InsertCiudad(Ciudad ciudad)
        {
            bool result = ciudadService.InsertCiudad(ciudad);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Ciudad {ciudad.cityId} insertada con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar la Ciudad {ciudad.cityId}.");
                Console.ResetColor();
            }
        }

        public void GetCiudadById(string cityId)
        {
            var ciudad = ciudadService.GetCiudadById(cityId);

            if (ciudad != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ciudad encontrada: {ciudad.nombre} (ID: {ciudad.cityId})");
                Console.WriteLine($"Código Postal: {ciudad.codPostal}, País: {ciudad.pais.nombre}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró la ciudad con ID {cityId}.");
                Console.ResetColor();
            }
        }

        public void GetAllCiudades()
        {
            var ciudades = ciudadService.GetAllCiudades();

            if (ciudades.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Total de ciudades encontradas: {ciudades.Count}");
                foreach (var ciudad in ciudades)
                {
                    Console.WriteLine($"ID: {ciudad.cityId} | Nombre: {ciudad.nombre}, País: {ciudad.pais.nombre}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No se encontraron ciudades.");
                Console.ResetColor();
            }
        }

        public void UpdateCiudad(Ciudad ciudad)
        {
            bool result = ciudadService.UpdateCiudad(ciudad);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Ciudad {ciudad.cityId} actualizada con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar la Ciudad {ciudad.cityId}.");
                Console.ResetColor();
            }
        }

        public void DeleteCiudad(string cityId)
        {
            bool result = ciudadService.DeleteCiudad(cityId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Ciudad {cityId} eliminada con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar la Ciudad {cityId}.");
                Console.ResetColor();
            }
        }
    }
}
