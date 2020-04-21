using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Util.Configuracoes
{
    public class ConfiguracaoEstoque : IConfiguracaoEstoque
    {
        private readonly IConfiguration _configuration;

        public ConfiguracaoEstoque(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DefaultConnection => _configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }
}
