﻿using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels.Chamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    [Authorize]//data anotation do owin
    public class ChamadoController : Controller
    {
        // GET: Chamado
        public ActionResult Index()
        {
            ListaChamadoViewModel vmListaChamados = new ListaChamadoViewModel();

            using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
            {
                if (User.IsInRole("Administrador"))
                {
                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repChamado.Listar());
                }
                else
                {
                    var claims = User.Identity as ClaimsIdentity;
                    var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repChamado.Listar(new Guid(id)));
                }


            }

            return View(vmListaChamados);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ChamadoViewModel vmChamado = new ChamadoViewModel();
            return View(vmChamado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ChamadoViewModel chamado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dadpos inválidos";
                    return View(chamado);
                }

                using (ChamadoRepositorio objRepChamado = new ChamadoRepositorio())
                {
                    var identity = User.Identity as ClaimsIdentity;
                    var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    chamado.IdUsuario = new Guid(id);

                    objRepChamado.Inserir(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));

                }
                TempData["Sucesso"] = "Chamado cadastrado com sucesso";
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View(chamado);
            }
        }

        [HttpGet]
        public ActionResult Editar(Guid? id)// string id evita erro ao carregar página
        {
            ChamadoViewModel objChamado = new ChamadoViewModel();


            try
            {
                /*bloco de validação de um id nulo*/
                if (id == null)
                {
                    TempData["Erro"] = "Id não identificado";
                    return RedirectToAction("Index"); ;
                }

                using (ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    // busca chamado pelo Id
                    //  objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepoChamado.BuscarPorId(new Guid (id)));
                    objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepoChamado.BuscarPorId(id.Value));

                    if (objChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";

                        return RedirectToAction("Index");
                    }

                    #region Buscar Id Usuario
                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    #endregion

                    if (User.IsInRole("Administrador") || idUsuario == objChamado.IdUsuario.ToString())
                        return View(objChamado);
                    else
                    {
                        TempData["Erro"] = "Este chamado pertence a outro usuario";
                        return RedirectToAction("Index");
                    }

                }

            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(objChamado);
            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ChamadoViewModel chamado)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados Invalidos";
                    return View(chamado);

                }



                using (ChamadoRepositorio objRepChamado = new ChamadoRepositorio())
                {
                    objRepChamado.Alterar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                    TempData["Sucesso"] = "Chamado Alterado";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        [HttpGet]
        //public ActionResult Excluir(string id)
        public ActionResult Excluir(Guid? id)
        {
            try
            {

                if (!User.IsInRole("Administrador"))
                {
                    TempData["Erro"] = "Voce não tem permissaõ de excluir o chamados";
                    return RedirectToAction("Index");
                }

                if (id == null)
                {
                    TempData["Erro"] = "Informe o id do chamado";
                    return RedirectToAction("Index");
                }

                ChamadoViewModel objChamado = new ChamadoViewModel();


                using (ChamadoRepositorio objRepChamado = new ChamadoRepositorio())
                {
                    //objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepChamado.BuscarPorId(new Guid(id)));
                    objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepChamado.BuscarPorId(id.Value));
                    if (objChamado == null)
                    {
                        TempData["Erro"] = "Chamado encontrado";
                        return RedirectToAction("Index");

                    }

                    #region Buscar Id Usuario
                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    #endregion 

                    if (User.IsInRole("Administrador") || idUsuario == objChamado.IdUsuario.ToString())
                    {
                        return View(objChamado);
                    }
                    TempData["Erro"] = "Você não possui permissão para excluir esste chamado";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(ChamadoViewModel chamado)
        {

            try
            {
                if (!User.IsInRole("Administrador"))
                {
                    TempData["Erro"] = "Voce não tem permissaõ de excluir o chamados";
                    return RedirectToAction("Index");
                }

                if (chamado.Id == Guid.Empty)
                {
                    TempData["Erro"] = "Infomr o Id do chamado";
                    return RedirectToAction("Index");
                }
                using (ChamadoRepositorio objRepChamado = new ChamadoRepositorio())
                {
                    ChamadoViewModel objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepChamado.BuscarPorId(chamado.Id));


                    if (objChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    objRepChamado.Deletar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(objChamado));
                    TempData["Sucesso"] = "Chamado excluido";
                    return RedirectToAction("Index");

                }



            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View(chamado);
            }


        }

    }
}