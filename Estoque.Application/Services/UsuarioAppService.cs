using AutoMapper;
using Estoque.Application.Interfaces;
using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util;
using Estoque.Util.Models;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioAppService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public UsuarioViewModel ValidarUsuario(UsuarioViewModel model, ref string mensagem)
        {

            if (model.Email.Trim() == string.Empty || model.Senha.Trim() == string.Empty)
            {
                mensagem = "Preencha todos os campos!";
                return null;
            }

            var buscaBanco = _usuarioRepository.Get(x => x.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();

            if (buscaBanco == null)
            {
                mensagem = "E-mail não existe no sistema!";
                return null;
            }

            else if (buscaBanco.Senha != model.Senha)
            {
                mensagem = "Senha informada esta invalida!";
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(buscaBanco);
        }

        public List<UsuarioViewModel> BuscarTodosUsuarios()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
            var todoUsuarios = _usuarioRepository.GetAll().ToList();

            foreach (var item in todoUsuarios)
            {
                UsuarioViewModel usuario = new UsuarioViewModel
                {
                    UsuarioId = item.UsuarioId,
                    CPF = item.CPF,
                    Email = item.Email,
                    Nome = item.Nome,
                    Ramal = item.Ramal
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
