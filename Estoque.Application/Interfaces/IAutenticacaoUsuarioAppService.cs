using Estoque.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Application.Interfaces
{
    public interface IAutenticacaoUsuarioAppService
    {
        string ValidarUsuario(AutenticarUsuarioViewModel model);
    }
}
