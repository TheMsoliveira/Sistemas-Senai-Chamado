using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senai.Chamados.Web.AutoMapper
{
    /// <summary>
    /// configura o Domain espelhado no ViewModel
    /// </summary>
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        
    }
}
}