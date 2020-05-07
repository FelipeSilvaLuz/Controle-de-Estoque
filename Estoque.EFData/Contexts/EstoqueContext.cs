using Estoque.EFData.Map;
using Estoque.Util.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace Estoque.EFData.Contexts
{
    public class EstoqueContext : DbContext
    {
        private readonly IConfiguracaoEstoque _configuracaoEstoque;

        public EstoqueContext(IConfiguracaoEstoque configuracaoEstoque)
        {
            _configuracaoEstoque = configuracaoEstoque;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // define the database to use
            optionsBuilder.UseMySql(_configuracaoEstoque.DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AutenticacaoUsuariosMap());
            modelBuilder.ApplyConfiguration(new ProdutosMap());

        }
    }
}
