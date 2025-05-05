using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatJoProject.Models
{
    public class User
    {
        public string id { get; set; }
        public string pNombre { get; set; }
        public string? sNombre { get; set; }
        public string pApellido { get; set; }
        public string? sApellido { get; set; }
        public string ndocIdent { get; set; }
        public string tipo_docIdent { get; set; }
        public Pais pais { get; set; }
        public Ciudad ciudad { get; set; }
        public Sexo sexo { get; set; }
        public DateOnly fNacimiento { get; set; }
        public int nTelefono1 { get; set; }
        public int? nTelefono2 { get; set; }
        public string direccion { get; set; }
        public string login { get => login; set => login = value; }
        public string pwd { get => pwd; set => pwd = value; }
        public string email { get => email; set => email = value; }

        public char indBloqueado {  get; set; }
        public char indActivo { get; set; }

        public User()
        {
        }
        public User(string id, string pNombre, string? sNombre, string pApellido, string ndocIdent, string tipo_docIdent, Pais pais, Ciudad ciudad, Sexo sexo, DateOnly fNacimiento, int nTelefono1, int nTelefono2, string direccion, string login, string pwd, string email, char indBloqueado, char indActivo)
        {
            this.id = id;
            this.pNombre = pNombre;
            this.sNombre = sNombre;
            this.pApellido = pApellido;
            this.sApellido = sApellido;
            this.ndocIdent = ndocIdent;
            this.tipo_docIdent = tipo_docIdent;
            this.pais = pais;
            this.ciudad = ciudad;
            this.sexo = sexo;
            this.fNacimiento = fNacimiento;
            this.nTelefono1 = nTelefono1;
            this.nTelefono2 = nTelefono2;
            this.direccion = direccion;
            this.login = login;
            this.pwd = pwd;
            this.email = email;
            this.indBloqueado = indBloqueado;
            this.indActivo = indActivo;
        }
    }

}
