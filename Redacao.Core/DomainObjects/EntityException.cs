using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Core.DomainObjects
{
    public class EntityException : Exception
    {
        public EntityException()
        { }

        public EntityException(string message) : base(message)
        { }

        public EntityException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
