using Estoque.Application.Interfaces;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util;
using System.Linq;

namespace Estoque.Application.Services
{
    public class AutenticacaoUsuarioAppService : IAutenticacaoUsuarioAppService
    {
        private readonly IAutenticarUsuarioRepository _autenticarUsuarioRepository;
        public AutenticacaoUsuarioAppService(IAutenticarUsuarioRepository autenticarUsuarioRepository)
        {
            _autenticarUsuarioRepository = autenticarUsuarioRepository;
        }
        public string ValidarUsuario(AutenticarUsuarioViewModel model, ref string mensagem)
        {

            var buscaBanco = _autenticarUsuarioRepository.Get(x => x.Email.ToLower() == model.Usuario.ToLower()).FirstOrDefault();

            if (buscaBanco == null)
            {
                mensagem = "E-mail não existe no sistema!";
                return null;
            }

            else if(buscaBanco.Senha != model.Senha)
            {
                mensagem = "Senha informada esta invalida!";
                return null;
            }

            // caso tenha acesso ele chega aqui
            return string.Empty;
        }
    }
}
