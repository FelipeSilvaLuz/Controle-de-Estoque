using Estoque.Domain.Entities;
using Estoque.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Application.Interfaces
{
    public interface IAutenticacaoUsuarioAppService
    {
        AutenticacaoUsuarios ValidarUsuario(AutenticarUsuarioViewModel model, ref string mensagem);
    }
}
