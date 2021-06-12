using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mapper
{
    class Program
    {
        private static readonly SomeOne someOneObj;
        private static readonly SomeOneDto someOneDtoObj;

        static void Main(string[] args)
        {
           var mappedDto = CEMapper.Map<SomeOne, SomeOneDto>(someOneObj, someOneDtoObj);
        }
    }

    public static class CEMapper
    {
        private static readonly IImmutableDictionary<(Type, Type), IConcreteMapper> _mappersRegistry;

        static CEMapper()
        {
            _mappersRegistry = MappersRegistry.Initialize();
        }

        public static IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> source) where TDest : class 
            => source.Select(src => Map<TSource, TDest>(src));

        public static IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> source, IEnumerable<TDest> destination)
            where TSource : class, IMappableObjectIdentifier
            where TDest : class, IMappableObjectIdentifier
                => source.Select(sourceObject =>
                {
                    var destinationObjectToMapTo = destination?.SingleOrDefault(dest => dest.Id == sourceObject.Id);
                    return Map<TSource, TDest>(sourceObject, destinationObjectToMapTo);
                });

        public static TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest: class
        {
            var dictKeyType = (typeof(TSource), typeof(TDest));    

            return _mappersRegistry.TryGetValue(dictKeyType, out var concreteMapper) 
                ? concreteMapper.Map<TSource, TDest>(source, destination)
                : throw new Exception("Bla bla bla no mapper found");
        }
    }
}
