using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Util.Models
{
    public class ArquivoView
    {
        public byte[] Arquivo { get; set; }
        public string ContentType { get; set; }
        public string NomeArquivo { get; set; }
    }
}
