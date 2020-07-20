using Estoque.Util.Models;
using System.Collections.Generic;

namespace Estoque.Application.Interfaces
{
    public interface IRegistroVendasAppService
    {
        List<RegistroVendasViewModel> BuscarRegistrosDeVendas(string codigo);

        bool RegistrarVenda(
          List<RegistrarVendaViewModel> venda,
          string usuarioLogado,
          ref List<string> mensagens);
    }
}