using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.EFData.Contexts;

namespace Estoque.EFData.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuarios>, IUsuarioRepository
    {
        public UsuarioRepository(EstoqueContext dbContext) : base(dbContext)
        {
        }
    }
}
