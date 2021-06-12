namespace Mapper
{
    public interface IConcreteMapper
    {
        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class;
    }
}