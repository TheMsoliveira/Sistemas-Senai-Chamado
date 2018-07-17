using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Chamado
{
    public class ChamadoViewModel :BaseViewModel
    {
        public ChamadoViewModel()
        {
            ListaSetor = CarregaListaSetores();
            ListaStatus = CarregaListaStatus();
        
        }
        /// <summary>
        /// Carrega Lista de setores a partir de mum enum
        /// </summary>
        /// <returns>Retorna um SelectList com os setores </returns>

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
        /// <summary>
        /// Carrega Lista de Status a partir de um Enum
        /// </summary>
        /// <returns>Retorna um SelectList com os status </returns>
        public SelectList CarregaListaStatus()
        {
            var listaStatus = new SelectList(Enum.GetValues(typeof(EnStatus)).Cast<EnStatus>().Select(c =>
              new SelectListItem
              {
                  Text = c.ToString(),
                  Value = ((int)c).ToString()

              }).ToList(), "Value", "Text");
            return listaStatus;
        }

        [Required(ErrorMessage ="Título deve ser preenchido")]
        [Display(Name = "Titulo Chamado")]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="A descrição deve ser preenchida ")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o setor ")]
        public EnSetor Setor { get; set; }

        [Required(ErrorMessage = "Informe o status")]
        public EnStatus Status { get; set; }


        public Guid IdUsuario { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }

        public SelectList ListaSetor { get; set; }
        public SelectList ListaStatus { get; set; }

    }
}