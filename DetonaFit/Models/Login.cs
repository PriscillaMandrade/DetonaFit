using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DetonaFit.Models
{
    [Table("TB_Login")]
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        [Column("Login")]
        public string LoginDesc { get; set; }

        public string Senha { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public int TipoUsuarioID { get; set; }

        public Aluno Aluno { get; set; }

        public int AlunoID { get; set; }
    }
}