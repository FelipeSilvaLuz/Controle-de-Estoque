using Estoque.Domain.Entities;
using Estoque.Util;
using Estoque.Util.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        UsuarioViewModel ValidarUsuario(UsuarioViewModel model, ref string mensagem);

        List<UsuarioViewModel> BuscarTodosUsuarios();
    }
}
