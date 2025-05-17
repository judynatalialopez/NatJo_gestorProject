using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                Console.WriteLine($"[INFO] Ciudad {ciudad.CityId} insertada con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar la Ciudad {ciudad.CityId}.");
                Console.ResetColor();
            }
        }

        public void GetCiudadById(int cityId)
        {
            var ciudad = ciudadService.GetCiudadById(cityId);

            if (ciudad != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ciudad encontrada: {ciudad.Nombre} (ID: {ciudad.CityId})");
                Console.WriteLine($"Código Postal: {ciudad.CodPostal}, País: {ciudad.Pais.Nombre}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró la ciudad con ID {cityId}.");
                Console.ResetColor();
            }
        }

        public List<Ciudad> GetAllCiudades()
        {
            var ciudades = ciudadService.GetAllCiudades();

            if (ciudades.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Total de ciudades encontradas: {ciudades.Count}");
                foreach (var ciudad in ciudades)
                {
                    Console.WriteLine($"ID: {ciudad.CityId} | Nombre: {ciudad.Nombre}, País: {ciudad.Pais.Nombre}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No se encontraron ciudades.");
                Console.ResetColor();
            }

            return ciudades;
        }

        public void UpdateCiudad(Ciudad ciudad)
        {
            bool result = ciudadService.UpdateCiudad(ciudad);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Ciudad {ciudad.CityId} actualizada con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar la Ciudad {ciudad.CityId}.");
                Console.ResetColor();
            }
        }

        public void DeleteCiudad(int cityId)
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
