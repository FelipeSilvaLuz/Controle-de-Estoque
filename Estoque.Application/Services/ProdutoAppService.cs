using AutoMapper;
using Estoque.Application.Interfaces;
using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Estoque.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutosRepository _produtosRepository;
        private readonly IRegistroVendasRepository _registroVendasRepository;

        public ProdutoAppService(
            IProdutosRepository produtosRepository,
            IRegistroVendasRepository registroVendasRepository,
            IMapper mapper)
        {
            _produtosRepository = produtosRepository;
            _registroVendasRepository = registroVendasRepository;
            _mapper = mapper;
        }

        public List<ProdutoViewModel> BuscarProdutos()
        {
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();

            var listaProdutos = _produtosRepository.GetAll().ToList();

            foreach (var item in listaProdutos)
            {
                ProdutoViewModel produto = new ProdutoViewModel
                {
                    ProdutoId = item.ProdutoId,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                    Nome = item.Nome,
                    Observacao = item.Observacao,
                    PrecoCusto = item.PrecoCusto,
                    PrecoVenda = item.PrecoVenda,
                    Quantidade = item.Quantidade,
                    Base64 = item.FotoBase64
                };

                produtos.Add(produto);
            }
            return produtos;
        }

        public ProdutoViewModel BuscarProdutoPorCodigo(string codigo)
        {
            var prod = _produtosRepository.Get(x => x.Codigo == codigo)
                .FirstOrDefault();

            if (prod == null)
                return null;

            ProdutoViewModel produto = new ProdutoViewModel
            {
                Codigo = prod.Codigo,
                Descricao = prod.Descricao,
                Nome = prod.Nome,
                Observacao = prod.Observacao,
                PrecoVenda = prod.PrecoVenda,
                Quantidade = prod.Quantidade
            };

            return produto;
        }

        public ArquivoView DownloadDadosVenda(string codigo)
        {
            var paginaExcel = new ExcelPackage();
            var conteudo = paginaExcel.Workbook.Worksheets.Add("Dados Produto");
            conteudo.DefaultColWidth = 18;

            ConteudoExcelDadosVenda(conteudo, codigo);

            ArquivoView arquivo = new ArquivoView
            {
                Arquivo = paginaExcel.GetAsByteArray(),
                NomeArquivo = "Código(" + codigo + ").xlsx",
                ContentType = "application/xlsx"
            };

            return arquivo;
        }

        public void ConteudoExcelDadosVenda(ExcelWorksheet conteudo, string codigo)
        {
            var produto = _produtosRepository.Get(x => x.Codigo == codigo).FirstOrDefault();

            var registroVendas = _registroVendasRepository.Get(x => x.Codigo == codigo).ToList();

            conteudo.Cells[2, 2].Style.Font.Bold = true;
            conteudo.Cells[2, 2].Style.Font.Size = 15;
            conteudo.Cells[2, 2].Value = "Dados de venda do produto: " + produto?.Nome;
            conteudo.Cells[5, 2].Value = "Descrição: " + produto?.Descricao;

            conteudo.Cells[12, 2].Value = "Venda Id";
            conteudo.Cells[12, 3].Value = "Preço Venda";
            conteudo.Cells[12, 4].Value = "Vendador";
            conteudo.Cells[12, 5].Value = "Data Venda";
            conteudo.Cells[12, 2, 12, 5].Style.Font.Bold = true;

            int contador = 13;

            foreach (var item in registroVendas)
            {
                conteudo.Cells[contador, 2].Value = item?.VendaId;
                conteudo.Cells[contador, 3].Value = item?.PrecoVenda;
                conteudo.Cells[contador, 4].Value = item?.Vendedor;
                conteudo.Cells[contador, 5].Value = item?.CriadoEm.Value.ToString("dd/MM/yyyy hh:mm:ss");

                contador++;
            }
            contador--;
            conteudo.Cells[12, 2, contador, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            conteudo.Cells[12, 2, contador, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            conteudo.Cells[12, 2, contador, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            conteudo.Cells[12, 2, contador, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        public string ImageToBase64(IFormFile file)
        {
            if (file == null)
                return string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public bool RemoverProduto(string codigo)
        {
            var produto = _produtosRepository.Get(x => x.Codigo == codigo).FirstOrDefault();

            if (produto != null)
            {
                _produtosRepository.Delete(new object[] { produto.ProdutoId, produto.Codigo });
                _produtosRepository.SaveChanges();

                return true;
            }
            return false;
        }

        public bool SalvarProduto(ProdutoViewModel view, string usuarioLogado, ref List<string> mensagens)
        {
            mensagens = ValidarCampos(view);

            if (mensagens.Any())
                return false;

            var buscaProduto = _produtosRepository.Get(x => x.Codigo == view.Codigo).FirstOrDefault();

            if (buscaProduto == null)
                CriarNovoProduto(view, usuarioLogado);
            else
                AlterarProduto(view, usuarioLogado, buscaProduto);

            return true;
        }

        private bool AlterarProduto(ProdutoViewModel view, string usuarioLogado, Produtos buscaProduto)
        {
            Produtos produtos = new Produtos();

            produtos.ProdutoId = buscaProduto.ProdutoId;
            produtos.Nome = view.Nome;
            produtos.Descricao = view.Descricao;
            produtos.Observacao = view.Observacao;
            produtos.PrecoCusto = view.PrecoCusto;
            produtos.PrecoVenda = view.PrecoVenda;
            produtos.Quantidade = view.Quantidade;
            produtos.Codigo = view.Codigo;
            produtos.AlteradoEm = DateTime.Now;
            produtos.AlteradoPor = usuarioLogado;

            if (view.files != null)
            {
                produtos.FotoBase64 = ImageToBase64(view.files);
                produtos.NomeFoto = view.files.FileName.Substring(0, view.files.FileName.IndexOf('.'));
            }
            else
            {
                produtos.FotoBase64 = buscaProduto.FotoBase64;
                produtos.NomeFoto = buscaProduto.NomeFoto;
            }

            _produtosRepository.Update(produtos);
            _produtosRepository.SaveChanges();

            return true;
        }

        private bool CriarNovoProduto(ProdutoViewModel view, string usuarioLogado)
        {
            Produtos produtos = new Produtos();

            produtos.Nome = view.Nome;
            produtos.Descricao = view.Descricao;
            produtos.Observacao = view.Observacao;
            produtos.PrecoCusto = view.PrecoCusto;
            produtos.PrecoVenda = view.PrecoVenda;
            produtos.Quantidade = view.Quantidade;
            produtos.Codigo = view.Codigo;
            produtos.FotoBase64 = ImageToBase64(view.files);
            produtos.NomeFoto = view.files.FileName.Substring(0, view.files.FileName.IndexOf('.'));
            produtos.CriadoEm = DateTime.Now;
            produtos.CriadoPor = usuarioLogado;

            _produtosRepository.Create(produtos);
            _produtosRepository.SaveChanges();

            return true;
        }

        private List<string> ValidarCampos(ProdutoViewModel view)
        {
            List<string> mensagens = new List<string>();

            if (view.Codigo == null || view.Codigo?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Código");

            if (view.Nome == null || view.Nome?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Nome Produto");

            if (view.Observacao == null || view.Observacao?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Observação");

            if (view.Descricao == null || view.Descricao?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Descricao");

            if (view.Quantidade == 0)
                mensagens.Add("Preencha o campo Quantidade");

            if (view.files == null && view.ExisteFoto == false)
                mensagens.Add("Preencha o campo Foto");

            if (view.PrecoCusto == 0)
                mensagens.Add("Preencha o campo Preco Custo");

            if (view.PrecoVenda == 0)
                mensagens.Add("Preencha o campo Preco Venda");

            return mensagens;
        }
    }
}