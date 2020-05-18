using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Estoque.Util.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public string PrecoCustoExibir { get { return ConverterParaMonetario(PrecoCusto); } }
        public double PrecoVenda { get; set; }
        public string PrecoVendaExibir { get { return ConverterParaMonetario(PrecoVenda); } }
        public long? Quantidade { get; set; }
        public string Base64 { get; set; }
        public IFormFile files { get; set; }
        public string Observacao { get; set; }

        public string ConverterParaMonetario(double valor)
        {
            return valor.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}