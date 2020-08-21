using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PacienteDominio.Entidades;
using PacienteDominio.Repositorio;
using PagedList;
using System.IO;
using System.Web.Configuration;


namespace CadastroWeb.Controllers
{
    public class PacienteController : Controller
    {
        private PacienteContexto db = new PacienteContexto();

        private IRepositorio<Paciente> _repositorioPaciente;

        private ConsultaContexto dbc = new ConsultaContexto();

        private IRepositorio2<Consulta> _repositorioConsulta;

        private UsuarioContexto dbu = new UsuarioContexto();

        private IRepositorio3<Usuario> _repositorioUsuario;

        public PacienteController()
        {
            _repositorioPaciente = new PacienteRepositorio(new PacienteContexto());
            _repositorioConsulta = new ConsultaRepositorio(new ConsultaContexto());
            _repositorioUsuario = new UsuarioRepositorio(new UsuarioContexto());

        }


        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (var dc = new UsuarioContexto())
                {
                    var _usuarios = dc.Usuarios.Where(a => a.NomeUsuario.Equals(u.NomeUsuario) && a.Senha.Equals(u.Senha)).FirstOrDefault();

                    if (_usuarios != null)
                    {

                        Session["usuarioLogadoID"] = _usuarios.ID.ToString();
                        Session["nomeUsuarioLogado"] = _usuarios.NomeUsuario.ToString();
                        return RedirectToAction("Catalogo");
                    }
                }
            }
            TempData["mensagem"] = string.Format("Login ou senha inválida");
            return View(u);
        }

        public ActionResult Catalogo(int? pagina)
        {


            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return View(_repositorioPaciente.GetTodos().ToPagedList(numeroPagina, tamanhoPagina));


        }

        // GET: Paciente
        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {

                return RedirectToAction("Catalogo");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        // GET: Paciente/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }


        // GET: Paciente/CreateLogado
        public ActionResult CreateLogado()
        {
            if (Session["usuarioLogadoID"] != null)
            {

                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Paciente/Create

        public ActionResult Create()
        {

            return View("Create");

        }

        // POST: Paciente/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PacienteID,Nome,Sobrenome,Endereço,Email,Telefone,Whatsapp,RG,CPF,Indicação,Dentista,Imagem,ImagemTipo")] Paciente paciente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Paciente
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }

                    paciente.Imagem = arqImagem.Imagem;
                    paciente.ImagemTipo = arqImagem.ImagemTipo;
                }
                db.Pacientes.Add(paciente);
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0} : foi incluido com sucesso", paciente.Nome);
                return RedirectToAction("Catalogo");

            }
            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PacienteID,Nome,Sobrenome,Endereço,Email,Telefone,Whatsapp,RG,CPF,Indicação,Dentista,Imagem,ImagemTipo")] Paciente paciente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Paciente
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }

                    paciente.Imagem = arqImagem.Imagem;
                    paciente.ImagemTipo = arqImagem.ImagemTipo;
                }
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0} : foi atualizado com sucesso", paciente.Nome);
                return RedirectToAction("Catalogo");
            }
            return View(paciente);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            db.Pacientes.Remove(paciente);
            db.SaveChanges();
            TempData["mensagem"] = string.Format("{0} : foi excluido com sucesso", paciente.Nome);
            return RedirectToAction("Catalogo");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult PesquisaLogado()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return RedirectToAction("Pesquisa");
            }
            else
            {
                return RedirectToAction("Login");
            }


        }



        [HttpGet]
        public ActionResult Pesquisa()
        {
            using (var db = new PacienteContexto())
            {
                var _paciente = db.Pacientes.OrderBy(c => c.Nome).ThenBy(m => m.Sobrenome).ToList();
                var data = new Paciente()
                {
                    Pacientes = _paciente
                };

                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Pesquisa(Paciente _paciente)
        {
            using (var db = new PacienteContexto())
            {
                var pacientePesquisa = from pacienterec in db.Pacientes
                                       where ((_paciente.Nome == null) || (pacienterec.Nome == _paciente.Nome))
                                       && ((_paciente.CPF == null) || (pacienterec.CPF == _paciente.CPF.Trim()))
                                       && ((_paciente.Dentista == null) || (pacienterec.Dentista == _paciente.Dentista))
                                       orderby pacienterec.Nome ascending
                                       select new
                                       {
                                           PacienteID = pacienterec.PacienteID,
                                           Nome = pacienterec.Nome,
                                           Sobrenome = pacienterec.Sobrenome,
                                           Endereço = pacienterec.Endereço,
                                           Email = pacienterec.Email,
                                           Telefone = pacienterec.Telefone,
                                           Whatsapp = pacienterec.Whatsapp,
                                           RG = pacienterec.RG,
                                           CPF = pacienterec.CPF,
                                           Indicação = pacienterec.Indicação,
                                           Dentista = pacienterec.Dentista

                                       };
                List<Paciente> listaPaciente = new List<Paciente>();

                foreach (var reg in pacientePesquisa)
                {
                    Paciente pacientevalor = new Paciente();
                    pacientevalor.PacienteID = reg.PacienteID;
                    pacientevalor.Nome = reg.Nome;
                    pacientevalor.Sobrenome = reg.Sobrenome;
                    pacientevalor.Endereço = reg.Endereço;
                    pacientevalor.Email = reg.Email;
                    pacientevalor.Telefone = reg.Telefone;
                    pacientevalor.Whatsapp = reg.Whatsapp;
                    pacientevalor.RG = reg.RG;
                    pacientevalor.CPF = reg.CPF;
                    pacientevalor.Indicação = reg.Indicação;
                    pacientevalor.Dentista = reg.Dentista;
                    listaPaciente.Add(pacientevalor);
                }
                _paciente.Pacientes = listaPaciente;
                return View(_paciente);
            }

        }
        [HttpGet]
        public ActionResult Agendamento()
        {
            using (var dbc = new ConsultaContexto())
            {

                var _consulta2 = dbc.Consultas.OrderBy(c => c.Dia).ThenBy(m => m.Hora).ToList();
                var data = new Consulta()
                {
                    Consultas = _consulta2
                };

                return View(data);
            }


        }
        [HttpPost]
        public ActionResult Agendamento(Consulta _consulta2)
        {

            using (var dbc = new ConsultaContexto())
            {


                var consultaPesquisa2 = from consultarec in dbc.Consultas
                                        where ((_consulta2.Dia == null) || (consultarec.Dia == _consulta2.Dia.Trim()))
                                        && ((_consulta2.Dentista == null) || (consultarec.Dentista == _consulta2.Dentista))
                                        && ((_consulta2.Paciente == null) || (consultarec.Paciente == _consulta2.Paciente))
                                        orderby consultarec.Dia ascending
                                        orderby consultarec.Dia ascending, consultarec.Hora ascending

                                        select new
                                        {
                                            AtendimentoID = consultarec.AtendimentoID,
                                            Paciente = consultarec.Paciente,
                                            Dia = consultarec.Dia,
                                            Hora = consultarec.Hora,
                                            Dentista = consultarec.Dentista

                                        };
                List<Consulta> listaConsulta2 = new List<Consulta>();

                foreach (var reg in consultaPesquisa2)
                {
                    Consulta consultavalor = new Consulta();
                    consultavalor.AtendimentoID = reg.AtendimentoID;
                    consultavalor.Paciente = reg.Paciente;
                    consultavalor.Dia = reg.Dia;
                    consultavalor.Hora = reg.Hora;
                    consultavalor.Dentista = reg.Dentista;
                    listaConsulta2.Add(consultavalor);
                }
                _consulta2.Consultas = listaConsulta2;
                return View(_consulta2);
            }

        }
        public ActionResult IndexConsulta()
        {
            return View(dbc.Consultas.ToList());
        }

        // GET: Consulta/Details/5
        public ActionResult DetailsConsulta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = dbc.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consulta/Create

        public ActionResult CreateConsulta()
        {
            return View();
        }

        // POST: Consulta/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConsulta([Bind(Include = "AtendimentoID,Paciente,Dentista,Dia,Hora")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                dbc.Consultas.Add(consulta);
                dbc.SaveChanges();
                TempData["mensagem"] = string.Format("A consulta foi agendada com sucesso");
                return RedirectToAction("Agendamento");
            }

            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public ActionResult EditConsulta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = dbc.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConsulta([Bind(Include = "AtendimentoID,Paciente,Dentista,Dia,Hora")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                dbc.Entry(consulta).State = EntityState.Modified;
                dbc.SaveChanges();
                TempData["mensagem"] = string.Format("A consulta foi alterada com sucesso");
                return RedirectToAction("Agendamento");
            }
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public ActionResult DeleteConsulta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = dbc.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("DeleteConsulta")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedConsulta(int id)
        {
            Consulta consulta = dbc.Consultas.Find(id);
            dbc.Consultas.Remove(consulta);
            dbc.SaveChanges();
            TempData["mensagem"] = string.Format("A consulta foi excluida com sucesso");
            return RedirectToAction("Agendamento");
        }

        // GET: Usuario/Create

        public ActionResult CreateSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSenha([Bind(Include = "ID,NomeUsuario,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                dbu.Usuarios.Add(usuario);
                dbu.SaveChanges();
                TempData["mensagem"] = string.Format("A senha foi criada com sucesso");
                return RedirectToAction("Catalogo");
            }

            return View(usuario);
        }

        // GET: Consulta/Delete/5
        public ActionResult DeleteSenha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = dbu.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("DeleteSenha")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSenha(int id)
        {
            Usuario usuario = dbu.Usuarios.Find(id);
            dbu.Usuarios.Remove(usuario);
            dbu.SaveChanges();
            TempData["mensagem"] = string.Format("A senha foi excluida com sucesso");
            return RedirectToAction("Catalogo");
        }

        // GET: Usuario/Edit/5
        public ActionResult EditSenha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = dbu.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Consulta/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSenha([Bind(Include = "ID,NomeUsuario,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                dbu.Entry(usuario).State = EntityState.Modified;
                dbu.SaveChanges();
                TempData["mensagem"] = string.Format("A senha foi alterada com sucesso");
                return RedirectToAction("Catalogo");
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Segurança()
        {
            using (var dbc = new UsuarioContexto())
            {
                var _usuario = dbc.Usuarios.ToList();
                var data = new Usuario()
                {
                    Usuarios = _usuario
                };

                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Segurança(Usuario _usuario)
        {
            using (var dbc = new UsuarioContexto())
            {
                var usuarioPesquisa = from usuariorec in dbc.Usuarios
                                      where (usuariorec.NomeUsuario == _usuario.NomeUsuario)
                                   
                                   select new
                                   {
                                       ID = usuariorec.ID,
                                       NomeUsuario = usuariorec.NomeUsuario,
                                       Senha = usuariorec.Senha,


                                   };
                List<Usuario> listaUsuario = new List<Usuario>();

                foreach (var reg in usuarioPesquisa)
                {
                    Usuario usuariovalor = new Usuario();
                    usuariovalor.ID = reg.ID;
                    usuariovalor.NomeUsuario = reg.NomeUsuario;
                    usuariovalor.Senha = reg.Senha;

                    listaUsuario.Add(usuariovalor);
                }
                _usuario.Usuarios = listaUsuario;
                return View(_usuario);
            }

        }



    }
}

   


