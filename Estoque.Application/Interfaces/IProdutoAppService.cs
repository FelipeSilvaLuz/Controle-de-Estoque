using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Estoque.Application.Interfaces
{
    public interface IProdutoAppService
    {
        List<ProdutoViewModel> BuscarProdutos();

        string ImageToBase64(IFormFile file);

        bool RemoverProduto(string codigo);

        bool SalvarProduto(ProdutoViewModel view, ref List<string> mensagens);
    }
}