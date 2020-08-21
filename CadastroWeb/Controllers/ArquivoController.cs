using PacienteDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroWeb.Controllers
{
    public class ArquivoController : Controller
    {
        // GET: Arquivo
        public ActionResult ExibirImagem(int id)
        {
            using (PacienteContexto db = new PacienteContexto())
            {
                var arquivoRetorno = db.Pacientes.Find(id);
                return File(arquivoRetorno.Imagem, arquivoRetorno.ImagemTipo);
            }
                
        }
    }
}