using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Estoque.Application.Interfaces
{
    public interface IProdutoAppService
    {
        bool SalvarProduto(ProdutoViewModel view, ref List<string> mensagens);
        List<ProdutoViewModel> BuscarProdutos();
        string ImageToBase64(IFormFile file);
    }
}