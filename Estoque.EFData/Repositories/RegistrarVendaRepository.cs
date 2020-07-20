using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.EFData.Contexts;

namespace Estoque.EFData.Repositories
{
    public class RegistrarVendaRepository : GenericRepository<RegistroVendas>, IRegistrarVendaRepository
    {
        public RegistrarVendaRepository(EstoqueContext dbContext) : base(dbContext)
        {
        }
    }
}
