using Estoque.Application.Interfaces;
using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.MvcCore.Controllers
{
    public class ItensController : BaseController
    {
        private readonly ILogger<ItensController> _logger;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IRegistroVendasAppService _registroVendasAppService;

        public ItensController(
            ILogger<ItensController> logger,
            IProdutoAppService produtoAppService,
            IRegistroVendasAppService registroVendasAppService)
        {
            _logger = logger;
            _produtoAppService = produtoAppService;
            _registroVendasAppService = registroVendasAppService;
        }

        [HttpGet]
        public IActionResult BuscarProdutoPorCodigo(string codigoProduto, bool telaDetalhes)
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
                        tipo = sucesso ? "sucesso" : "alerta"
                    });
                }
                else
                {
                    if (telaDetalhes)
                        produtoPorCodigo.DetalhesProduto = _registroVendasAppService
                            .BuscarRegistrosDeVendas(codigoProduto).ToList();

                    return Json(new
                    {
                        sucesso = sucesso,
                        mensagens = mensagens,
                        dados = produtoPorCodigo,
                        tipo = sucesso ? "sucesso" : "alerta"
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

        [HttpGet]
        [Route("Produtos/DownloadDadosProduto/{codigo}")]
        public IActionResult DownloadDadosProduto(string codigo)
        {
            try
            {
                var arquivo = _produtoAppService.DownloadDadosVenda(codigo);

                return new FileContentResult(arquivo?.Arquivo, arquivo?.ContentType)
                {
                    FileDownloadName = arquivo?.NomeArquivo
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar criar arquivo de download do produto");
                return null;
            }
        }

        [HttpPost]
        public IActionResult ImageToBase64(IFormFile files)
        {
            try
            {
                bool sucesso = true;
                int imageWidth = 0;
                int imageHeight = 0;

                List<string> mensagens = new List<string>();

                var imageBase64 = _produtoAppService.ImageToBase64(files);

                if (imageBase64 != null)
                {
                    var image = Image.Load(files.OpenReadStream());

                    imageWidth = image.Width;
                    imageHeight = image.Height;
                }

                return Json(new
                {
                    sucesso = sucesso,
                    mensagens = mensagens,
                    dados = "data:image/jpeg;base64," + imageBase64,
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
        public IActionResult RemoverProduto(string codigo)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                sucesso = _produtoAppService.RemoverProduto(codigo);

                if (sucesso)
                {
                    mensagens.Add("Produto removido com sucesso!");
                    return Json(new
                    {
                        sucesso = sucesso,
                        mensagens = mensagens,
                        tipo = true ? "sucesso" : "alerta"
                    });
                }
                else
                {
                    mensagens.Add("Não foi possível remover o produto selecionado, tente novamente ou contate o administrador!");
                    return Json(new
                    {
                        sucesso = sucesso,
                        mensagens = mensagens,
                        tipo = true ? "sucesso" : "alerta"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover produto");
                return Json(new
                {
                    sucesso = false,
                    tipo = "erro",
                    mensagens = new List<string> { "Erro ao executar ação, tente novamente ou entre em contato com o administrador." }
                });
            }
        }

        [HttpPost]
        public IActionResult SalvarProduto(ProdutoViewModel view)
        {
            try
            {
                bool sucesso = true;
                List<string> mensagens = new List<string>();

                sucesso = _produtoAppService.SalvarProduto(view, NomeUsuario, ref mensagens);

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