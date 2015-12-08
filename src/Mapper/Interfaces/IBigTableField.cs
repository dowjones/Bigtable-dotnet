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
