﻿using Senai.Chamados.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Domain.Contratos
{
    public interface IUsuarioRepositorio : IDisposable, IRepositorioBase<UsuarioDomain>
    {
        UsuarioDomain Login(string email, string senha); 
    }
}
