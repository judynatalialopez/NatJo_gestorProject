using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatJoProject.Models;
using NatJoProject.Services;

namespace NatJoProject.Controllers
{
    public class TeamController
    {
        private readonly TeamService teamService = new TeamService();

        public void InsertTeam(Team team)
        {
            bool result = teamService.InsertTeam(team);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Equipo {team.teamId} insertado correctamente.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo insertar el equipo {team.teamId}.");
            }

            Console.ResetColor();
        }

        public void GetTeamById(string teamId)
        {
            var team = teamService.GetTeamById(teamId);

            if (team != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Equipo: {team.nombre} (ID: {team.teamId})");
                Console.WriteLine($"Proyecto: {team.proyecto.nombre}, Owner: {team.owner.pNombre} {team.owner.pApellido}");
                Console.WriteLine($"Miembros: {team.miembros.Count}");
                foreach (var m in team.miembros)
                    Console.WriteLine($" - {m.pNombre} {m.pApellido} ({m.rolUser.descripcion})");
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
                Console.WriteLine($"ID: {team.teamId} | Nombre: {team.nombre} | Activo: {team.indActivo}");
            }
            Console.ResetColor();
        }

        public void UpdateTeam(Team team)
        {
            bool result = teamService.UpdateTeam(team);

            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] Equipo {team.teamId} actualizado correctamente.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] No se pudo actualizar el equipo {team.teamId}.");
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

