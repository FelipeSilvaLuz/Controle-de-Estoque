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

        [HttpGet]
        public IActionResult BuscarProdutoPorCodigo(string codigoProduto)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                var produtoPorCodigo = _produtoAppService.BuscarProdutos()
                    .Where(x => x.Codigo == codigoProduto).FirstOrDefault();

                if (produtoPorCodigo == null)
                {
                    sucesso = false;
                    return Json(new
                    {
                        sucesso = sucesso,
                        tipo = true ? "sucesso" : "alerta"
                    });
                }
                else
                {
                    return Json(new
                    {
                        sucesso = sucesso,
                        mensagens = mensagens,
                        dados = produtoPorCodigo,
                        tipo = true ? "sucesso" : "alerta"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar buscar produto por codigo");
                return Json(new
                {
                    sucesso = false,
                    tipo = "erro",
                    mensagens = new List<string> { "Erro ao executar ação, tente novamente ou entre em contato com o administrador." }
                });
            }
        }

        [HttpGet]
        public IActionResult BuscarTodosProdutos()
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                var listProdutos = _produtoAppService.BuscarProdutos();

                return Json(new
                {
                    sucesso = sucesso,
                    mensagens = mensagens,
                    dados = listProdutos,
                    tipo = true ? "sucesso" : "alerta"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar buscar produto");
                return Json(new
                {
                    sucesso = false,
                    tipo = "erro",
                    mensagens = new List<string> { "Erro ao executar ação, tente novamente ou entre em contato com o administrador." }
                });
            }
        }

        [HttpPost]
        public IActionResult ImageToBase64(IFormFile files)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                var imageBase64 = _produtoAppService.ImageToBase64(files);

                return Json(new
                {
                    sucesso = sucesso,
                    mensagens = mensagens,
                    dados = imageBase64,
                    tipo = true ? "sucesso" : "alerta"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar converter imagem para base64");
                return Json(new
                {
                    sucesso = false,
                    tipo = "erro",
                    mensagens = new List<string> { "Erro ao executar ação, tente novamente ou entre em contato com o administrador." }
                });
            }
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
                _logger.LogError(ex, "Erro ao tentar salvar produto");
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