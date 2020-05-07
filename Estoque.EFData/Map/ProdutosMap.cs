using Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.EFData.Map
{
    public class ProdutosMap : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(prod => new { prod.ProdutoId, prod.Codigo });

            builder.Property(prod => prod.ProdutoId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(prod => prod.Codigo).IsRequired();
            builder.Property(prod => prod.Descricao);
            builder.Property(prod => prod.FotoBase64);
            builder.Property(prod => prod.NomeFoto);
            builder.Property(prod => prod.Nome);
            builder.Property(prod => prod.PrecoCusto);
            builder.Property(prod => prod.PrecoVenda);
            builder.Property(prod => prod.Quantidade);
            builder.Property(prod => prod.Observacao);

            builder.Ignore(usuario => usuario.ChavePrimaria);
            builder.Ignore(usuario => usuario.ValidationErrors);
        }
    }
}