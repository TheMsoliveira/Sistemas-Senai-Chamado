using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.Util;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    //[Authorize (Roles = "Administrador")] // liberação de acesso  para todos os campos
    public class UsuarioController : Controller
    {
        // GET: Usuario
        
        public ActionResult Index()
        {
            // commentario para usar o recurso de [Authorize (Roles = "Administrador")]  em apenas uma das funções
            //if (!User.IsInRole ("Administrador"))
            //{
            //    ViewBag.Erro = "Você não tem permissão para acessar esta tela";
            //    return View();
            //}
            ListaUsuarioViewModel vmListaUsuario = new ListaUsuarioViewModel();

            /*sempre que for fazer integração com a abase de dados usamos a classe repositorio do objeto : CRUD sempre usar o repositorio*/
            using (UsuarioRepositorio _repositorio = new UsuarioRepositorio())
            {
                vmListaUsuario.ListaUsuarios = Mapper.Map<List<UsuarioDomain>, List<UsuarioViewModel>>(_repositorio.Listar());
            }

            return View(vmListaUsuario);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult Editar(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o ID do Usuario";
                return RedirectToAction("Index");
            }

            UsuarioViewModel vmUsuario = new UsuarioViewModel();

            using (UsuarioRepositorio _repositorio = new UsuarioRepositorio())
            {
                vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repositorio.BuscarPorId(Id));

                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuario não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vmUsuario);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados invalidos";
                return View(usuario);


            }

            try
            {
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                usuario.Cep = usuario.Cep.Replace("-", "");
                usuario.Telefone = usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                usuario.Senha = Hash.GerarHash(usuario.Senha);

                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Alterar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));
                }
                TempData["Erro"] = "Usuario Editado";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Deletar(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(id));
                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuario não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {

                    return View(vmUsuario);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Deletar(UsuarioViewModel usuario)
        {
            if (usuario.Id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(usuario.Id));
                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuario não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    _repUsuario.Deletar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(vmUsuario));
                    TempData["Erro"] = "Usuario excluído";
                    return RedirectToAction("Index");
                }
            }

        }

       
    }

}
