using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Estoque.MvcCore.Controllers
{
    public class VendasController : BaseController
    {
        private readonly ILogger<VendasController> _logger;
        private readonly IProdutoAppService _produtoAppService;
        public VendasController(
            ILogger<VendasController> logger,
            IProdutoAppService produtoAppService)
        {
            _logger = logger;
            _produtoAppService = produtoAppService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult BuscarProdutoPorCodigo(string codigoProduto)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                var prod = _produtoAppService.BuscarProdutoPorCodigo(codigoProduto);

                if (prod == null)
                    sucesso = false;

                return Json(new
                {
                    sucesso = sucesso,
                    tipo = sucesso ? "sucesso" : "alerta",
                    mensagens = mensagens,
                    nomeProduto = prod?.Nome,
                    valorUnitario = prod?.PrecoVenda,
                    observacao = prod?.Observacao
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar buscar produto por codigo!");
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