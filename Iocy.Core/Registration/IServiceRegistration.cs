using System;
using System.Collections.Generic;

namespace Iocy.Core.Registration
{
    public interface IServiceRegistration
    {
        Type Implementation{get;}

        Type Interface { get;}

        object Instance { get;  }

        IDictionary<string, object> DependOnParameters {get;}
    }
}