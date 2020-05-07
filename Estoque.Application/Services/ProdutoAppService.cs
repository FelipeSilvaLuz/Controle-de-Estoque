using Estoque.Application.Interfaces;
using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Estoque.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutosRepository _produtosRepository;
        public ProdutoAppService(
            IProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }
        public bool SalvarProduto(ProdutoViewModel view, ref List<string> mensagens)
        {
            mensagens = ValidarCampos(view);

            if (mensagens.Any())
                return false;

            Produtos produto = new Produtos
            {
                Nome = view.Nome,
                Descricao = view.Descricao,
                Observacao = view.Observacao,
                PrecoCusto = view.PrecoCusto,
                PrecoVenda = view.PrecoVenda,
                Quantidade = view.Quantidade,
                NomeFoto = view.files.FileName.Substring(0, view.files.FileName.IndexOf('.')),
                Codigo = view.Codigo,
                CriadoEm = DateTime.Now,
                CriadoPor = "usuario logado",
                FotoBase64 = ImageToBase64(view.files)
            };

            _produtosRepository.Create(produto);
            _produtosRepository.SaveChanges();

            return true;
        }

        public string ImageToBase64(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            return Convert.ToBase64String(memoryStream.ToArray());
        }
        private List<string> ValidarCampos(ProdutoViewModel view)
        {
            List<string> mensagens = new List<string>();

            if (view.Nome == null || view.Nome?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Nome");

            if (view.Observacao == null || view.Observacao?.Trim() == string.Empty)
                mensagens.Add("Preencha o campo Observação");

            if (view.Quantidade == 0)
                mensagens.Add("Preencha o campo Quantidade");

            if (view.files == null)
                mensagens.Add("Preencha o campo Foto");

            if (view.PrecoCusto == 0)
                mensagens.Add("Preencha o campo Preco Custo");

            if (view.PrecoVenda == 0)
                mensagens.Add("Preencha o campo Preco Venda");

            return mensagens;
        }
    }
}