using Estoque.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Estoque.Domain.Entities
{
    public abstract class Entidade : IEntidade
    {
        public Entidade()
        {
            ValidationErrors = new List<string>();
        }

        public DateTime? AlteradoEm { get; set; }
        public string AlteradoPor { get; set; }
        public abstract object[] ChavePrimaria { get; }
        public DateTime? CriadoEm { get; set; }
        public string CriadoPor { get; set; }

        public IList<string> ValidationErrors { get; protected set; }
    }
}
