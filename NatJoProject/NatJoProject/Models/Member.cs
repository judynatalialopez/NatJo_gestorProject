using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    class Member : User
    {
        public Rol rolUser { get; set; }
        public char indOwner { get; set; }
        public char indAdmin { get; set; }

        public Member(Rol rolUser, char indOwner, char indAdmin)
            : base(id, pNombre, sNombre, pApellido, ndocIdent, tipo_docIdent, pais, ciudad, sexo, fNacimiento, nTelefono1, nTelefono2, direccion, login, pwd, email, indBloqueado, indActivo)
        {
            this.rolUser = rolUser;
            this.indOwner = indOwner;
            this.indAdmin = indAdmin;
        }
    }

}
