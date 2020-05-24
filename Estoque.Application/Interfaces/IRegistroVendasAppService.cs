using Estoque.Util.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Application.Interfaces
{
    public interface IRegistroVendasAppService
    {
        List<RegistroVendasViewModel> BuscarRegistrosDeVendas(string codigo);
    }
}
