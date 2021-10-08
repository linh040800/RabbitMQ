using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.Common
{
    public class CatalogDomainException : Exception
    {
        public CatalogDomainException()
        { }

        public CatalogDomainException(string message)
            : base(message)
        { }

        public CatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
