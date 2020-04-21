using Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.EFData.Map
{
    public class AutenticacaoUsuariosMap : IEntityTypeConfiguration<AutenticacaoUsuarios>
    {
        public void Configure(EntityTypeBuilder<AutenticacaoUsuarios> builder)
        {
            builder.ToTable("AutenticacaoUsuarios");
            
            builder.HasKey(usuario => usuario.CPF);

            builder.Property(usuario => usuario.UsuarioId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(usuario => usuario.Email);
            builder.Property(usuario => usuario.Senha);
            builder.Property(usuario => usuario.Ramal);

            builder.Ignore(usuario => usuario.ChavePrimaria);
            builder.Ignore(usuario => usuario.ValidationErrors);
        }
    }
}
