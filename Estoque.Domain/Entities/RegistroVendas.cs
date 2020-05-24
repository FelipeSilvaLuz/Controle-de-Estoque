
namespace Estoque.Domain.Entities
{
    public class RegistroVendas : Entidade
    {
        public override object[] ChavePrimaria => new object[] { VendaId };

        public long VendaId { get; set; }
        public string Codigo { get; set; }
        public string Vendedor { get; set; }
        public double PrecoVenda { get; set; }
    }
}