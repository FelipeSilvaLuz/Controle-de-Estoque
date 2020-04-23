using Estoque.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Application.Interfaces
{
    public interface IAutenticacaoUsuarioAppService
    {
        AutenticarUsuarioViewModel ValidarUsuario(AutenticarUsuarioViewModel model, ref string mensagem);
    }
}
