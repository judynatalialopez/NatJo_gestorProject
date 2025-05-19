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
    public class TeamController
    {
        private readonly TeamService teamService = new TeamService(new MemberService());
        public void InsertTeam(Team team)
        {
            bool result = teamService.InsertTeam(team);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Equipo {team.TeamId} insertado correctamente.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el equipo {team.TeamId}.");
            }

            Console.ResetColor();
        }

        public void GetTeamById(string teamId)
        {
            var team = teamService.GetTeamById(teamId);

            if (team != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Equipo: {team.Nombre} (ID: {team.TeamId})");
                Console.WriteLine($"Proyecto: {team.Proyecto.Nombre}, Owner: {team.Owner.Pnombre} {team.Owner.Papellido}");
                Console.WriteLine($"Miembros: {team.Miembros.Count}");
                foreach (var m in team.Miembros)
                    Console.WriteLine($" - {m.Pnombre} {m.Papellido} ({m.RolUser.Descripcion})");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No se encontró el equipo con ID {teamId}.");
            }

            Console.ResetColor();
        }

        public void GetAllTeams()
        {
            var teams = teamService.GetAllTeams();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Total de equipos: {teams.Count}");
            foreach (var team in teams)
            {
                Console.WriteLine($"ID: {team.TeamId} | Nombre: {team.Nombre} | Activo: {team.IndActivo}");
            }
            Console.ResetColor();
        }

        public void UpdateTeam(Team team)
        {
            bool result = teamService.UpdateTeam(team);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Equipo {team.TeamId} actualizado correctamente.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el equipo {team.TeamId}.");
            }

            Console.ResetColor();
        }

        public void DeleteTeam(string teamId)
        {
            bool result = teamService.DeleteTeam(teamId);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Equipo {teamId} eliminado con éxito.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo eliminar el equipo {teamId}.");
            }

            Console.ResetColor();
        }
    }
}

