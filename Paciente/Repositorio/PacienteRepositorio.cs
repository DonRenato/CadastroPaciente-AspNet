using PacienteDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Repositorio
{
    public class PacienteRepositorio : IRepositorio<Paciente>
    {
        private PacienteContexto contexto;

        public PacienteRepositorio (PacienteContexto pacienteContexto)
        {
            this.contexto = pacienteContexto;
        }

        public IEnumerable<Paciente> GetTodos()
        {
            return contexto.Pacientes.ToList();
        }
    }
}
