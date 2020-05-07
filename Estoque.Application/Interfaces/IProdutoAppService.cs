using Estoque.Util.Models;
using System.Collections.Generic;

namespace Estoque.Application.Interfaces
{
    public interface IProdutoAppService
    {
        bool SalvarProduto(ProdutoViewModel view, ref List<string> mensagens);
    }
}