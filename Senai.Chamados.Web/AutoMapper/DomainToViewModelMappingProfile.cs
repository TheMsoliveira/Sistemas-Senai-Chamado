using AutoMapper;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.Models;
using Senai.Chamados.Web.ViewModels.Chamado;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senai.Chamados.Web.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        
        protected override void Configure()
        {   /* usado para conversaõ de view model para domain e domain para view modelmodel */
            Mapper.CreateMap(typeof(UsuarioDomain), typeof(CadastrarUsuarioViewModel));
            Mapper.CreateMap(typeof(UsuarioDomain), typeof(UsuarioViewModel));
            Mapper.CreateMap(typeof(ChamadoDomain), typeof(ChamadoViewModel));
           
        }
    }
}