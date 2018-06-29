using AutoMapper;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.Models;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senai.Chamados.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap(typeof(CadastrarUsuarioViewModel), typeof(UsuarioDomain));
            Mapper.CreateMap(typeof(UsuarioViewModel), typeof(UsuarioDomain));
        }
    }
}