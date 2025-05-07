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
        public string Id { get; set; }
        public string Pnombre { get; set; }
        public string? Snombre { get; set; }
        public string Papellido { get; set; }
        public string? Sapellido { get; set; }
        public string NdocIdent { get; set; }
        public string Tipo_docIdent { get; set; }
        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }
        public Sexo Sexo { get; set; }
        public DateOnly Fnacimiento { get; set; }
        public int Ntelefono1 { get; set; }
        public int? Ntelefono2 { get; set; }
        public string Direccion { get; set; }
        public string Login { get => Pwd; set => Pwd = value; }
        public string Pwd { get => Pwd; set => Pwd = value; }
        public string Email { get => Pwd; set => Pwd = value; }

        public char IndBloqueado {  get; set; }
        public char IndActivo { get; set; }

        public User()
        {
        }
        public User(string Id, string Pnombre, string? Snombre, string Papellido, string? Sapellido, string NdocIdent, string Tipo_docIdent, Pais Pais, Ciudad Ciudad, Sexo Sexo, DateOnly Fnacimiento, int Ntelefono1, int Ntelefono2, string Direccion, string Login, string Pwd, string Email, char IndBloqueado, char IndActivo)
        {
            this.Id = Id;
            this.Pnombre = Pnombre;
            this.Snombre = Snombre;
            this.Papellido = Papellido;
            this.Sapellido = Sapellido;
            this.NdocIdent = NdocIdent;
            this.Tipo_docIdent = Tipo_docIdent;
            this.Pais = Pais;
            this.Ciudad = Ciudad;
            this.Sexo = Sexo;
            this.Fnacimiento = Fnacimiento;
            this.Ntelefono1 = Ntelefono1;
            this.Ntelefono2 = Ntelefono2;
            this.Direccion = Direccion;
            this.Login = Login;
            this.Pwd = Pwd;
            this.Email = Email;
            this.IndBloqueado = IndBloqueado;
            this.IndActivo = IndActivo;
        }
    }

}
