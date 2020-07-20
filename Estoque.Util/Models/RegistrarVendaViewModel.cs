using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Util.Models
{
    public class RegistrarVendaViewModel
    {
        public int contador { get; set; }
        public string nomeProduto { get; set; }
        public string quantidade { get; set; }
        public string valorTotal { get; set; }
    }
}
