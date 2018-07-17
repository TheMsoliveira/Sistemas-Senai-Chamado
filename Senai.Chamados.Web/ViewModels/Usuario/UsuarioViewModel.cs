﻿using Senai.Chamados.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Usuario
{
    public class UsuarioViewModel : BaseViewModel
    {
        /*copiado de CadastrarUsuarioViewModel*/
        /*DataAnotation - validação de campos */
        [Display(Name = "Informe o Nome")]
        [Required(ErrorMessage = "Informe o campo nome")]
        public string Nome { get; set; }

        [Display(Name = "Informe o E-mail")]
        [Required(ErrorMessage = "Informe o campo e-mail")]
        [EmailAddress(ErrorMessage = "O email é invalido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Informe o Telefone")]
        [Required(ErrorMessage = "Informe o campo telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "Informe o CPF")]
        [Required(ErrorMessage = "informe o CPF")]
        [MaxLength(14)]
        public string Cpf { get; set; }



        [Display(Name = "Informe a Senha")]
        [Required(ErrorMessage = "Informe o campo senha")]
        [DataType(DataType.Password)]
        [MaxLength(8, ErrorMessage = " Número maximo de caracteres é 8")]
        [MinLength(4, ErrorMessage = "Número minimo de caracteres é 4")]
        public string Senha { get; set; }
        /*select list exibe uma lista de dados --  sempre realizarmos o uso de uma
         comboBox, necessário usar uma propriedade para lista e outra para captuda de valor*/

        public EnTipoUsuario TipoUsuario { get; set; }

        public SelectList ListaSexo { get; set; }
        //[Required(ErrorMessage = "Informe o Sexo")]
        
        public EnSexo Sexo { get; set; }

        [Display(Name = "Informe o CEP")]
        [MaxLength(9, ErrorMessage = "O CEP deve conter 9 digitos")]
        public string Cep { get; set; }

        [Display(Name = "Informe o logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Informe o número")]
        public string Numero { get; set; }

        [Display(Name = "Informe o complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Informe o bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Informe a cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Informe o estado")]
        public string Estado { get; set; }


        

    }
}