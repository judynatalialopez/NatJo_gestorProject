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

        public Member(string Id,
            Rol RolUser,
            char IndOwner,
            char IndAdmin)
            : base(Id)
        {
            this.RolUser = RolUser;
            this.IndOwner = IndOwner;
            this.IndAdmin = IndAdmin;
        }

        public Member(string Id, string Pnombre, string? Snombre, string Papellido, string? Sapellido, string NdocIdent, string Tipo_docIdent, Pais Pais, Ciudad Ciudad, Sexo Sexo, DateOnly Fnacimiento, string Ntelefono1, string? Ntelefono2, string Direccion, string Login, string Pwd, string Email, char IndBloqueado, char IndActivo,
            Rol RolUser,
            char IndOwner,
            char IndAdmin)
            : base(Id, Pnombre, Snombre, Papellido,Sapellido, NdocIdent, Tipo_docIdent, Pais, Ciudad, Sexo, Fnacimiento, Ntelefono1, Ntelefono2 ?? "", Direccion, Login, Pwd, Email, IndBloqueado, IndActivo)
        {
            this.RolUser = RolUser;
            this.IndOwner = IndOwner;
            this.IndAdmin = IndAdmin;
        }
    }

}
