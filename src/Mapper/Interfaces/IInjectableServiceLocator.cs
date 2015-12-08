using System;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IInjectableServiceLocator
    {
        bool CanCreate(Type type);

        object LocateService(Type type);
    }
}
