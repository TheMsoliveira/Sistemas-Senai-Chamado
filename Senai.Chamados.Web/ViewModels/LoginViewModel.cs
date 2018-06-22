using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Senai.Chamados.Web.ViewModels
{
    public class LoginViewModel
    {


        [Display(Name = "Informe o E-mail")]
        [Required(ErrorMessage = "Informe o campo e-mail")]
        [EmailAddress(ErrorMessage = "O email é invalido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Informe a Senha")]
        [Required(ErrorMessage = "Informe o campo senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}