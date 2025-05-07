using System;
using System.Collections.Generic;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class MemberController
    {
        private readonly MemberService memberService = new MemberService();

        public void InsertMember(Member member)
        {
            bool result = memberService.InsertMember(member);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Miembro {member.Id} insertado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el Miembro {member.Id}.");
                Console.ResetColor();
            }
        }

        public void GetMemberById(string userId)
        {
            var member = memberService.GetMemberById(userId);

            if (member != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Miembro encontrado: {member.Pnombre} {member.Snombre}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Miembro con ID {userId} no encontrado.");
                Console.ResetColor();
            }
        }

        public void UpdateMember(Member member)
        {
            bool result = memberService.UpdateMember(member);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Miembro {member.Id} actualizado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el Miembro {member.Id}.");
                Console.ResetColor();
            }
        }

        public void DeleteMember(string userId)
        {
            bool result = memberService.DeleteMember(userId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Miembro {userId} eliminado con éxito.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el Miembro {userId}.");
                Console.ResetColor();
            }
        }
    }
}