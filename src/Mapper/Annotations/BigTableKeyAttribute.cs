using BigtableNet.Mapper.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableKeyAttribute : BigTableFieldAnnotation
    {
        public int Ordinal { get; set; }

        public BigTableKeyAttribute()
        {
            Ordinal = int.MaxValue;
        }
    }
}
