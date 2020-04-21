using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Domain.Entities
{
    public class AutenticacaoUsuarios : Entidade
    {
        public override object[] ChavePrimaria => new object[] { UsuarioId };

        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public string Ramal { get; set; }
    }
}
