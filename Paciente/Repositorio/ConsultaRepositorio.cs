using PacienteDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Repositorio
{
    public class ConsultaRepositorio : IRepositorio2<Consulta>
    {
        private ConsultaContexto contexto;

        public ConsultaRepositorio(ConsultaContexto consultaContexto)
        {
            this.contexto = consultaContexto;
        }

        public IEnumerable<Consulta> GetTodos()
        {
            return contexto.Consultas.ToList();
        }


    }
}
