using Estoque.Application.Interfaces;
using Estoque.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Estoque.MvcCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAutenticacaoUsuarioAppService _autenticacaoUsuarioAppService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(
            IAutenticacaoUsuarioAppService autenticacaoUsuarioAppService,
            ILogger<LoginController> logger)
        {
            _autenticacaoUsuarioAppService = autenticacaoUsuarioAppService;
            _logger = logger;
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
    }
}