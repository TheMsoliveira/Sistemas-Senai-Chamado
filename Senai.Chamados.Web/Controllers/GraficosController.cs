using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Chamado;
using Senai.Chamados.Web.ViewModels.Grafico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    
    public class GraficosController : Controller
    {
        // GET: Graficos
        [Authorize]
        public ActionResult Index()
        {
            try
            {

                ListaGraficoViewModel vmGrafico = new ListaGraficoViewModel();
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
                #region Grafico Status

                // faz o agrupamento dos dados por status
                var grupoStatus = vmListaChamados.ListaChamados
                         .GroupBy(x => x.Status)
                         .Select(n => new
                         {
                             status = RetornaStatus(n.Key),
                             Quatidade = Convert.ToDouble(n.Count())
                         }).OrderBy(n => n.Quatidade);
                // atribui as labels que serão mostradas no grafico
                vmGrafico.GraficoStatus.Labels = grupoStatus.Select(x=>x.status).ToArray();

                //atribuir os dados que serão apresentados nos graficos
                vmGrafico.GraficoStatus.Data = grupoStatus.Select(x => x.Quatidade).ToArray();
                #endregion

                #region Grafico Setor

                // faz o agrupamento dos dados por status
                var grupoSetor = vmListaChamados.ListaChamados
                         .GroupBy(x => x.Status)
                         .Select(n => new
                         {
                             status = RetornaSetor(n.Key),
                             Quatidade = Convert.ToDouble(n.Count())
                         }).OrderBy(n => n.Quatidade);
                // atribui as labels que serão mostradas no grafico
                vmGrafico.GraficoSetor.Labels = grupoSetor.Select(x => x.status).ToArray();

                //atribuir os dados que serão apresentados nos graficos
                vmGrafico.GraficoSetor.Data = grupoStatus.Select(x => x.Quatidade).ToArray();
                #endregion

                return View(vmGrafico);
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View();
            }


           
        }

        private string RetornaStatus(EnStatus status)
        {

            switch (status)
            {
                case EnStatus.Aguardando:
                    return "Aguardando";
                case EnStatus.Iniciado:
                    return "Iniciado";
                case EnStatus.Finalizado:
                    return "Finalizado";

            }

            return null;
        }

        private string RetornaSetor(EnStatus status)
        {

            switch (status)
            {
                case EnStatus.Aguardando:
                    return "Aguardando";
                case EnStatus.Iniciado:
                    return "Iniciado";
                case EnStatus.Finalizado:
                    return "Finalizado";

            }

            return null;
        }


    }
}