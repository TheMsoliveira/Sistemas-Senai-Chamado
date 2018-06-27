using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Domain.Entidades;
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
        public ActionResult Login(LoginViewModel Login)
        {
            // verificando se o estado do model é valido
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados invalidos";
                return View();
            }

            // Valida Usuário
            if (Login.Email == "senai@senai.sp" && Login.Senha == "123456")
            {
                TempData["Autenticado"] = "Usuário Autenticado";
                //redireciona para a pagina Home -- para outro controller
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Autenticado = "Usuario não cadastrado";
                // envia para pagina Cadastrar Usuário -- para mesmo controller
                return RedirectToAction("CadastrarUsuario");
            }

            //TODO: efetuar Login
            //return View(); -- passa a não ser util por conta do Validação de Usuário
        }
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            CadastrarUsuarioViewModel objCadastrarUsuario = new CadastrarUsuarioViewModel();
            //objCadastrarUsuario.Nome = "Madson";
            //objCadastrarUsuario.Email = "madson@senai.com";
            objCadastrarUsuario.Sexo = new SelectList(
            new List<SelectListItem>
            {
            new SelectListItem{ Text = "Masculino", Value = "1" },
            new SelectListItem { Text = "Feminino", Value = "2" },
            }, "Value", "Text");

            return View(objCadastrarUsuario);
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            usuario.Sexo = ListaSexo();

            // verificando se o estado do model é valido
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados invalidos";
                return View(usuario);
            }

            #region Exemplo
            /*necessário retornar a mesma lista no post , pode ser feito dessa forma ou criando um metodo*/
            //usuario.Sexo= new SelectList(
            //new List<SelectListItem>
            //{
            //new SelectListItem{ Text = "Masculino", Value = "1" },
            //new SelectListItem { Text = "Feminino", Value = "2" },
            //}, "Value", "Text");
            #endregion

            SenaiChamadosDbContext objContext = new SenaiChamadosDbContext();
            UsuarioDomain usuarioBanco = new UsuarioDomain();

            try
            {
                /*ao tentar adicionar um novo usuario necessário iniciar o id*/
                usuarioBanco.Id = Guid.NewGuid();

                usuarioBanco.Nome = usuario.Nome;
                usuarioBanco.Email = usuario.Email;
                usuarioBanco.Senha = usuario.Senha;
                usuarioBanco.Telefone = usuario.Telefone.Replace("(","").Replace(")","").Replace("-","").Replace(" ", "").Trim();
                /*necessário incluir após a criação da view dos campos*/
                usuarioBanco.Cpf = usuario.Cpf.Replace(".","").Replace(".", "").Replace(".", "").Replace("-", "");
                usuario.Cep = usuario.Cep.Replace("-", "");
                usuarioBanco.Logradouro = usuario.Logradouro;
                usuarioBanco.Numero = usuario.Numero;
                usuarioBanco.Complemento = usuario.Complemento;
                usuarioBanco.Bairro = usuario.Bairro;
                usuarioBanco.Cidade = usuario.Cidade;
                usuarioBanco.Estado = usuario.Estado;

                usuarioBanco.DataCriacao = DateTime.Now;
                usuarioBanco.DataAlteracao = DateTime.Now;

                /*Usuarios - necessário add pacote do NuGet*/
                objContext.Usuarios.Add(usuarioBanco);
                objContext.SaveChanges();

                /*temp-data semelhante ao view bag porém é usado para views diferentes*/
                TempData["Mensagem"] = "Usuario cadastrado";
                return RedirectToAction("Login");
              
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(usuario);
                
            }
            finally
            {
                objContext = null;
                usuario = null;
            }
           
           
            
        }

        private SelectList ListaSexo()
        {
            return new SelectList(
            new List<SelectListItem>
            {
            new SelectListItem{ Text = "Masculino", Value = "1" },
            new SelectListItem { Text = "Feminino", Value = "2" },
            }, "Value", "Text");
        

        }
    }
}