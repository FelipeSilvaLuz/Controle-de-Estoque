using Microsoft.AspNetCore.Http;

namespace Estoque.Util.Models
{
    public class ProdutoViewModel
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public long Quantidade { get; set; }
        public IFormFile files { get; set; }
        public string Observacao { get; set; }
    }
}