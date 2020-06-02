using AutoMapper;
using Estoque.Application.Interfaces;
using Estoque.Domain.Entities;
using Estoque.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Estoque.MvcCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAutenticacaoUsuarioAppService _autenticacaoUsuarioAppService;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(
            IAutenticacaoUsuarioAppService autenticacaoUsuarioAppService,
            ILogger<LoginController> logger,
            IMapper mapper)
        {
            _autenticacaoUsuarioAppService = autenticacaoUsuarioAppService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Autenticar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarAcesso([FromBody] AutenticarUsuarioViewModel dados)
        {
            try
            {
                bool sucesso = true;
                string mensagens = string.Empty;
                List<string> mensagem = new List<string>();

                var validar = _autenticacaoUsuarioAppService.ValidarUsuario(dados, ref mensagens);

                if (validar == null)
                {
                    sucesso = false;
                    return Json(new
                    {
                        sucesso = sucesso,
                        tipo = sucesso ? "sucesso" : "alerta",
                        mensagem = mensagens
                    });
                }
                else
                {
                    CriarSessoes(validar);

                    return Json(new
                    {
                        sucesso = sucesso,
                        tipo = sucesso ? "sucesso" : "alerta",
                        mensagem = mensagens
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar validar acesso de usuario no sistema");
                return Json(new
                {
                    sucesso = false,
                    tipo = "erro",
                    mensagens = new List<string> { "Erro ao executar ação, tente novamente ou entre em contato com o administrador." }
                });
            }
        }

        private void CriarSessoes(AutenticacaoUsuarios usuario)
        {
            HttpContext.Session.Set("Id", new byte[] { Convert.ToByte(usuario.UsuarioId) });
            HttpContext.Session.SetString("Email", usuario.Email);
            HttpContext.Session.SetString("CPF", usuario.CPF);
            HttpContext.Session.SetString("Ramal", usuario.Ramal);
            HttpContext.Session.SetString("Nome", usuario.Nome);
        }
    }
}