using Estoque.Application.Interfaces;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Estoque.Application.Services
{
    public class AutenticacaoUsuarioAppService : IAutenticacaoUsuarioAppService
    {
        private readonly IAutenticarUsuarioRepository _autenticarUsuarioRepository;
        public AutenticacaoUsuarioAppService(IAutenticarUsuarioRepository autenticarUsuarioRepository)
        {
            _autenticarUsuarioRepository = autenticarUsuarioRepository;
        }
        public string ValidarUsuario(AutenticarUsuarioViewModel model)
        {

            var buscaBanco = _autenticarUsuarioRepository.Get(x => x.Email.ToLower() == model.Usuario.ToLower()).FirstOrDefault();

            return string.Empty;
        }
    }
}
