using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IInjectableServiceLocator
    {
        bool CanCreate(Type type);

        object LocateService(Type type);
    }
}
