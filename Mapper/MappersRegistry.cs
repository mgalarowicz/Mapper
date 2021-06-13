using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mapper
{
    public static class MappersRegistry
    {
        public static IImmutableDictionary<Type, IMapper> Initialize()
        {
            var mappers = new Dictionary<Type, IMapper>
            {
                { typeof(SomeOne), new MapperNumberOne() },
                { typeof(SomeOneDto), new MapperNumberOne() }
            };

            return mappers.ToImmutableDictionary();
        }
    }
}
