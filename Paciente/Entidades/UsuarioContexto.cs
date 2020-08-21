using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Entidades
{
    public class UsuarioContexto : DbContext
    {
        public UsuarioContexto()
        : base("name=ConexaoPacientes")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ConsultaContexto>(new CreateDatabaseIfNotExists<ConsultaContexto>());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public object Usuario { get; set; }
    }
}

