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
        public Rol rolUser { get; set; }
        public char indOwner { get; set; }
        public char indAdmin { get; set; }

        public Member()
        {
        }
        public Member(string id,
            string pNombre,
            string? sNombre,
            string pApellido,
            string? sApellido,
            string ndocIdent,
            string tipo_docIdent,
            Pais pais,
            Ciudad ciudad,
            Sexo sexo,
            DateOnly fNacimiento,
            int nTelefono1,
            int? nTelefono2,
            string direccion,
            string login,
            string pwd,
            string email,
            char indBloqueado,
            char indActivo,
            Rol rolUser,
            char indOwner,
            char indAdmin)
            : base(id, pNombre, sNombre, pApellido, ndocIdent, tipo_docIdent, pais, ciudad, sexo, fNacimiento, nTelefono1, nTelefono2 ?? 0, direccion, login, pwd, email, indBloqueado, indActivo)
        {
            this.rolUser = rolUser;
            this.indOwner = indOwner;
            this.indAdmin = indAdmin;
        }
    }

}
