using System;
using System.Collections.Generic;
using System.Text;

namespace Estoque.Domain.Interfaces
{
    public interface IEntidade
    {
        object[] ChavePrimaria { get; }
        IList<string> ValidationErrors { get; }
    }
}
