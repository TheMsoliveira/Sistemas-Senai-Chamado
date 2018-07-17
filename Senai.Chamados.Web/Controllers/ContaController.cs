using AutoMapper;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Contratos;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.Models;
using Senai.Chamados.Web.Util;
using Senai.Chamados.Web.ViewModels;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel Login)
        {
            // verificando se o estado do model é valido
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
                return View();
            }

            using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioDomain objUsuario = _repUsuario.Login(Login.Email, Hash.GerarHash(Login.Senha));

                if(objUsuario != null)
                {

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name,objUsuario.Nome),
                        new Claim(ClaimTypes.Email, objUsuario.Email),
                        //new Claim(ClaimTypes.PrimarySid, objUsuario.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, objUsuario.Id.ToString()),
                        // define uma Claim com um novo tipo
                        new Claim("Telefone", objUsuario.Telefone.ToString()),
                        new Claim(ClaimTypes.Role, objUsuario.TipoUsuario.ToString()),
                    }, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(identities: identity);

                    return  RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválida. Tente novamente";
                    return View(Login);
                }

            }
            /*trecho de código não é mais necessário uma vez que a validação esta sendo feita acima*/
            //// Valida Usuário
            //if (Login.Email == "senai@senai.sp" && Login.Senha == "123456")
            //{
            //    TempData["Autenticado"] = "Usuário Autenticado";
            //    //redireciona para a pagina Home -- para outro controller
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ViewBag.Autenticado = "Usuario não cadastrado";
            //    // envia para pagina Cadastrar Usuário -- para mesmo controller
            //    return RedirectToAction("CadastrarUsuario");
            //}

            //TODO: efetuar Login
            //return View(); -- passa a não ser util por conta do Validação de Usuário
        }


        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            //CadastrarUsuarioViewModel objCadastrarUsuario = new CadastrarUsuarioViewModel();
           UsuarioViewModel objCadastrarUsuario = new UsuarioViewModel();
            //objCadastrarUsuario.Nome = "Madson";
            //objCadastrarUsuario.Email = "madson@senai.com";
            objCadastrarUsuario.ListaSexo = new SelectList(
            new List<SelectListItem>
            {
            new SelectListItem{ Text = "Masculino", Value = "1" },
            new SelectListItem { Text = "Feminino", Value = "2" },
            }, "Value", "Text");

            return View(objCadastrarUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            usuario.ListaSexo = ListaSexo();

            // verificando se o estado do model é valido
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
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

            /*não utilizado após a ultima implementação*/
            //SenaiChamadosDbContext objContext = new SenaiChamadosDbContext();
            UsuarioDomain usuarioBanco = new UsuarioDomain();

            try
            {
                /*ao tentar adicionar um novo usuario necessário iniciar o id*/
                //usuarioBanco.Id = Guid.NewGuid();

                ///*não serão mais necessários as linhas abaixo por conta do AutoMapper*/
                //usuarioBanco.Nome = usuario.Nome;
                //usuarioBanco.Email = usuario.Email;
                //usuarioBanco.Senha = usuario.Senha;
                //usuarioBanco.Telefone = usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                ///*necessário incluir após a criação da view dos campos*/
               // usuarioBanco.Cpf = usuario.Cpf.Replace(".", "").Replace(".", "").Replace(".", "").Replace("-", "");
                //usuarioBanco.Cep = usuario.Cep.Replace("-", "");
                
                /*usuario banco não sera mais necessário pois a viewModel esta sendo convertida para o Domain*/
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace(".", "").Replace(".", "").Replace("-", "");
                usuario.Cep = usuario.Cep.Replace("-", "");
                //usuarioBanco.Logradouro = usuario.Logradouro;
                //usuarioBanco.Numero = usuario.Numero;
                //usuarioBanco.Complemento = usuario.Complemento;
                //usuarioBanco.Bairro = usuario.Bairro;
                //usuarioBanco.Cidade = usuario.Cidade;
                //usuarioBanco.Estado = usuario.Estado;

                //usuarioBanco.DataCriacao = DateTime.Now;
                //usuarioBanco.DataAlteracao = DateTime.Now;

                /*Usuarios - necessário add pacote do NuGet*/
                //objContext.Usuarios.Add(usuarioBanco);
                //objContext.SaveChanges();

                usuario.Telefone = usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                usuario.Cep = usuario.Cep.Replace("-", "");
                usuario.TipoUsuario = EnTipoUsuario.Padrao; 
                usuario.Senha = Hash.GerarHash(usuario.Senha);


                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    //_repUsuario.Inserir(usuarioBanco);
                    _repUsuario.Inserir(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));

                }


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
                //objContext = null;
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

        [HttpGet]
        public ActionResult Deletar(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuario";
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
                    TempData["Erro"] = "Usuário excluido";
                    return RedirectToAction("Index");
                }

            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }

    }
}