using System;
using System.Collections.Concurrent;

namespace Mapper
{
    public static class Mapping
    {
        public static void Register<TSource, TDest>(this ConcurrentDictionary<(Type, Type), Func<object, object, object>> mappings, Func<TSource, TDest, TDest> map)
        {
            mappings.TryAdd((typeof(TSource), typeof(TDest)), (source, destination) => map((TSource)source, (TDest)destination));
        }
    }
}