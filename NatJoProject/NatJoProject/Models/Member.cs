using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class Member : User
    {
        public Rol RolUser { get; set; }
        public char IndOwner { get; set; }
        public char IndAdmin { get; set; }

        public Member()
        {
        }
        public Member(string Id, string Pnombre, string? Snombre, string Papellido, string NdocIdent, string Tipo_docIdent, Pais Pais, Ciudad Ciudad, Sexo Sexo, DateOnly Fnacimiento, int Ntelefono1, int? Ntelefono2, string Direccion, string Login, string Pwd, string Email, char IndBloqueado, char IndActivo,
            Rol RolUser,
            char IndOwner,
            char IndAdmin)
            : base(Id, Pnombre, Snombre, Papellido, NdocIdent, Tipo_docIdent, Pais, Ciudad, Sexo, Fnacimiento, Ntelefono1, Ntelefono2 ?? 0, Direccion, Login, Pwd, Email, IndBloqueado, IndActivo)
        {
            this.RolUser = RolUser;
            this.IndOwner = IndOwner;
            this.IndAdmin = IndAdmin;
        }
    }

}
