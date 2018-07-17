using Senai.Chamados.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Chamado
{
    public class ListaChamadoViewModel
    {

        public List<ChamadoViewModel> ListaChamados { get; set; }
        public SelectList ListaSetores { get; set; }

        public SelectList CarregaListaSetores()
        {


            var listaSetores = new SelectList(Enum.GetValues(typeof(EnSetor)).Cast<EnSetor>().Select(c =>
                  new SelectListItem
                  {
                      Text = c.ToString(),
                      Value = ((int)c).ToString()

                  }).ToList(), "Value", "Text");
            return listaSetores;
        }


        public ListaChamadoViewModel()
        {
            ListaSetores = CarregaListaSetores();
        }
    }
}