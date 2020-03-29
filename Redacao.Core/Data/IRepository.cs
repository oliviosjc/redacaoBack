using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Core.Data
{
    public interface IRepository<T>  where T : IAggregateRoot
    {
    }
}
