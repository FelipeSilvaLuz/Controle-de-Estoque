using Estoque.Application.Interfaces;
using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.MvcCore.Controllers
{
    public class ItensController : BaseController
    {
        private readonly ILogger<ItensController> _logger;
        private readonly IProdutoAppService _produtoAppService;
        public ItensController(
            ILogger<ItensController> logger,
            IProdutoAppService produtoAppService)
        {
            _logger = logger;
            _produtoAppService = produtoAppService;
        }


        [Route("listagem")]
        public IActionResult ListaItens()
        {

            ViewBag.verBotoes = true;

            return View();
        }

        [HttpPost]
        public IActionResult SalvarProduto(ProdutoViewModel view)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                sucesso = _produtoAppService.SalvarProduto(view, ref mensagens);

                if (mensagens.Any())
                {
                    return Json(new
                    {
                        sucesso = sucesso,
                        mensagens = mensagens,
                        tipo = true ? "sucesso" : "alerta"
                    });
                }

                return Json(new
                {
                    sucesso = sucesso,
                    mensagens = mensagens,
                    tipo = true ? "sucesso" : "alerta"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar fazer download do arquivo jurídico");
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