using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Senai.Chamados.Web.Models
{
    public class CadastrarUsuarioViewModel
    {   /*DataAnotation - validação de campos */
        [Display(Name ="Informr o Nome")]
        [Required(ErrorMessage ="Informe o campo nome")]
        public string  Nome{ get; set; }

        [Display(Name ="Informr o E-mail")]
        [Required(ErrorMessage = "Informe o campo e-mail")]
        [EmailAddress(ErrorMessage = "O email é invalido")]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }

        [Display(Name = "Informr o Telefone")]
        [Required(ErrorMessage = "Informe o campo telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone{ get; set; }

        [Display(Name = "Informe a Senha")]
        [Required(ErrorMessage = "Informe o campo senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}