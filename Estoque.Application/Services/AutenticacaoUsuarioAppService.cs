using AutoMapper;
using Estoque.Application.Interfaces;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util;
using System.Linq;

namespace Estoque.Application.Services
{
    public class AutenticacaoUsuarioAppService : IAutenticacaoUsuarioAppService
    {
        private readonly IAutenticarUsuarioRepository _autenticarUsuarioRepository;
        private readonly IMapper _mapper;
        public AutenticacaoUsuarioAppService(
            IAutenticarUsuarioRepository autenticarUsuarioRepository,
            IMapper mapper)
        {
            _autenticarUsuarioRepository = autenticarUsuarioRepository;
            _mapper = mapper;
        }
        public AutenticarUsuarioViewModel ValidarUsuario(AutenticarUsuarioViewModel model, ref string mensagem)
        {

            if (model.Email.Trim() == string.Empty || model.Senha.Trim() == string.Empty)
            {
                mensagem = "Preencha todos os campos!";
                return null;
            }

            var buscaBanco = _autenticarUsuarioRepository.Get(x => x.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();

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

           return _mapper.Map<AutenticarUsuarioViewModel>(buscaBanco);
        }
    }
}
