
namespace Estoque.Domain.Entities
{
    public class RegistroVendas : Entidade
    {
        public override object[] ChavePrimaria => new object[] { VendaId };

        public int VendaId { get; set; }
        public string Codigo { get; set; }
        public string NomeProduto { get; set; }
        public double PrecoUnitario { get; set; }
        public string Vendedor { get; set; }
        public int Quantidade { get; set; }
        //public virtual Produtos Produtos { get; set; }
    }
}