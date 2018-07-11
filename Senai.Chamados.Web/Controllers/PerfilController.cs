using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.Util;
using Senai.Chamados.Web.ViewModels.Conta;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel senha )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Ocorreu um erro. Verifique!";
                    return View();
                }
                //obtem claims do usuario
                var identity = User.Identity as ClaimsIdentity;
                // pega valor do id do Usuario
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                // obtem o valor de uma clais não definica anteriormente em contaController
                var telefomne = identity.Claims.FirstOrDefault(x => x.Type == "Telefone").Value;

                using (UsuarioRepositorio objRepUsuario = new UsuarioRepositorio())
                {   
                    // verifica se a senha ionformada é igual a atual
                    UsuarioDomain objUsuario =  objRepUsuario.BuscarPorId(new Guid(id));
                    if (Hash.GerarHash(senha.SenhaAtual) != objUsuario.Senha)
                    {
                        //  senha invalida informo o usuario
                        ModelState.AddModelError("Senha Atual", "Senha incorreta");
                        return View();
                    }
                    // atribue o valor da nova senha ao objeto usuario
                    objUsuario.Senha = Hash.GerarHash(senha.NovaSenha);
                    // altera o usuario no bando
                    objRepUsuario.Alterar(objUsuario);
                    // Defimos a mensagem que ira aparecer na tela
                    TempData["Sucesso"] = "Senha Altereda";
                    // retornamos ao controller Usuario , Index
                    return RedirectToAction("Index", "Usuario");
                }


            }
            catch (Exception ex)
            {

                ViewBag.message = "Ocorreu um erro"+ex.Message;
                return View();
            }


            return RedirectToAction("Index", "Usuario");
        }
    }
}