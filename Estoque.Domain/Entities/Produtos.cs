using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Domain.Entities
{
    public class Produtos : Entidade
    {
        public override object[] ChavePrimaria
        {
            get { return new object[] { ProdutoId, Codigo }; }
        }

        public long ProdutoId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public long Quantidade { get; set; }
        public string FotoBase64 { get; set; }
        public string NomeFoto { get; set; }
        public string Observacao { get; set; }
    }
}
