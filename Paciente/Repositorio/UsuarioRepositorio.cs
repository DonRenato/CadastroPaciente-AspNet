using PacienteDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Repositorio
{
    public class UsuarioRepositorio : IRepositorio3<Usuario>
    {
        private UsuarioContexto contexto;

        public UsuarioRepositorio(UsuarioContexto usuarioContexto)
        {
            this.contexto = usuarioContexto;
        }

        public IEnumerable<Usuario> GetTodos()
        {
            return contexto.Usuarios.ToList();
        }


    }
}

