using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Estoque.Util.Models
{
    public class ProdutoViewModel
    {
        public string Base64 { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public List<RegistroVendasViewModel> DetalhesProduto { get; set; }
        public bool ExisteFoto { get; set; }
        public IFormFile files { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public double PrecoCusto { get; set; }
        public string PrecoCustoExibir { get { return ConverterParaMonetario(PrecoCusto); } }
        public double PrecoVenda { get; set; }
        public string PrecoVendaExibir { get { return ConverterParaMonetario(PrecoVenda); } }
        public int ProdutoId { get; set; }
        public double Quantidade { get; set; }
        public string QuantidadeExibir { get { return ConverterParaNumero(Quantidade); } }
        public string ValorEstoqueExibir { get { return SomaProdutos(PrecoVenda, Quantidade); } }

        public string ConverterParaMonetario(double valor)
        {
            var resultado = valor.ToString("C2", CultureInfo.CurrentCulture);

            if (resultado.Contains("R$"))
                return resultado.Replace("R$ ", "");

            return resultado;
        }

        public string ConverterParaNumero(double valor)
        {
            var resultado = Convert.ToDecimal(valor).ToString("#,##0");

            return resultado;
        }

        public string SomaProdutos(double precoVenda, double quantidade)
        {
            var soma = precoVenda * quantidade;

            return soma.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}