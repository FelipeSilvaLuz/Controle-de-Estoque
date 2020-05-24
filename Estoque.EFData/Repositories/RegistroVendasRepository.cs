using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.EFData.Contexts;

namespace Estoque.EFData.Repositories
{
    public class RegistroVendasRepository : GenericRepository<RegistroVendas>, IRegistroVendasRepository
    {
        public RegistroVendasRepository(EstoqueContext dbContext) : base(dbContext)
        {
        }
    }
}