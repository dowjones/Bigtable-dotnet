using BigtableNet.Mapper.Abstraction;

namespace BigtableNet.Mapper.Annotations
{
    public class BigTableFieldAttribute : BigTableFieldAnnotation
    {
        internal string Name { get; set; }

        public BigTableFieldAttribute()
        {
            
        }
        public BigTableFieldAttribute(string name)
        {
            Name = name;
        }
    }
}
