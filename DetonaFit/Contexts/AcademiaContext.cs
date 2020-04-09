using DetonaFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DetonaFit.Contexts
{
    public class AcademiaContext : DbContext
    {
        public DbSet<Aluno> Aluno { get; set; }

        public DbSet<Instrutor> Instrutor { get; set; }

        public DbSet<Login> Login { get; set; }

        public DbSet<TipoUsuario> TipoUsuario { get; set; }
    }
}