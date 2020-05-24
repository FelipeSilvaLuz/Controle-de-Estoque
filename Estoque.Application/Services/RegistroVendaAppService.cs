using AutoMapper;
using Estoque.Application.Interfaces;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Util.Models;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.Application.Services
{
    public class RegistroVendaAppService : IRegistroVendasAppService
    {
        private readonly IRegistroVendasRepository _registroVendasRepository;
        private readonly IMapper _mapper;
        public RegistroVendaAppService(
            IRegistroVendasRepository registroVendasRepository,
            IMapper mapper)
        {
            _registroVendasRepository = registroVendasRepository;
            _mapper = mapper;
        }
        public List<RegistroVendasViewModel> BuscarRegistrosDeVendas(string codigo)
        {
            var listaVendas = _registroVendasRepository
                .Get(x => x.Codigo == codigo).ToList();

            return _mapper.Map<List<RegistroVendasViewModel>>(listaVendas);

        }
    }
}