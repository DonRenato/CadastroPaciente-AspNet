using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Entidades
{
    public class ConsultaContexto : DbContext
    {
        public ConsultaContexto()
        : base("name=ConexaoPacientes")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ConsultaContexto>(new CreateDatabaseIfNotExists<ConsultaContexto>());
        }

        public DbSet<Consulta> Consultas { get; set; }
    }
}
