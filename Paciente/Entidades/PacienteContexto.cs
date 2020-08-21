using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Entidades
{
    public class PacienteContexto : DbContext
    {
        public PacienteContexto()
            : base("name=ConexaoPacientes")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PacienteContexto>(new CreateDatabaseIfNotExists<PacienteContexto>());
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public object Paciente { get; set; }
    }


}
