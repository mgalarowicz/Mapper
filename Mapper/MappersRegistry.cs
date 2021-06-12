using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mapper
{
    public static class MappersRegistry
    {
        public static IImmutableDictionary<(Type, Type), IConcreteMapper> Initialize()
        {
            var dictWithMappers = new Dictionary<(Type, Type), IConcreteMapper>
            {
                { (typeof(SomeOne), typeof(SomeOneDto)), new MapperNumberOne() }
            };

            return dictWithMappers.ToImmutableDictionary();
        }
    }
}
