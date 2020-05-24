
using System;

namespace Estoque.Util.Models
{
    public class RegistroVendasViewModel
    {
        public long VendaId { get; set; }
        public string Codigo { get; set; }
        public DateTime CriadoEm { get; set; }
        public string CriadoEmExibir { get { return ConverterData(CriadoEm); } }
        public string Vendedor { get; set; }
        public double PrecoVenda { get; set; }
        public string PrecoVendaExibir { get { return ConverterParaNumero(PrecoVenda); } }
        public string ConverterParaNumero(double valor)
        {
            var resultado = Convert.ToDecimal(valor).ToString("#,##0");

            return resultado;
        }
        public string ConverterData(DateTime data)
        {
            var retorno = data.Day.ToString().PadLeft(2, '0') + "/"
                + data.Month.ToString().PadLeft(2, '0') + "/"
                + data.Year.ToString().PadLeft(4, '0');

            return retorno;
        }
    }
}