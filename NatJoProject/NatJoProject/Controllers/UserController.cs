using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using NatJoProject.Database;
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

        public bool LoginUser(string email, string pwd)
        {
            bool loginSuccess = userService.UserLogin(email, pwd);

            if (loginSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[LOGIN ÉXITO] Bienvenido");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[LOGIN ERROR] Email o contraseña incorrectos.");
                Console.ResetColor();
                return false;
            }
        }

        public bool VerifyEmail(string email)
        {
            bool existe = userService.VerifyEmail(email);

            if (existe)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[VALIDACIÓN EMAIL] El email '{email}' ya está registrado.");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[VALIDACIÓN EMAIL] El email '{email}' está disponible.");
                Console.ResetColor();
                return false;
            }
        }

        public bool VerifyDocId(string docId)
        {
            bool existe = userService.VerifyDocId(docId);

            if (existe)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[VALIDACIÓN documento] El documento '{docId}' ya está registrado.");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[VALIDACIÓN documento] El documento '{docId}' está disponible.");
                Console.ResetColor();
                return false;
            }
        }

        public User? GetUserByLogin(string login)
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

            return user; 
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
