﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IProdutosRepository _produtosRepository;

        public ProdutoAppService(
            IProdutosRepository produtosRepository,
            IMapper mapper)
        {
            _produtosRepository = produtosRepository;
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

        public string ImageToBase64(IFormFile file)
        {
            if (file == null)
                return string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public bool SalvarProduto(ProdutoViewModel view, ref List<string> mensagens)
        {
            mensagens = ValidarCampos(view);

            if (mensagens.Any())
                return false;

            var buscaProduto = _produtosRepository.Get(x => x.Codigo == view.Codigo).FirstOrDefault();

            if (buscaProduto == null)
                CriarNovoProduto(view);
            else
                AlterarProduto(view, buscaProduto);

            return true;
        }

        private bool AlterarProduto(ProdutoViewModel view, Produtos buscaProduto)
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
            produtos.AlteradoPor = "usuario logado";

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

        private bool CriarNovoProduto(ProdutoViewModel view)
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
            produtos.CriadoPor = "usuario logado";

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