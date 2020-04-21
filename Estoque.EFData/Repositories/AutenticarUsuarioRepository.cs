using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.EFData.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.EFData.Repositories
{
    public class AutenticarUsuarioRepository : GenericRepository<AutenticacaoUsuarios>, IAutenticarUsuarioRepository
    {
        public AutenticarUsuarioRepository(EstoqueContext dbContext) : base(dbContext)
        {
        }
    }
}
