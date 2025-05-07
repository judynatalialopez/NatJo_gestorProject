using System;
using System.Collections.Generic;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class UserController
    {
        private readonly UserService userService = new UserService();

        public void InsertUser(User user)
        {
            bool result = userService.InsertUser(user);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Usuario {user.Id} insertado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el Usuario {user.Id}.");
                Console.ResetColor();
            }
        }

        public void GetUserByLogin(string login)
        {
            var user = userService.GetUserByLogin(login);

            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Usuario encontrado: {user.Pnombre} {user.Papellido}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró el usuario con login {login}.");
                Console.ResetColor();
            }
        }

        // Nuevo método para obtener usuario por ID
        public void GetUserById(string id)
        {
            var user = userService.GetUserById(id);

            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Usuario encontrado: {user.Pnombre} {user.Papellido} (ID: {user.Id})");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró el usuario con ID {id}.");
                Console.ResetColor();
            }
        }

        public void UpdateUser(User user)
        {
            bool result = userService.UpdateUser(user);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Usuario {user.Id} actualizado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el Usuario {user.Id}.");
                Console.ResetColor();
            }
        }

        public void DeleteUser(string userId)
        {
            bool result = userService.DeleteUser(userId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Usuario {userId} eliminado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el Usuario {userId}.");
                Console.ResetColor();
            }
        }

        public void GetAllUsers()
        {
            var users = userService.GetAllUsers();

            if (users.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Total de usuarios encontrados: {users.Count}");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id} | Nombre: {user.Pnombre} {user.Papellido}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No se encontraron usuarios.");
                Console.ResetColor();
            }
        }
    }
}
