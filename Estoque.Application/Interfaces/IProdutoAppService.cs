using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Estoque.Application.Interfaces
{
    public interface IProdutoAppService
    {
        List<ProdutoViewModel> BuscarProdutos();

        ArquivoView DownloadDadosVenda(string codigo);

        string ImageToBase64(IFormFile file);

        bool RemoverProduto(string codigo);

        bool SalvarProduto(ProdutoViewModel view, string usuarioLogado, ref List<string> mensagens);
    }
}