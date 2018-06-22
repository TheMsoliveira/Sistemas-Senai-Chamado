using Senai.Chamados.Web.Models;
using Senai.Chamados.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        [HttpGet] /*definindo fomato de requisição*/
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            // verificando se o estado do model é valido
            if(!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados invalidos";
                return View();
            }
            //TODO: efetuar Login
            return View();
        }
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            // verificando se o estado do model é valido
            if(!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados invalidos";
                return View();
            }
            //TODO: efetuar cadastro no campo de dados
            return View();
        }
    }
}