using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Administrador
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string P_Nombre { get; set; }
        public string S_Nombre { get; set; }
        public string P_Apellido { get; set; }
        public string S_Apellido { get; set; }

        public Administrador()
        {
        }

        public Administrador(int Admin_Id, string Email, string Pwd, string P_Nombre, string S_Nombre, string P_Apellido, string S_Apellido)
        {
            this.AdminId = Admin_Id;
            this.Email = Email;
            this.Pwd = Pwd;
            this.P_Nombre = P_Nombre;
            this.S_Nombre = S_Nombre;
            this.P_Apellido = P_Apellido;
            this.S_Apellido = S_Apellido;
        }

        public Administrador(string Email, string Pwd)
        {
            this.Email = Email;
            this.Pwd = Pwd;

        }
    }
}
