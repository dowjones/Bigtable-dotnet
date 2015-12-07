using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Mapper.Interfaces
{
    public interface IBigTableField
    {
        bool IsSpecified { get; }

    }
    public interface IBigTableField<out T> : IBigTableField
    {
        T Value { get; }
    }
}
