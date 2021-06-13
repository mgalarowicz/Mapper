using System;
using System.Collections.Immutable;

namespace Mapper
{
    public interface IMapper
    {
        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class;
    }
}