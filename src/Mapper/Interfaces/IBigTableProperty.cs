using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigTableProperty
    {
        bool IsSpecified { get; }

    }
    public interface IBigTableProperty<out T> : IBigTableProperty
    {
        T Value { get; }
    }
}
