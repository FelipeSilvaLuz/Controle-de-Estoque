using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.EFData.Contexts;

namespace Estoque.EFData.Repositories
{
    public class ProdutosRepository : GenericRepository<Produtos>, IProdutosRepository
    {
        public ProdutosRepository(EstoqueContext dbContext) : base(dbContext)
        {
        }
    }
}
